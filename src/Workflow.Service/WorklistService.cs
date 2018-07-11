using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Diagram;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.WM;
using Workflow.DataObject.Worklists;
using Workflow.Framework;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{

    public class WorklistService : IWorklistService {
        
        private readonly IActivityHistoryRepository _actLog;

        private readonly IProcInstProvider _provider;

        protected IWMRepository _repo;

        private string _account;

        public WorklistService(string userAccount = null) {
            IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _actLog = new ActivityHistoryRepository(dbFactory);
            _repo = new WMRepository(dbFactory);
            _provider = new ProcInstProvider(userAccount);
            _account = userAccount.FQNWithK2Label();
        }

        public object RunTest(string param)
        {
            if (!string.IsNullOrEmpty(param))
            {
                return _provider.GetWorklist();
            }
            return new { };
        }

        public ResourceWrapper GetWorklists(WMQueryParameter parameter)
        {
            return _repo.GetWorklists(parameter);
        }

        public ResourceWrapper GetProcInstAudits(int procInstId) {
            IEnumerable<InstanceAuditDto> records = _repo.GetProcInstAudits(procInstId);
            var wrapper = new ResourceWrapper();
            wrapper.TotalRecords = records.Count();
            wrapper.Records = records;
            return wrapper;
        }

        public void ExecuteAction(string serialNumber, string action, string sharedUser, string managedUser)
        {
            var param = new ExecInstParam()
            {
                SerialNo = serialNumber,
                Action = action
            };
            _provider.Execute(param, sharedUser, managedUser);
        }

        public List<ActivityDto> GetActivities() {
            return _repo.GetActivities();
        }

        public List<ProcessDto> GetProcesses() {
            return _repo.GetProcesses();
        }

        public IEnumerable<DestinationDto> GetSharedUsers(string sn) {
            var destUsers = _provider.GetSharedUsers(sn);
            return _repo.GetDestinationDto(destUsers.ToArray());
        }

        public List<WorkflowDto> GetWorkflows() {
            return _repo.GetWorkflows();
        }

        public ProcInst GetWorklistItem(int proctInstId)
        {
            var wItem = _provider.OpenWorklistItem(proctInstId);
            if (wItem != null)
            {
                wItem.OpenedBy = _repo.GetDisplayNameByUser(wItem.AllocatedUser);
            }
            return wItem;
        }
        
        public async Task<ResourceWrapper> GetWorklistWrapper()
        {
            var tasks = new List<object>();
            var k2WL = _provider.GetWorklist();

            var callback = new object();
            if (k2WL != null && k2WL.Count() > 0)
            {
                IList<string> procInstIds = k2WL.Select(p => p.ProcInstId.ToString()).ToList();
                var dbWL = _repo.GetWorklistHeader(procInstIds.ToArray());
                var query = from p in k2WL
                            join pm in dbWL on p.ProcInstId equals pm.ProcInstId
                            select p.Update(pm);

                tasks = query.ToList<object>();
            }

            return await Task.Factory.StartNew(() => new ResourceWrapper()
            {
                TotalRecords = tasks.Count(),
                Records = tasks
            });
        }

        public OOFWrapper GetShareWorklist() {
            
            var wrapper = _provider.GetOutOffice();

            if (wrapper != null) {
                var destLoginNames = wrapper.WorkType.Destinations.Select(p => p.LoginName);
                wrapper.WorkType.Destinations = _repo.GetDestinationDto(destLoginNames.ToArray());
                wrapper.WorkType.WorkTypeExceptions.Each(p =>
                {
                    p.Destinations = _repo.GetDestinationDto(p.Destinations.Select(x => x.LoginName).ToArray());
                    p.ProcessPath = p.ProcessPath.Replace(@"NAGAWORLD\", string.Empty);
                    p.ActDisplayName = p.Activity;
                });
            }

            return wrapper;
        }

        public bool Redirect(string serialNumber, DestinationDto user, Domain.Entities.ActivityHistory actLog) {
            var success = false;
            success = _provider.DoRedirect(serialNumber, user);

            if (success)
            {
                _actLog.Add(actLog);
                _actLog.Commit();
            }

            return success;
        }

        public void Release(string serialNumber) {
            _provider.DoRelease(serialNumber);
        }

        public bool SetOutOfOffice(OOFWrapper wrapper) {
            return _provider.SetOutOffice(wrapper);
        }

        public bool SetSharedUsers(string sn, IList<DestinationDto> users, Domain.Entities.ActivityHistory actLog) {
            var success = false;
            success = _provider.DoShare(sn, users);

            if (success) {
                _actLog.Add(actLog);
                _actLog.Commit();
            }
            
            return success;
        }
        
        public byte[] GetImageStream(int procInstId) {
            ViewControl control = new ViewControl();
            control.Xml = _provider.GetViewProcess(procInstId);
            return control.GetImageStream();
        }

        public object GetJson(int procInstId) {
            ViewControl control = new ViewControl();
            control.Xml = _provider.GetViewProcess(procInstId);
            return control.GetJson();
        }

        public object GetApprovers(int procInstId) {
            List<Activity> activities = new List<Activity>();
            int currentActInstId = 0;
            var xml = _provider.GetViewProcess(procInstId);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("ActInsts")) {
                var actInstId = Convert.ToInt32(GetXmlAttr(node, "ActInstID"));
                var actId = Convert.ToInt32(GetXmlAttr(node, "ActID"));
                var name = GetXmlAttr(node, "ActName");
                var status = Convert.ToInt32(GetXmlAttr(node, "Status"));

                if(status == 2) {
                    name = string.Format("{0} (Current)", name);
                    currentActInstId = Convert.ToInt32(GetXmlAttr(node, "ActInstID"));
                }

                if (!name.IsCaseInsensitiveEqual("Start") && NotExist(activities, actId, actInstId, status)) {
                    activities.Add(new Activity { status = status, actId = actInstId, name = name, id = actId});
                }
            }

            var result = _provider.GetParticipants(procInstId, currentActInstId);

            object participant = new { total = result.Participant.Count, data = result.Participant};

            return new { activityId = currentActInstId, participant = participant, activities = new { total = activities.Count, data = activities } };
        }

        private bool NotExist(List<Activity> activities, int actId, int actInstId, int status) {
            bool noExist = true;
            activities.ForEach(x => {
                if (x.id == actId) {
                    if(x.actId < actInstId)
                        x.actId = actInstId;

                    if (status == 2)
                        x.name = string.Format("{0} (Current)", x.name.Replace("(Current)", ""));

                    noExist = false;
                    return;
                }
            });

            return noExist;
        }

        public object GetParticipants(int procInstId, int actId) {
            var participant = _provider.GetParticipants(procInstId, actId);
            return participant != null ? new { total = participant.Participant.Count, data = participant.Participant }: null;
        }

        private string GetXmlAttr(XmlNode node, string attr) {
            XmlNode namedItem = node.Attributes.GetNamedItem(attr);
            if (namedItem == null)
                return "";
            return namedItem.Value;
        }

        public bool RetryError(int procInstID, int procId = 0) {
            return _provider.RetryError(procInstID, procId);
        }

        private class Activity {
            public int id { get; set; }
            public string name { get; set; }
            public int actId { get; set; }
            public int status { get; set; }
        }
    }

}
