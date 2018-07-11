using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using System.Collections.Generic;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.VoucherRequest;
using Workflow.Domain.Entities.VoucherRequest;
using System;
using Workflow.DataAcess.Repositories;

namespace Workflow.Business.AccessCardRequest {

    public class RequestForm : AbstractRequestFormBC<RACWorkflowInstance, IDataProcessing>, IRequestForm {

        private IAccessCardRepository _repo = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            _repo = new AccessCardRepository(dbFactory);
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
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
                    IsSaveRequestData = false,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.RAC;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.Information = _repo.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var o = _repo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if(o == null) {
                    var n = WorkflowInstance.Information;
                    n.RequestHeaderId = RequestHeader.Id;
                    _repo.Add(n);
                } else {
                    o.RequestHeaderId = RequestHeader.Id;
                    o.Item = WorkflowInstance.Information.Item;
                    o.Reason = WorkflowInstance.Information.Reason;
                    o.Remark = WorkflowInstance.Information.Remark;
                    o.SerialNo = WorkflowInstance.Information.SerialNo;
                    o.IssueDate = WorkflowInstance.Information.IssueDate;
                    _repo.Update(o);
                }

                _dataField.Add("Reason", WorkflowInstance.Information.Reason);
            }
        }

        #endregion
    }
}
