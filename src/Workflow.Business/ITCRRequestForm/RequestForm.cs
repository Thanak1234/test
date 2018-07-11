using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using System.Linq;
using System.Collections.Generic;
using Workflow.DataAcess.Repositories.ITCR;
using Workflow.DataAcess.Repositories;
using System.Data.SqlClient;

namespace Workflow.Business.ITCRRequestForm
{
    public class RequestForm : AbstractRequestFormBC<ITCRequestWorkflowInstance, IDataProcessing>, IRequestForm {

        private IRequestFormDataRepository _repos;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            _repos = new RequestFormDataRepository(dbFactory);
        }

        protected override void InitActivityConfiguration() {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(
                () => {
                    return CreateEmailData("MODIFICATION");
                },
                new FormDataProcessing() {
                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = false,
                    IsSaveActivityHistory = true,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.ITCR;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.FormData = _repos.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var oFormData = _repos.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if (oFormData == null && WorkflowInstance.FormData != null)
                {
                    oFormData = WorkflowInstance.FormData;
                    oFormData.RequestHeaderId = RequestHeader.Id;
                    _repos.Add(oFormData);
                }
                else
                {
                    oFormData.DateRequest = WorkflowInstance.FormData.DateRequest;
                    oFormData.TargetDate = WorkflowInstance.FormData.TargetDate;
                    oFormData.Session = WorkflowInstance.FormData.Session;
                    oFormData.ChangeType = WorkflowInstance.FormData.ChangeType;
                    oFormData.RequestChange = WorkflowInstance.FormData.RequestChange;
                    oFormData.Justification = WorkflowInstance.FormData.Justification;
                    oFormData.Implmentation = WorkflowInstance.FormData.Implmentation;
                    oFormData.Failback = WorkflowInstance.FormData.Failback;
                    oFormData.Intervention = WorkflowInstance.FormData.Intervention;
                    oFormData.RestorationLavel = WorkflowInstance.FormData.RestorationLavel;
                    oFormData.DireedResult = WorkflowInstance.FormData.DireedResult;
                    oFormData.TestParameters = WorkflowInstance.FormData.TestParameters;
                    oFormData.ActualResult = WorkflowInstance.FormData.ActualResult;
                    oFormData.AdditionalNotes = WorkflowInstance.FormData.AdditionalNotes;
                    oFormData.AkResult = WorkflowInstance.FormData.AkResult;
                    oFormData.AkRemark = WorkflowInstance.FormData.AkRemark;
                    _repos.Update(oFormData);
                }

                if(CurrentActivity().ActivityName.IsCaseInsensitiveEqual("Submission") || CurrentActivity().ActivityName.IsCaseInsensitiveEqual("Requestor Rework"))
                {
                    _dataField.Add("DeptSession", GetApprovalRoleFromStoreProcedure());
                }
            }
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }        

        #endregion
    }
}
