/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.HGVR;
using System.Linq;

namespace Workflow.Business
{
    public class HGVRRequestFormBC : 
        AbstractRequestFormBC<HGVRRequestWorkflowInstance, IDataProcessing>, 
        IHGVRRequestFormBC
    {
        private IVoucherHotelRepository _voucherHotelRepository = null;
        private IVoucherHotelFinanceRepository _voucherHotelFinanceRepository = null;
        private IVoucherHotelDetailRepository _voucherHotelDetailRepository = null;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public HGVRRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _voucherHotelRepository = new VoucherHotelRepository(dbFactory);
            _voucherHotelFinanceRepository = new VoucherHotelFinanceRepository(dbFactory);
            _voucherHotelDetailRepository = new VoucherHotelDetailRepository(dbFactory);
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
            return PROCESSCODE.HGVR;
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        } 
        #endregion

        #region Load/Save Form

        protected override void LoadFormData() {
            var voucherHotel = _voucherHotelRepository.GetByRequestHeader(RequestHeader.Id);
            if (voucherHotel != null) {
                WorkflowInstance.VoucherHotel = voucherHotel;
                WorkflowInstance.VoucherHotelFinances = _voucherHotelFinanceRepository.GetByRequestHeaderId(RequestHeader.Id);
                WorkflowInstance.VoucherHotelDetails = _voucherHotelDetailRepository.GetByRequestHeaderId(RequestHeader.Id);
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
                if (WorkflowInstance.VoucherHotel != null)
                {
                    var VoucherHotel = WorkflowInstance.VoucherHotel;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentVoucherHotel = _voucherHotelRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentVoucherHotel != null)
                        {
                            currentVoucherHotel.RequestHeaderId = VoucherHotel.RequestHeaderId;
                            currentVoucherHotel.PresentedTo = VoucherHotel.PresentedTo;
                            currentVoucherHotel.InChargedDept = VoucherHotel.InChargedDept;
                            currentVoucherHotel.Justification = VoucherHotel.Justification;
                            _voucherHotelRepository.Update(currentVoucherHotel);
                            isUpdate = true;

                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.VoucherHotel.RequestHeaderId = RequestHeader.Id;
                        _voucherHotelRepository.Add(VoucherHotel);
                    }

                    // Process transaction data for disposal detail item
                    ProcessVoucherHotelFinanceData(WorkflowInstance.AddVoucherHotelFinances, DataOP.AddNew);
                    ProcessVoucherHotelFinanceData(WorkflowInstance.EditVoucherHotelFinances, DataOP.EDIT);
                    ProcessVoucherHotelFinanceData(WorkflowInstance.DelVoucherHotelFinances, DataOP.DEL);

                    // Process transaction data for asset control detail item
                    ProcessVoucherHotelDetailData(WorkflowInstance.AddVoucherHotelDetails, DataOP.AddNew);
                    ProcessVoucherHotelDetailData(WorkflowInstance.EditVoucherHotelDetails, DataOP.EDIT);
                    ProcessVoucherHotelDetailData(WorkflowInstance.DelVoucherHotelDetails, DataOP.DEL);

                }
                else
                {
                    throw new Exception("Fixed asset disposal form has no request instance");
                }
            }
        }


        private void ProcessVoucherHotelFinanceData(IEnumerable<VoucherHotelFinance> voucherHotelFinances, DataOP op)
        {
            var voucherHotelDetailList = new List<VoucherHotelDetail>();
            if (voucherHotelFinances == null) return;
            
            foreach (var voucherHotelFinance in voucherHotelFinances)
            {
                voucherHotelFinance.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _voucherHotelFinanceRepository.Add(voucherHotelFinance);
                    WorkflowInstance.AddVoucherHotelDetails = voucherHotelDetailList;
                }
                else if (DataOP.EDIT == op)
                {
                    _voucherHotelFinanceRepository.Update(voucherHotelFinance);
                    WorkflowInstance.EditVoucherHotelDetails = voucherHotelDetailList;
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _voucherHotelFinanceRepository.GetById(voucherHotelFinance.Id);
                    if (removeRecord != null)
                    {
                        _voucherHotelFinanceRepository.Delete(removeRecord);
                        WorkflowInstance.DelVoucherHotelDetails = voucherHotelDetailList;
                    }
                }
            }
        }

        private void ProcessVoucherHotelDetailData(IEnumerable<VoucherHotelDetail> voucherHotelDetails, DataOP op)
        {
            if (voucherHotelDetails == null) return;

            foreach (var voucherHotelDetail in voucherHotelDetails)
            {
                voucherHotelDetail.RequestHeaderId = RequestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    _voucherHotelDetailRepository.Add(voucherHotelDetail);
                }
                else if (DataOP.EDIT == op)
                {
                    _voucherHotelDetailRepository.Update(voucherHotelDetail);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _voucherHotelDetailRepository.GetById(voucherHotelDetail.Id);
                    if (removeRecord != null)
                    {
                        _voucherHotelDetailRepository.Delete(removeRecord);
                    }
                }
            }
        }

        #endregion
    }
}
