/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.FAT;

namespace Workflow.Business
{
    public class FATRequestFormBC : 
        AbstractRequestFormBC<FATRequestWorkflowInstance, IDataProcessing>, 
        IFATRequestFormBC
    {
        private IAssetTransferRepository _assetTransferRepository = null;
        private IAssetTransferDetailRepository _assetTransferDetailRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public FATRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _assetTransferRepository = new AssetTransferRepository(dbFactory);
            _assetTransferDetailRepository = new AssetTransferDetailRepository(dbFactory);
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
            return PROCESSCODE.FAT;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var assetTransfer = _assetTransferRepository.GetByRequestHeader(RequestHeader.Id);
            if (assetTransfer != null) {
                WorkflowInstance.AssetTransfer = assetTransfer;
                WorkflowInstance.AssetTransferDetails = _assetTransferDetailRepository.GetByRequestHeaderId(RequestHeader.Id);
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
                if (WorkflowInstance.AssetTransfer != null)
                {
                    var assetTransfer = WorkflowInstance.AssetTransfer;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentAssetTransfer = _assetTransferRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentAssetTransfer != null)
                        {
                            currentAssetTransfer.RequestHeaderId = assetTransfer.RequestHeaderId;
                            currentAssetTransfer.TransferToDeptId = assetTransfer.TransferToDeptId;

                            _assetTransferRepository.Update(currentAssetTransfer);
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.AssetTransfer.RequestHeaderId = RequestHeader.Id;
                        _assetTransferRepository.Add(assetTransfer);
                    }

                    _dataField.Add("Company", assetTransfer.CompanyBranch);
                    _dataField.Add("TransferToDeptId", assetTransfer.TransferToDeptId);

                    // Process transaction data for request items
                    ProcessAssetTransferDetailData(WorkflowInstance.AddAssetTransferDetails, DataOP.AddNew);
                    ProcessAssetTransferDetailData(WorkflowInstance.EditAssetTransferDetails, DataOP.EDIT);
                    ProcessAssetTransferDetailData(WorkflowInstance.DelAssetTransferDetails, DataOP.DEL);
                    
                }
                else
                {
                    throw new Exception("Fixed asset transfer form has no request instance");
                }
            }
        }
        
        private void ProcessAssetTransferDetailData(IEnumerable<AssetTransferDetail> assetTransferDetails, DataOP op) {
            if (assetTransferDetails == null) return;

            foreach (var assetTransferDetail in assetTransferDetails)
            {
                assetTransferDetail.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _assetTransferDetailRepository.Add(assetTransferDetail);
                }
                else if (DataOP.EDIT == op)
                {
                    _assetTransferDetailRepository.Update(assetTransferDetail);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _assetTransferDetailRepository.GetById(assetTransferDetail.Id);
                    if (removeRecord != null) {
                        _assetTransferDetailRepository.Delete(removeRecord);
                    }
                }
            }
        } 
        #endregion
    }
}
