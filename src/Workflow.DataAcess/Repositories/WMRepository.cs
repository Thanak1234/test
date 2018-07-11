using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.WM;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories {

    public class WMRepository: RepositoryBase<Object>, IWMRepository {

        public WMRepository(IDbFactory dbFactory) : base(dbFactory) {

        }

        public IEnumerable<InstanceAuditDto> GetProcInstAudits(int procInstId)
        {
            string sql = string.Format("SELECT [PROCESS_INSTANCE_ID] ,[ACTIVITY_INSTANCE_ID] ,[PROCESS_NAME] ,[ACTIVITY_NAME] ,[FOLIO] ,[USER_NAME] ,[DATE] ,[AUDIT_DESCRIPTION] FROM K2.INSTANCE_AUDIT WHERE USER_NAME != 'K2Server' AND PROCESS_INSTANCE_ID = {0} ORDER BY [DATE] DESC", procInstId);
            IEnumerable<InstanceAuditDto> instanceAudits = SqlQuery<InstanceAuditDto>(sql);
            return instanceAudits;
        }

        public string GetDisplayNameByUser(string userName)
        {
            return SqlQuery<string>(string.Format(@"
                    SELECT TOP(1) (E.EMP_NO + ' - ' + DISPLAY_NAME) EMPLOYEE FROM HR.EMPLOYEE E 
                    WHERE E.LOGIN_NAME = REPLACE('{0}', 'K2:', '')", userName)).SingleOrDefault();
        }

        public IDictionary<string, string> GetPairLoginDisplayName(string[] users)
        {
            string sqlQuery = string.Format(
                @"SELECT DISTINCT LOWER(LOGIN_NAME) LoginName, DISPLAY_NAME DisplayName FROM HR.EMPLOYEE WHERE LOWER(LOGIN_NAME) IN ({0})", 
                string.Join(",", users));
            return SqlQuery<QueryResult>(sqlQuery).ToDictionary(p => p.LoginName, p => p.DisplayName);
            
        }

        public List<WlCriteriaDto> GetWorklistByUser(string userName) {
            return DbContext.Database.SqlQuery<WlCriteriaDto>(
            "EXEC K2..mGetWorklistCriteria @UserName=@UserName", new object[] {
                new SqlParameter("@UserName", userName)
            }).ToList();
        }

        public List<Worklist> GetDbWorklist(string account)
        {
            var worklist = SqlQuery<Worklist>("EXEC [K2].[WORKLIST] @UserName=@UserName", new object[] {
                new SqlParameter("@UserName", account)
            });
            if (worklist != null) {
                return worklist.ToList();
            }
            return new List<Worklist>();
        }
        
        public List<WorklistHeader> GetWorklistHeader(string[] procInstIds)
        {
            string sql = string.Format(@"SELECT 
	                RH.PROCESS_INSTANCE_ID ProcInstId, 
	                RH.ID RequestHeaderId,
	                (EMP.EMP_NO + ' - ' + ISNULL(EMP.DISPLAY_NAME, 'N/A')) Requestor,
	                RH.REQUEST_CODE RequestCode,
	                RH.LAST_ACTION_DATE LastActionDate,
	                RA.PROCESS_NAME ProcessName
                FROM BPMDATA.REQUEST_HEADER RH 
                INNER JOIN HR.EMPLOYEE EMP ON RH.REQUESTOR = EMP.ID 
                INNER JOIN BPMDATA.REQUEST_APPLICATION RA ON RA.REQUEST_CODE = RH.REQUEST_CODE
                WHERE RH.PROCESS_INSTANCE_ID IN ({0})", string.Join(",", procInstIds));
            return SqlQuery<WorklistHeader>(sql).ToList();
        }


        public RequestApplication GetReqAppByCode(string code)
        {

            IQueryable<Activity> activity = DbContext.Set<Activity>();
            var applications = DbContext.Set<RequestApplication>();
            var application = applications.Where(t => t.RequestCode == code);

            return application.Single();
        }

        public List<WorkflowDto> GetWorkflows()
        {

            IQueryable<Activity> activity = DbContext.Set<Activity>();
            IQueryable<RequestApplication> application = DbContext.Set<RequestApplication>();

            var query = (from a in activity
                         join rq in application
                         on a.WorkflowId equals rq.Id
                         select new WorkflowDto()
                         {
                             RequestDescription = rq.RequestDesc,
                             ProcessName = rq.ProcessName,
                             ActivityName = a.Name,
                             ActivityDisplayName = a.DisplayName
                         });


            return query.ToList();
        }

        public List<ProcessDto> GetProcesses()
        {
            return DbContext.Set<RequestApplication>()
                .Where(x => x.Active == true && x.ProcessPath != "" && x.ProcessPath != "N/A")
                .Select(x => new ProcessDto()
                {
                    Id = x.Id,
                    RequestCode = x.RequestCode,
                    RequestDesc = x.RequestDesc,
                    ProcessName = x.ProcessName,
                    ProcessCode = x.ProcessCode,
                    ProcessPath = x.ProcessPath,
                    FormUrl = x.FormUrl,
                    GenId = x.GenId,
                    IconIndex = x.IconIndex,
                    Active = x.Active
                }).ToList();
        }

        public List<ActivityDto> GetActivitiesByReqCode(string reqCode)
        {

            var activities = GetActivities().Where(t => t.RequestCode == reqCode);
            return activities.ToList();
        }

        public List<ActivityDto> GetActivities()
        {

            var context = DbContext as WorkflowContext;

            var query = (from p in context.RequestApplications
                         join a in context.Activities on p.Id equals a.WorkflowId
                         where p.Active == true && a.Active == true
                         select new ActivityDto()
                         {
                             RequestCode = p.RequestCode,
                             RequestDesc = p.RequestDesc,
                             ProcessPath = p.ProcessPath,
                             ProcessName = p.ProcessName,
                             Id = a.Id,
                             WorkflowId = a.WorkflowId,
                             Type = a.Type,
                             Name = a.Name,
                             DisplayName = a.DisplayName,
                             ActCode = a.ActCode,
                             Property = a.Property,
                             Sequence = a.Sequence,
                             Active = a.Active
                         });
            return query.ToList();
        }

        public Dictionary<string, dynamic> GetProcessDictionary()
        {

            var processList = GetProcesses();
            var result = new Dictionary<string, dynamic>();

            foreach (var dto in processList)
            {
                result.Add(dto.ProcessPath, new { RequestDesc = dto.RequestDesc, ProcessName = dto.ProcessName });
            }

            return result;
        }

        public List<DestinationDto> GetDestinationDto(string[] userNames)
        {
            var destinationDto = new List<DestinationDto>();
            var context = DbContext as WorkflowContext;

            foreach (string userName in userNames)
            {
                var employee = context.EmployeeViews.Where(x => x.LoginName.ToLower() == userName.ToLower()).FirstOrDefault();

                if (employee != null)
                {

                    var dto = new DestinationDto()
                    {
                        DeptType = employee.DeptType,
                        DisplayName = employee.DisplayName,
                        Email = employee.Email,
                        EmpNo = employee.EmpNo,
                        GroupName = employee.GroupName,
                        LoginName = employee.LoginName,
                        Manager = employee.Manager,
                        MobilePhone = employee.MobilePhone,
                        Position = employee.Position,
                        TeamName = employee.TeamName,
                        Telephone = employee.Telephone
                    };

                    destinationDto.Add(dto);
                }
            }

            return destinationDto;
        }

        public Dictionary<string, string> GetActivitiesDictionary()
        {

            var activityList = GetActivities();
            var result = new Dictionary<string, string>();

            foreach (var dto in activityList)
            {
                string key = string.Format("{0}\\{1}", dto.ProcessPath, dto.Name);
                result.Add(key, dto.DisplayName); ;
            }

            return result;
        }

        public ResourceWrapper GetWorklists(WMQueryParameter parameter)
        {

            object empNo = DBNull.Value;
            object folio = DBNull.Value;
            object procName = DBNull.Value;
            object activityName = DBNull.Value;
            object destination = DBNull.Value;
            object worklistDate = DBNull.Value;

            if (!string.IsNullOrEmpty(parameter.EmpNo))
                empNo = parameter.EmpNo;

            if (!string.IsNullOrEmpty(parameter.Folio))
                folio = parameter.Folio;

            if (!string.IsNullOrEmpty(parameter.ProcName))
                procName = parameter.ProcName;

            if (!string.IsNullOrEmpty(parameter.ActivityName))
                activityName = parameter.ActivityName;

            if (!string.IsNullOrEmpty(parameter.Destination))
                destination = parameter.Destination;

            if (parameter.WorklistDate != null)
                worklistDate = parameter.WorklistDate;

            SqlParameter totalRecords = new SqlParameter("@TotalCount", System.Data.SqlDbType.Int);
            totalRecords.Direction = ParameterDirection.Output;

            var worklists = this.DbContext.Database.SqlQuery<WLItemDto>(@"EXEC [K2]..[mGetAllWorklists] 
                                    @Start = @Start, 
                                    @Limit = @Limit, 
                                    @EmpNo = @EmpNo, 
                                    @Folio = @Folio, 
                                    @ProcName = @ProcName, 
                                    @ActivityName = @ActivityName, 
                                    @Destination = @Destination, 
                                    @WorklistDate = @WorklistDate, 
                                    @TotalCount = @TotalCount OUTPUT", 
                                    new object[] {
                                        new SqlParameter("@Start", parameter.start),
                                        new SqlParameter("@Limit", parameter.limit),
                                        new SqlParameter("@EmpNo", empNo),
                                        new SqlParameter("@Folio", folio),
                                        new SqlParameter("@ProcName", procName),
                                        new SqlParameter("@ActivityName", activityName),
                                        new SqlParameter("@Destination", destination),
                                        new SqlParameter("@WorklistDate", worklistDate),
                                        totalRecords
                                    }).ToList();

            ResourceWrapper resource = new ResourceWrapper();
            resource.Page = 0;
            resource.Size = parameter.limit;
            resource.Records = worklists;
            resource.TotalRecords = (int)totalRecords.Value;

            return resource;
        }
        
        private class QueryResult
        {
            public string DisplayName { get; set; }
            public string LoginName { get; set; }
        }
    }

}
