/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.ITEIRQ;
using Workflow.Domain.Entities.ITEIRQ;

namespace Workflow.Business
{
    public class ITEIRQRequestFormBC : 
        AbstractRequestFormBC<ITEIRQRequestWorkflowInstance, IDataProcessing>, 
        IITEIRQRequestFormBC
    {
        private IEventInternetRepository _eventInternetRepository = null;
        private IQuotationRepository _quotationRepository = null;

        public ITEIRQRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _eventInternetRepository = new EventInternetRepository(dbFactory);
            _quotationRepository = new QuotationRepository(dbFactory);
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
            return PROCESSCODE.IBR;
        }
        
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var eventInternet = _eventInternetRepository.GetByRequestHeader(RequestHeader.Id);
            if (eventInternet != null) {
                WorkflowInstance.EventInternet = eventInternet;
                WorkflowInstance.Quotations = _quotationRepository.GetByRequestHeaderId(RequestHeader.Id);
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
                if (WorkflowInstance.EventInternet != null)
                {
                    var EventInternet = WorkflowInstance.EventInternet;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentEventInternet = _eventInternetRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentEventInternet != null)
                        {
                            currentEventInternet.RequestHeaderId = EventInternet.RequestHeaderId;
                            currentEventInternet.Subject = EventInternet.Subject;
                            currentEventInternet.StartDate = EventInternet.StartDate;
                            currentEventInternet.EndDate = EventInternet.EndDate;
                            currentEventInternet.Bandwidth = EventInternet.Bandwidth;
                            currentEventInternet.Cost = EventInternet.Cost;
                            currentEventInternet.requestDescr = EventInternet.requestDescr;
                            currentEventInternet.Comment = EventInternet.Comment;

                            _eventInternetRepository.Update(currentEventInternet);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.EventInternet.RequestHeaderId = RequestHeader.Id;
                        _eventInternetRepository.Add(EventInternet);
                    }

                    // Process transaction data for disposal detail item
                    ProcessQuotationData(WorkflowInstance.AddQuotations, DataOP.AddNew);
                    ProcessQuotationData(WorkflowInstance.EditQuotations, DataOP.EDIT);
                    ProcessQuotationData(WorkflowInstance.DelQuotations, DataOP.DEL);
                }
                else
                {
                    throw new Exception("Fixed asset disposal form has no request instance");
                }
            }
        }
        
        private void ProcessQuotationData(IEnumerable<Quotation> quotations, DataOP op)
        {
            if (quotations == null) return;

            foreach (var quotation in quotations)
            {
                quotation.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _quotationRepository.Add(quotation);
                }
                else if (DataOP.EDIT == op)
                {
                    _quotationRepository.Update(quotation);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _quotationRepository.GetById(quotation.Id);
                    if (removeRecord != null)
                    {
                        _quotationRepository.Delete(removeRecord);
                    }
                }
            }
        }

        #endregion
    }
}
