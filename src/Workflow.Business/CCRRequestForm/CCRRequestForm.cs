using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using System.Collections.Generic;
using Workflow.Domain.Entities.VoucherRequest;
using System;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories;
using Workflow.Domain.Entities.Core.CCR;

namespace Workflow.Business.CCRRequestForm {

    public class CCRRequestForm : AbstractRequestFormBC<CCRWorkflowInstance, IDataProcessing>, ICCRRequestForm {

        private IContractDraftRepository contractDraftRepo;

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public CCRRequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            contractDraftRepo = new ContractDraftRepository(dbFactory);
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
            return PROCESSCODE.CCR;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.ContractDraft = contractDraftRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var ContractDraft = WorkflowInstance.ContractDraft;
                var oEntity = contractDraftRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if(oEntity == null) {
                    var nEntity = WorkflowInstance.ContractDraft;
                    nEntity.RequestHeaderId = RequestHeader.Id;
                    contractDraftRepo.Add(nEntity);
                } else {
                    oEntity.RequestHeaderId = RequestHeader.Id;
                    oEntity.Name = ContractDraft.Name;
                    oEntity.Vat = ContractDraft.Vat;
                    oEntity.Address = ContractDraft.Address;
                    oEntity.Email = ContractDraft.Email;
                    oEntity.RegistrationNo = ContractDraft.RegistrationNo;
                    oEntity.Phone = ContractDraft.Phone;
                    oEntity.ContactName = ContractDraft.ContactName;
                    oEntity.Position = ContractDraft.Position;
                    oEntity.IssueedBy = ContractDraft.IssueedBy;
                    oEntity.Term = ContractDraft.Term;
                    oEntity.StartDate = ContractDraft.StartDate;
                    oEntity.InclusiveTax = ContractDraft.InclusiveTax;
                    oEntity.EndingDate = ContractDraft.EndingDate;
                    oEntity.PaymentTerm = ContractDraft.PaymentTerm;
                    oEntity.AtSa = ContractDraft.AtSa;
                    oEntity.AtSpa = ContractDraft.AtSpa;
                    oEntity.AtLa = ContractDraft.AtLa;
                    oEntity.AtCa = ContractDraft.AtCa;
                    oEntity.AtLea = ContractDraft.AtLea;
                    oEntity.AtEa = ContractDraft.AtEa;
                    oEntity.AtOther = ContractDraft.AtOther;
                    oEntity.StatusNew = ContractDraft.StatusNew;
                    oEntity.StatusRenewal = ContractDraft.StatusRenewal;
                    oEntity.StatusReplacement = ContractDraft.StatusReplacement;
                    oEntity.StatusAddendum = ContractDraft.StatusAddendum;
                    oEntity.Vis = ContractDraft.Vis;
                    oEntity.IsCapex = ContractDraft.IsCapex;
                    oEntity.BcjNumber = ContractDraft.BcjNumber;
                    oEntity.ActA = ContractDraft.ActA;
                    oEntity.ActB = ContractDraft.ActB;
                    oEntity.ActC = ContractDraft.ActC;
                    oEntity.ActD = ContractDraft.ActD;
                    contractDraftRepo.Update(oEntity);
                }
            }
        }

        #endregion
    }
}
