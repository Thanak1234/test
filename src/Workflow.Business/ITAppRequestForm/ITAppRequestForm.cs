using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using AttachmentEntity = Workflow.Domain.Entities.Attachment;
using Workflow.DataAcess.Repositories.PBF;
using Repositories = Workflow.DataAcess.Repositories.PBF;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.Domain.Entities.PBF;
using Workflow.DataAcess;
using Workflow.DataAcess.Repositories.AdmCppForm;
using Workflow.Domain.Entities.ADMCPPForm;
using Workflow.DataAcess.Repositories.ITApp;
using Workflow.Domain.Entities.Core.ITApp;

namespace Workflow.Business.ITAppRequestForm {

    public class ITAppRequestForm : AbstractRequestFormBC<ITAppWorkflowInstance, IDataProcessing>, IITAppRequestForm {

        private IItappProjectInitRepository _itappProjectInitRepository;
        private IItappProjectDevRepository _itappProjectDevRepository;
        private IItappProjectApprovalRepository _itappProjectApprovalRepository;

        public ITAppRequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            _itappProjectInitRepository = new ItappProjectInitRepository(dbFactory);
            _itappProjectDevRepository = new ItappProjectDevRepository(dbFactory);
            _itappProjectApprovalRepository = new ItappProjectApprovalRepository(dbFactory);
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
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.ITSWD;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.ProjectInit = _itappProjectInitRepository.Get(x => x.RequestHeaderId == RequestHeader.Id);
            WorkflowInstance.ProjectDev = _itappProjectDevRepository.Get(x => x.RequestHeaderId == RequestHeader.Id && x.IsQA == false);
            WorkflowInstance.ProjectQA = _itappProjectDevRepository.Get(x => x.RequestHeaderId == RequestHeader.Id && x.IsQA == true);
            WorkflowInstance.ProjectApproval = _itappProjectApprovalRepository.Get(x => x.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                string actName = CurrentActivity().ActivityName;
                string action = CurrentActivity().CurrAction.ActionName;

                var entity = _itappProjectInitRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if (entity == null) {
                    var projectInit = WorkflowInstance.ProjectInit;
                    entity = new ItappProjectInit();
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.PriorityConsideration = projectInit.PriorityConsideration ?? 0;
                    entity.ProposedChange = projectInit.ProposedChange;
                    entity.Application = projectInit.Application;
                    entity.BenefitCS = projectInit.BenefitCS;
                    entity.BenefitIIS = projectInit.BenefitIIS;
                    entity.BenefitOther = projectInit.BenefitOther;
                    entity.Description = projectInit.Description;
                    entity.BenefitRM = projectInit.BenefitRM;
                    _itappProjectInitRepository.Add(entity);
                } else {
                    var projectInit = WorkflowInstance.ProjectInit;
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.PriorityConsideration = projectInit.PriorityConsideration ?? 0;
                    entity.ProposedChange = projectInit.ProposedChange;
                    entity.Application = projectInit.Application;
                    entity.BenefitCS = projectInit.BenefitCS;
                    entity.BenefitIIS = projectInit.BenefitIIS;
                    entity.BenefitOther = projectInit.BenefitOther;
                    entity.Description = projectInit.Description;
                    entity.BenefitRM = projectInit.BenefitRM;
                    _itappProjectInitRepository.Update(entity);

                    if (actName.IsCaseInsensitiveEqual("IT App Manager") && action.IsCaseInsensitiveEqual("Approved")) {
                        var projectApproval = WorkflowInstance.ProjectApproval;
                        var oEntity = _itappProjectApprovalRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
                        if (oEntity == null) {
                            oEntity = new ItappProjectApproval();
                            oEntity.DeliveryDate = projectApproval.DeliveryDate;
                            oEntity.GoLiveDate = projectApproval.GoLiveDate;
                            oEntity.Hc = projectApproval.Hc ?? 0;
                            oEntity.Rawm = projectApproval.Rawm;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.Rsim = projectApproval.Rsim;
                            oEntity.Scmd = projectApproval.Scmd ?? 0;
                            oEntity.Slc = projectApproval.Slc ?? 0;
                            _itappProjectApprovalRepository.Add(oEntity);
                        } else {
                            oEntity.DeliveryDate = projectApproval.DeliveryDate;
                            oEntity.GoLiveDate = projectApproval.GoLiveDate;
                            oEntity.Hc = projectApproval.Hc ?? 0;
                            oEntity.Rawm = projectApproval.Rawm;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.Rsim = projectApproval.Rsim;
                            oEntity.Scmd = projectApproval.Scmd ?? 0;
                            oEntity.Slc = projectApproval.Slc ?? 0;
                            _itappProjectApprovalRepository.Update(oEntity);
                        }
                    }

                    if(actName.IsCaseInsensitiveEqual("Input Go Live Date")) {
                        var projectApproval = WorkflowInstance.ProjectApproval;
                        var oEntity = _itappProjectApprovalRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
                        if(oEntity != null) {
                            oEntity.GoLiveDate = projectApproval.GoLiveDate;
                            _itappProjectApprovalRepository.Update(oEntity);
                        }
                    }

                    if (actName.IsCaseInsensitiveEqual("Development")) {
                        var projectDev = WorkflowInstance.ProjectDev;
                        var oEntity = _itappProjectDevRepository.Get(p => p.RequestHeaderId == RequestHeader.Id && p.IsQA == false);
                        if (oEntity == null) {
                            oEntity = new ItappProjectDev();
                            oEntity.IsQA = false;
                            oEntity.Remark = projectDev.Remark;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.StartDate = projectDev.StartDate;
                            oEntity.EndDate = projectDev.EndDate;
                            _itappProjectDevRepository.Add(oEntity);
                        } else {
                            oEntity.IsQA = false;
                            oEntity.Remark = projectDev.Remark;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.StartDate = projectDev.StartDate;
                            oEntity.EndDate = projectDev.EndDate;
                            _itappProjectDevRepository.Update(oEntity);
                        }
                    }

                    if (actName.IsCaseInsensitiveEqual("IT App Manager QA") && action.IsCaseInsensitiveEqual("Accepted")) {
                        var projectQA = WorkflowInstance.ProjectQA;
                        var oEntity = _itappProjectDevRepository.Get(p => p.RequestHeaderId == RequestHeader.Id && p.IsQA == true);
                        if (oEntity == null) {
                            oEntity = new ItappProjectDev();
                            oEntity.IsQA = true;
                            oEntity.Remark = projectQA.Remark;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.StartDate = projectQA.StartDate;
                            oEntity.EndDate = projectQA.EndDate;
                            _itappProjectDevRepository.Add(oEntity);
                        } else {
                            oEntity.IsQA = true;
                            oEntity.Remark = projectQA.Remark;
                            oEntity.RequestHeaderId = RequestHeader.Id;
                            oEntity.StartDate = projectQA.StartDate;
                            oEntity.EndDate = projectQA.EndDate;
                            _itappProjectDevRepository.Update(oEntity);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
