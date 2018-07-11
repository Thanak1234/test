/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.FAD;
using System.Linq;

namespace Workflow.Business
{
    public class FADRequestFormBC : 
        AbstractRequestFormBC<FADRequestWorkflowInstance, IDataProcessing>, 
        IFADRequestFormBC
    {
        private IAssetDisposalRepository _assetDisposalRepository = null;
        private IAssetDisposalDetailRepository _assetDisposalDetailRepository = null;
        private IAssetControlDetailRepository _assetControlDetailRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public FADRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _assetDisposalRepository = new AssetDisposalRepository(dbFactory);
            _assetDisposalDetailRepository = new AssetDisposalDetailRepository(dbFactory);
            _assetControlDetailRepository = new AssetControlDetailRepository(dbFactory);
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
            return PROCESSCODE.FAD;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var assetDisposal = _assetDisposalRepository.GetByRequestHeader(RequestHeader.Id);
            if (assetDisposal != null) {
                WorkflowInstance.AssetDisposal = assetDisposal;
                WorkflowInstance.AssetDisposalDetails = _assetDisposalDetailRepository.GetByRequestHeaderId(RequestHeader.Id);
                WorkflowInstance.AssetControlDetails = _assetControlDetailRepository.GetByRequestHeaderId(RequestHeader.Id);
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
                if (WorkflowInstance.AssetDisposal != null)
                {
                    var AssetDisposal = WorkflowInstance.AssetDisposal;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentAssetDisposal = _assetDisposalRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentAssetDisposal != null)
                        {
                            currentAssetDisposal.RequestHeaderId = AssetDisposal.RequestHeaderId;
                            currentAssetDisposal.CoporationBranch = AssetDisposal.CoporationBranch;
                            currentAssetDisposal.AssetGroupId = AssetDisposal.AssetGroupId;
                            currentAssetDisposal.TotalNetBookValue = AssetDisposal.TotalNetBookValue;
                            _assetDisposalRepository.Update(currentAssetDisposal);
                            isUpdate = true;

                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.AssetDisposal.RequestHeaderId = RequestHeader.Id;
                        _assetDisposalRepository.Add(AssetDisposal);
                    }
                    _dataField.Add("IsGaming", 
                        AssetDisposal.CoporationBranch == "Gaming" || 
                        AssetDisposal.CoporationBranch == "N2 - Gaming");
                    _dataField.Add("CoporationBranch", AssetDisposal.CoporationBranch);
                    _dataField.Add("TotalNBV", AssetDisposal.TotalNetBookValue); 

                    // Process transaction data for disposal detail item
                    ProcessAssetDisposalDetailData(WorkflowInstance.AddAssetDisposalDetails, DataOP.AddNew);
                    ProcessAssetDisposalDetailData(WorkflowInstance.EditAssetDisposalDetails, DataOP.EDIT);
                    ProcessAssetDisposalDetailData(WorkflowInstance.DelAssetDisposalDetails, DataOP.DEL);

                    // Process transaction data for asset control detail item
                    ProcessAssetControlDetailData(WorkflowInstance.AddAssetControlDetails, DataOP.AddNew);
                    ProcessAssetControlDetailData(WorkflowInstance.EditAssetControlDetails, DataOP.EDIT);
                    ProcessAssetControlDetailData(WorkflowInstance.DelAssetControlDetails, DataOP.DEL);

                }
                else
                {
                    throw new Exception("Fixed asset disposal form has no request instance");
                }
            }
        }


        private void ProcessAssetDisposalDetailData(IEnumerable<AssetDisposalDetail> assetDisposalDetails, DataOP op)
        {
            var assetControlDetailList = new List<AssetControlDetail>();
            if (assetDisposalDetails == null) return;
            
            foreach (var assetDisposalDetail in assetDisposalDetails)
            {
                assetDisposalDetail.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _assetDisposalDetailRepository.Add(assetDisposalDetail);
                    // update asset control acordingly
                    assetControlDetailList.Add(new AssetControlDetail()
                    {
                        AssetDisposalDetailId = assetDisposalDetail.Id,
                        SerialNo = assetDisposalDetail.SerialNo,
                        Description = assetDisposalDetail.Description
                    });
                    WorkflowInstance.AddAssetControlDetails = assetControlDetailList;
                }
                else if (DataOP.EDIT == op)
                {
                    _assetDisposalDetailRepository.Update(assetDisposalDetail);
                    // update asset control acordingly
                    assetControlDetailList.Add(new AssetControlDetail()
                    {
                        AssetDisposalDetailId = assetDisposalDetail.Id,
                        SerialNo = assetDisposalDetail.SerialNo,
                        Description = assetDisposalDetail.Description
                    });
                    WorkflowInstance.EditAssetControlDetails = assetControlDetailList;
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _assetDisposalDetailRepository.GetById(assetDisposalDetail.Id);
                    if (removeRecord != null)
                    {
                        _assetDisposalDetailRepository.Delete(removeRecord);
                        // update asset control acordingly
                        assetControlDetailList.Add(new AssetControlDetail()
                        {
                            AssetDisposalDetailId = assetDisposalDetail.Id,
                            SerialNo = assetDisposalDetail.SerialNo,
                            Description = assetDisposalDetail.Description
                        });
                        WorkflowInstance.DelAssetControlDetails = assetControlDetailList;
                    }
                }
            }
        }

        private void ProcessAssetControlDetailData(IEnumerable<AssetControlDetail> assetControlDetails, DataOP op)
        {
            if (assetControlDetails == null) return;

            foreach (var assetControlDetail in assetControlDetails)
            {
                assetControlDetail.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _assetControlDetailRepository.Add(assetControlDetail);
                }
                else if (DataOP.EDIT == op)
                {
                    _assetControlDetailRepository.Update(assetControlDetail);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _assetControlDetailRepository.GetById(assetControlDetail.Id);
                    if (removeRecord != null)
                    {
                        _assetControlDetailRepository.Delete(removeRecord);
                    }
                }
            }
        }

        #endregion
    }
}
