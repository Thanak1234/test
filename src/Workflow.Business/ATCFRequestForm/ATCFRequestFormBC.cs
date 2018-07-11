/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.ATCF;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Business
{
    public class ATCFRequestFormBC : 
        AbstractRequestFormBC<ATCFRequestWorkflowInstance, IDataProcessing>, 
        IATCFRequestFormBC
    {
        private IAdditionalTimeWorkedRepository _AdditionalTimeWorkedRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public ATCFRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _AdditionalTimeWorkedRepository = new AdditionalTimeWorkedRepository(dbFactory);
        }

        #region Override Method
        protected override void InitActivityConfiguration()
        {
            AddActivities(new ActivityEngine());
            AddActivities(new ActivityEngine(REQUESTOR_REWORKED));
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(() =>
            {
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
            }));
        }

        protected override string GetRequestCode()
        {
            return PROCESSCODE.ATCF;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            if (RequestHeader != null) {
                WorkflowInstance.AdditionalTimeWorkeds = _AdditionalTimeWorkedRepository.GetByRequestHeaderId(RequestHeader.Id);
            }
        }

        protected override bool IsAuthorizeRemoveAttachment(string activityCode)
        {
            return (CurrentActivityCode == activityCode);
        }

        protected override void TakeFormAction() {
            var currentActvity = CurrentActivity();
            if (currentActvity.CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if (RequestHeader != null)
                {
                    // Process transaction data for request items
                    ProcessAdditionalTimeWorkedData(WorkflowInstance.AddAdditionalTimeWorkeds, DataOP.AddNew);
                    ProcessAdditionalTimeWorkedData(WorkflowInstance.EditAdditionalTimeWorkeds, DataOP.EDIT);
                    ProcessAdditionalTimeWorkedData(WorkflowInstance.DelAdditionalTimeWorkeds, DataOP.DEL);
                    
                }
                else
                {
                    throw new Exception("Fixed asset transfer form has no request instance");
                }
            }
        }
        
        private void ProcessAdditionalTimeWorkedData(IEnumerable<AdditionalTimeWorked> AdditionalTimeWorkeds, DataOP op) {
            if (AdditionalTimeWorkeds == null) return;

            foreach (var AdditionalTimeWorked in AdditionalTimeWorkeds)
            {
                AdditionalTimeWorked.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _AdditionalTimeWorkedRepository.Add(AdditionalTimeWorked);
                }
                else if (DataOP.EDIT == op)
                {
                    _AdditionalTimeWorkedRepository.Update(AdditionalTimeWorked);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _AdditionalTimeWorkedRepository.GetById(AdditionalTimeWorked.Id);
                    if (removeRecord != null) {
                        _AdditionalTimeWorkedRepository.Delete(removeRecord);
                    }
                }
            }
        } 
        #endregion
    }
}
