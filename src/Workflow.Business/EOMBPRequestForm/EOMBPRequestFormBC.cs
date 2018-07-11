/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;

namespace Workflow.Business
{
    public class EOMBPRequestFormBC : 
        AbstractRequestFormBC<EOMBPRequestWorkflowInstance, IDataProcessing>, 
        IEOMBPRequestFormBC
    {
        private IBestPerformanceRepository _BestPerformanceRepository = null;
        private IBestPerformanceDetailRepository _BestPerformanceDetailRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public EOMBPRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _BestPerformanceRepository = new BestPerformanceRepository(dbFactory);
            _BestPerformanceDetailRepository = new BestPerformanceDetailRepository(dbFactory);
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
            return PROCESSCODE.EOMBP;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var BestPerformance = _BestPerformanceRepository.GetByRequestHeader(RequestHeader.Id);
            if (BestPerformance != null) {
                WorkflowInstance.BestPerformance = BestPerformance;
                WorkflowInstance.EmployeeOfMonthDetails = _BestPerformanceDetailRepository.GetByRequestHeaderId(RequestHeader.Id, "EOM");
                WorkflowInstance.BestPerformanceDetails = _BestPerformanceDetailRepository.GetByRequestHeaderId(RequestHeader.Id, "BP");
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
                if (WorkflowInstance.BestPerformance != null)
                {
                    var BestPerformance = WorkflowInstance.BestPerformance;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentBestPerformance = _BestPerformanceRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentBestPerformance != null)
                        {
                            currentBestPerformance.RequestHeaderId = BestPerformance.RequestHeaderId;
                            currentBestPerformance.EmployeeOfMonth = BestPerformance.EmployeeOfMonth;
                            currentBestPerformance.EOMAward = BestPerformance.EOMAward;
                            currentBestPerformance.BestPerformanceAward = BestPerformance.BestPerformanceAward;

                            _BestPerformanceRepository.Update(currentBestPerformance);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.BestPerformance.RequestHeaderId = RequestHeader.Id;
                        _BestPerformanceRepository.Add(BestPerformance);
                    }

                    // Process transaction data for request items
                    EmployeeOfMonthDetailData(WorkflowInstance.AddEmployeeOfMonthDetails, DataOP.AddNew);
                    EmployeeOfMonthDetailData(WorkflowInstance.EditEmployeeOfMonthDetails, DataOP.EDIT);
                    EmployeeOfMonthDetailData(WorkflowInstance.DelEmployeeOfMonthDetails, DataOP.DEL);

                    ProcessBestPerformanceDetailData(WorkflowInstance.AddBestPerformanceDetails, DataOP.AddNew);
                    ProcessBestPerformanceDetailData(WorkflowInstance.EditBestPerformanceDetails, DataOP.EDIT);
                    ProcessBestPerformanceDetailData(WorkflowInstance.DelBestPerformanceDetails, DataOP.DEL);
                }
                else
                {
                    throw new Exception("Fixed EOM Best Performance form has no request instance");
                }
            }
        }

        private void EmployeeOfMonthDetailData(IEnumerable<BestPerformanceDetail> BestPerformanceDetails, DataOP op)
        {
            if (BestPerformanceDetails == null) return;

            foreach (var BestPerformanceDetail in BestPerformanceDetails)
            {
                BestPerformanceDetail.RequestHeaderId = RequestHeader.Id;
                BestPerformanceDetail.Type = "EOM";
                if (DataOP.AddNew == op)
                {
                    _BestPerformanceDetailRepository.Add(BestPerformanceDetail);
                }
                else if (DataOP.EDIT == op)
                {

                    _BestPerformanceDetailRepository.Update(BestPerformanceDetail);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _BestPerformanceDetailRepository.GetById(BestPerformanceDetail.Id);
                    if (removeRecord != null)
                    {
                        _BestPerformanceDetailRepository.Delete(removeRecord);
                    }
                }
            }
        }

        private void ProcessBestPerformanceDetailData(IEnumerable<BestPerformanceDetail> BestPerformanceDetails, DataOP op) {
            if (BestPerformanceDetails == null) return;

            foreach (var BestPerformanceDetail in BestPerformanceDetails)
            {
                BestPerformanceDetail.RequestHeaderId = RequestHeader.Id;
                BestPerformanceDetail.Type = "BP";
                if (DataOP.AddNew == op)
                {
                    _BestPerformanceDetailRepository.Add(BestPerformanceDetail);
                }
                else if (DataOP.EDIT == op)
                {
                    _BestPerformanceDetailRepository.Update(BestPerformanceDetail);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _BestPerformanceDetailRepository.GetById(BestPerformanceDetail.Id);
                    if (removeRecord != null) {
                        _BestPerformanceDetailRepository.Delete(removeRecord);
                    }
                }
            }
        }
        #endregion
    }
}
