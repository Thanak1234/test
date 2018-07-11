using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.OSHA;
using Workflow.Domain.Entities.BatchData;
using System.Linq;
using Workflow.Domain.Entities.OSHA;
using System;

namespace Workflow.Business.OSHARequestForm
{
    public class OSHARequestForm : AbstractRequestFormBC<OSHARequestWorkflowInstance, IDataProcessing>, IOSHARequestForm {

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();
        private IOSHAEmployeeRepository OSHAEmployeeRepo;
        private IOSHAInformationRepository OSHAInformationRepo;

        public OSHARequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            OSHAEmployeeRepo = new OSHAEmployeeRepository(dbFactory);
            OSHAInformationRepo = new OSHAInformationRepository(dbFactory);
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.OSHA;
        }

        protected override void LoadFormData()
        {
            WorkflowInstance.Information = OSHAInformationRepo.GetByRequestHeader(RequestHeader.Id);
            WorkflowInstance.Victims = OSHAEmployeeRepo.GetMany(p => p.RequestHeaderId == RequestHeader.Id && p.EmpType == "VICTIMS").ToList();
            WorkflowInstance.Withness = OSHAEmployeeRepo.GetMany(p => p.RequestHeaderId == RequestHeader.Id && p.EmpType == "WITHNESS").ToList();
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }

        protected override void InitActivityConfiguration()
        {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(
                () => {
                    return CreateEmailData("MODIFICATION");
                },
                new FormDataProcessing()
                {
                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = false,
                    IsSaveActivityHistory = true,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                }
                )
            );
        }

        protected override void TakeFormAction() {
            var currentActivity = CurrentActivity();
            if (currentActivity.CurrAction.FormDataProcessing.IsSaveRequestData) {
                if (RequestHeader != null)
                {
                    var information = WorkflowInstance.Information;
                    bool isUpdate = false;

                    var currentInfo = OSHAInformationRepo.GetByRequestHeader(RequestHeader.Id);
                    if (currentInfo != null)
                    {
                        currentInfo.RequestHeaderId = information.RequestHeaderId;

                        currentInfo.NTA = information.NTA;
                        currentInfo.LAI = information.LAI;
                        currentInfo.DTA = information.DTA;
                        currentInfo.CA = information.CA;
                        currentInfo.DF = information.DF;
                        currentInfo.HSC = information.HSC;
                        currentInfo.YESDONE = information.YESDONE;
                        currentInfo.NODONE = information.NODONE;
                        currentInfo.ACNR = information.ACNR;
                        currentInfo.AT = information.AT;

                        currentInfo.DIEGSD = information.DIEGSD;
                        currentInfo.E1 = information.E1;
                        currentInfo.E2 = information.E2;
                        currentInfo.G3 = information.G3;
                        currentInfo.G4 = information.G4;
                        currentInfo.G5 = information.G5;
                        currentInfo.HCAT = information.HCAT;
                        
                        OSHAInformationRepo.Update(currentInfo);
                        information.RequestHeaderId = RequestHeader.Id;
                        isUpdate = true;
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.Information.RequestHeaderId = RequestHeader.Id;
                        OSHAInformationRepo.Add(information);
                    }

                    // process transaction of OSHA Victims
                    ProcessOSHAEmployeeData(WorkflowInstance.AddVictims, DataOP.AddNew);
                    ProcessOSHAEmployeeData(WorkflowInstance.EditVictims, DataOP.EDIT);
                    ProcessOSHAEmployeeData(WorkflowInstance.RemoveVictims, DataOP.DEL);

                    // process transaction of OSHA Withness
                    ProcessOSHAEmployeeData(WorkflowInstance.AddWithness, DataOP.AddNew);
                    ProcessOSHAEmployeeData(WorkflowInstance.EditWithness, DataOP.EDIT);
                    ProcessOSHAEmployeeData(WorkflowInstance.RemoveWithness, DataOP.DEL);
                }
                else
                {
                    throw new Exception("Fixed asset transfer form has no request instance");
                }
            }
        }

        private void ProcessOSHAEmployeeData(IEnumerable<OSHAEmployee> oshaEmployees, DataOP op)
        {
            if (oshaEmployees == null) return;

            foreach (var oshaEmployee in oshaEmployees)
            {
                oshaEmployee.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    OSHAEmployeeRepo.Add(oshaEmployee);
                }
                else if (DataOP.EDIT == op)
                {
                    OSHAEmployeeRepo.Update(oshaEmployee);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = OSHAEmployeeRepo.GetById(oshaEmployee.Id);
                    if (removeRecord != null)
                    {
                        OSHAEmployeeRepo.Delete(removeRecord);
                    }
                }
            }
        }
    }

}
