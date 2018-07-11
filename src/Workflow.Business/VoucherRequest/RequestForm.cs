using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Repositories.ITApp;
using Workflow.Domain.Entities.Core.ITApp;
using Workflow.DataAcess.Repositories.VAF;
using System.Linq;
using System.Collections.Generic;
using Workflow.Domain.Entities.VAF;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.VoucherRequest;
using Workflow.Domain.Entities.VoucherRequest;
using System;

namespace Workflow.Business.VoucherRequest {

    public class RequestForm : AbstractRequestFormBC<VRWorkflowInstance, IDataProcessing>, IRequestForm {

        private IRequestDataRepository requestDataRepo;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            requestDataRepo = new RequestDataRepository(dbFactory);
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
            return PROCESSCODE.VR;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.Information = requestDataRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var requestData = WorkflowInstance.Information;
                var entity = requestDataRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                int totalQty = requestDataRepo.GetTotalQty();
                if (entity == null) {                    
                    var nEntity = new RequestData();
                    requestData.QtyRequest = requestData.QtyRequest ?? 0;
                    nEntity.RequestHeaderId = RequestHeader.Id;
                    nEntity.VoucherType = requestData.VoucherType;
                    nEntity.QtyRequest = requestData.QtyRequest;
                    nEntity.DateRequired = requestData.DateRequired;
                    nEntity.VoucherNo = string.Format("{0}-{1:000000}/{2:000000}", DateTime.Now.ToString("yy"), totalQty + 1, totalQty + requestData.QtyRequest); ;
                    nEntity.AvailableStock = requestData.AvailableStock ?? 0;
                    nEntity.MonthlyUtilsation = requestData.MonthlyUtilsation ?? 0;
                    nEntity.IsReprint = requestData.IsReprint;
                    nEntity.HeaderOnVoucher = requestData.HeaderOnVoucher;
                    nEntity.Detail = requestData.Detail;
                    nEntity.Justification = requestData.Justification;
                    nEntity.ValidFrom = requestData.ValidFrom;
                    nEntity.ValidTo = requestData.ValidTo;
                    nEntity.Validity = requestData.Validity;
                    nEntity.Discount = requestData.Discount ?? 0;
                    nEntity.TC1ChangesByRequestor = requestData.TC1ChangesByRequestor;
                    nEntity.TC1ChangesByFinance = requestData.TC1ChangesByFinance;
                    nEntity.TC2ChangesByRequestor = requestData.TC2ChangesByRequestor;
                    nEntity.TC2ChangesByFinance = requestData.TC2ChangesByFinance;
                    nEntity.TC3ChangesByRequestor = requestData.TC3ChangesByRequestor;
                    nEntity.TC3ChangesByFinance = requestData.TC3ChangesByFinance;
                    nEntity.TC4ChangesByRequestor = requestData.TC4ChangesByRequestor;
                    nEntity.TC4ChangesByFinance = requestData.TC4ChangesByFinance;
                    nEntity.TC5ChangesByRequestor = requestData.TC5ChangesByRequestor;
                    nEntity.TC5ChangesByFinance = requestData.TC5ChangesByFinance;
                    nEntity.TC6ChangesByRequestor = requestData.TC6ChangesByRequestor;
                    nEntity.TC6ChangesByFinance = requestData.TC6ChangesByFinance;
                    nEntity.TC7ChangesByRequestor = requestData.TC7ChangesByRequestor;
                    nEntity.TC7ChangesByFinance = requestData.TC7ChangesByFinance;
                    nEntity.TC8ChangesByRequestor = requestData.TC8ChangesByRequestor;
                    nEntity.TC8ChangesByFinance = requestData.TC8ChangesByFinance;
                    nEntity.DoneByCreative = requestData.DoneByCreative;
                    nEntity.DoneByOutsideVendor = requestData.DoneByOutsideVendor;
                    nEntity.IsHotelVoucher = requestData.IsHotelVoucher;
                    nEntity.IsGamingVoucher = requestData.IsGamingVoucher;
                    requestDataRepo.Add(nEntity);
                    _dataField.Add("IsHotel", nEntity.IsHotelVoucher);
                } else {
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.VoucherType = requestData.VoucherType;
                    //if (CurrentActivity().ActivityName.IsCaseInsensitiveEqual("Finance Review") 
                    //    && CurrentActivity().CurrAction.ActionName.IsCaseInsensitiveEqual("Reviewed")
                    //    && string.IsNullOrEmpty(entity.VoucherNo))
                    //{
                    //    int qty = requestDataRepo.GetQty(RequestHeader.Id);
                    //    entity.VoucherNo = string.Format("{0}-{1:000000}/{2:000000}", DateTime.Now.ToString("yy"), qty + 1, qty + requestData.QtyRequest);
                    //}
                    //entity.QtyRequest = requestData.QtyRequest;
                    entity.DateRequired = requestData.DateRequired;
                    entity.AvailableStock = requestData.AvailableStock;
                    entity.MonthlyUtilsation = requestData.MonthlyUtilsation;
                    entity.IsReprint = requestData.IsReprint;
                    entity.HeaderOnVoucher = requestData.HeaderOnVoucher;
                    entity.Detail = requestData.Detail;
                    entity.Justification = requestData.Justification;
                    entity.ValidFrom = requestData.ValidFrom;
                    entity.ValidTo = requestData.ValidTo;
                    entity.Validity = requestData.Validity;
                    entity.Discount = requestData.Discount;
                    entity.TC1ChangesByRequestor = requestData.TC1ChangesByRequestor;
                    entity.TC1ChangesByFinance = requestData.TC1ChangesByFinance;
                    entity.TC2ChangesByRequestor = requestData.TC2ChangesByRequestor;
                    entity.TC2ChangesByFinance = requestData.TC2ChangesByFinance;
                    entity.TC3ChangesByRequestor = requestData.TC3ChangesByRequestor;
                    entity.TC3ChangesByFinance = requestData.TC3ChangesByFinance;
                    entity.TC4ChangesByRequestor = requestData.TC4ChangesByRequestor;
                    entity.TC4ChangesByFinance = requestData.TC4ChangesByFinance;
                    entity.TC5ChangesByRequestor = requestData.TC5ChangesByRequestor;
                    entity.TC5ChangesByFinance = requestData.TC5ChangesByFinance;
                    entity.TC6ChangesByRequestor = requestData.TC6ChangesByRequestor;
                    entity.TC6ChangesByFinance = requestData.TC6ChangesByFinance;
                    entity.TC7ChangesByRequestor = requestData.TC7ChangesByRequestor;
                    entity.TC7ChangesByFinance = requestData.TC7ChangesByFinance;
                    entity.TC8ChangesByRequestor = requestData.TC8ChangesByRequestor;
                    entity.TC8ChangesByFinance = requestData.TC8ChangesByFinance;
                    entity.IsHotelVoucher = requestData.IsHotelVoucher;
                    entity.IsGamingVoucher = requestData.IsGamingVoucher;
                    _dataField.Add("IsHotel", entity.IsHotelVoucher);
                    if (CurrentActivity().ActivityName.IsCaseInsensitiveEqual("Creative Review")) {
                        if(requestData.DoneByCreative == null) {
                            entity.DoneByCreative = true;
                            entity.DoneByOutsideVendor = false;
                            _dataField.Add("IsCreative", entity.DoneByCreative);
                        } else {
                            entity.DoneByCreative = requestData.DoneByCreative;
                            entity.DoneByOutsideVendor = requestData.DoneByOutsideVendor;
                            _dataField.Add("IsCreative", entity.DoneByCreative);
                        }
                    } 

                    requestDataRepo.Update(entity);
                }
            }
        }

        #endregion
    }
}
