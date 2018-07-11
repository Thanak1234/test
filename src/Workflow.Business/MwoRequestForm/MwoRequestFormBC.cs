using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.MWO;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.MwoRequestForm
{
    public class MwoRequestFormBC : AbstractRequestFormBC<MwoRequestWorkflowInstance, IDataProcessing>, IMwoRequestFormBC {

        private IInformationRepository informationRepo;
        private DepartmentChargableRepository departmentRepo;

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public MwoRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            informationRepo = new InformationRepository(dbFactory);
            departmentRepo = new DepartmentChargableRepository(dbFactory);
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.MWO;
        }

        protected override void LoadFormData() {
            WorkflowInstance.Information = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
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
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var oInformation = informationRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if(oInformation == null)
                {
                    var nInformation = WorkflowInstance.Information;
                    
                    nInformation.JaTechnician = null;
                    nInformation.RequestHeaderId = RequestHeader.Id;
                    informationRepo.Add(nInformation);
                } else
                {                    
                    oInformation.Instruction = WorkflowInstance.Information.Instruction;
                    oInformation.JaDate = WorkflowInstance.Information.JaDate;
                    if(CurrentActivity().ActivityName ==  "ADM Approval" && CurrentActivity().CurrAction.ActionName == "Approved" && string.IsNullOrEmpty(oInformation.ReferenceNumber))
                    {
                        oInformation.ReferenceNumber = GetRefNum();
                    }
                    oInformation.Location = WorkflowInstance.Information.Location;
                    oInformation.Mode = WorkflowInstance.Information.Mode;
                    oInformation.Remark = WorkflowInstance.Information.Remark;
                    oInformation.RequestType = WorkflowInstance.Information.RequestType;
                    oInformation.SubLocation = WorkflowInstance.Information.SubLocation;
                    oInformation.TcDesc = WorkflowInstance.Information.TcDesc;
                    oInformation.WorkType = WorkflowInstance.Information.WorkType;
                    oInformation.Wrjd = WorkflowInstance.Information.Wrjd;
                    oInformation.CcdId = WorkflowInstance.Information.CcdId;
                    oInformation.JaTechnician = WorkflowInstance.Information.JaTechnician;

                    informationRepo.Update(oInformation);
                }
            }
        }

        private string GetRefNum()
        {
            string refs = string.Empty;
            var ccd = departmentRepo.Get(p => p.Id == WorkflowInstance.Information.CcdId);
            if (ccd != null)
            {
                ++ccd.CurrentNumber;
                refs = string.Format("{0}-{1:D6}", ccd.CCD, ccd.CurrentNumber);
                departmentRepo.Update(ccd);
            }
            return refs;
        }
    }

}
