/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EOMRequestForm;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.EOMRequestForm
{

    public class EOMRequestFormBC : AbstractRequestFormBC<EOMRequestWorkflowInstance, IEOMFormDataProcessing>, IEOMRequestFormBC {
        public const string TD_REVIEW = "T&D Review";
        public const string TD_APPROVAL = "T&D Approval";
        public const string PAYROLL = "Payroll";
        public const string CREATIVE = "Creative";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";

        private const string REVIEWED_ACTION = "Reviewed";
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IEOMRequestInformationRepository _EOMRequestInformationRepository = null;
        private IEOMRequestFilesRepository _EOMRequestFilesRepository = null;

      
        public EOMRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _EOMRequestInformationRepository = new EOMRequestInformationRepository(dbFactory);
            _EOMRequestFilesRepository = new EOMRequestFilesRepository(dbDocFactory);
            AddActivities(new EOMRequestSubmissionActivity());
            AddActivities(new EOMDeptHoDApprovalActivity());
            AddActivities(new EOMReworkedActivity());
            AddActivities(new EOMTDReviewActivity());
            AddActivities(new EOMPayrolllActivity());
            AddActivities(new EOMCreativeActivity());
            AddActivities(new EOMTDApprovalActivity());
            AddActivities(new EOMEditFormAcitvity(() => {
                return CreateEmailData("HOD,T&D APPROVAL");
            }));
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new EOMRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "EOM";
        }

        protected override string GetRequestCode()
        {
            return "EOM_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "EOM";
        }

        protected override void LoadFormData()
        {
            WorkflowInstance.EOMInfo = _EOMRequestInformationRepository.GetByRequestHeaderId(RequestHeader.Id);
        }

        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void TakeFormAction()
        {
            if(CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData){
                var oEntity = _EOMRequestInformationRepository.GetByRequestHeaderId(RequestHeader.Id);
                if(oEntity == null) {
                    var info = WorkflowInstance.EOMInfo;
                    var nEntity = new EOMDetail();
                    nEntity.RequestHeaderId = RequestHeader.Id;

                    nEntity.Aprd = info.Aprd;
                    nEntity.Cfie = info.Cfie;
                    nEntity.Lc = info.Lc;
                    nEntity.Psdm = info.Psdm;
                    nEntity.Tmp = info.Tmp;

                    nEntity.Month = info.Month;
                    nEntity.Reason = info.Reason;
                    
                    nEntity.TotalScore = info.TotalScore;
                    nEntity.Cash = 0;
                    nEntity.Voucher = 0;
                    _EOMRequestInformationRepository.Add(nEntity);
                } else {
                    var info = WorkflowInstance.EOMInfo;
                    oEntity.RequestHeaderId = RequestHeader.Id;

                    oEntity.Aprd = info.Aprd;
                    oEntity.Cfie = info.Cfie;
                    oEntity.Lc = info.Lc;
                    oEntity.Psdm = info.Psdm;
                    oEntity.Tmp = info.Tmp;

                    oEntity.Month = info.Month;
                    oEntity.Reason = info.Reason;
                    oEntity.TotalScore = info.TotalScore;
                    
                    if (CurrentActivity().ActivityName.IsCaseInsensitiveEqual("T&D REVIEW") && CurrentActivity().CurrAction.ActionName.IsCaseInsensitiveEqual("Reviewed")) {
                        oEntity.Cash = info.Cash == null ? 0 : info.Cash;
                        oEntity.Voucher = info.Voucher == null ? 0 : info.Voucher;
                    }

                    _EOMRequestInformationRepository.Update(oEntity);

                    if(CurrentActivity().ActivityName.IsCaseInsensitiveEqual("T&D REVIEW") && CurrentActivity().CurrAction.ActionName.IsCaseInsensitiveEqual("Reviewed")) {                        
                        _dataField.Add("Cash", oEntity.Cash);
                        _dataField.Add("Voucher", oEntity.Voucher);
                    }
                }                
            }
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }


        protected override string GetUserComment()
        {
            return "UserComment";
        }
    }
}
