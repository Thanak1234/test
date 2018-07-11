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

namespace Workflow.Business.AdmCppRequestForm {
    public class AdmCppRequestFormBC : AbstractRequestFormBC<AdmCppRequestWorkflowInstance, IDataProcessing>, IAdmCppRequestFormBC {

        private IAdminCppRepository adminCppRepository;

        public AdmCppRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            adminCppRepository = new AdminCppRepository(dbFactory);
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
                }
                )
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.ADMCPP;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.AdmCpp = adminCppRepository.GetByRequestHeaderId(RequestHeader.Id);
        }

        protected override string Invalid() {
            var admcpp = WorkflowInstance.AdmCpp;
            string serialNo = admcpp.CPSN;
            if (!string.IsNullOrEmpty(serialNo) && adminCppRepository.ExistSerialNo(serialNo, RequestHeader.Id)) {
                return string.Format("Car Park S/N already exist."); ;
            }
            return base.Invalid();
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var oEntity = adminCppRepository.GetByRequestHeaderId(RequestHeader.Id);
                var admcpp = WorkflowInstance.AdmCpp;
                var activityName = CurrentActivity().ActivityName;                              

                if (oEntity == null) {
                    var nEntity = new ADMCPP();
                    nEntity.RequestHeaderId = RequestHeader.Id;
                    nEntity.Model = admcpp.Model;
                    nEntity.IssueDate = activityName.IsCaseInsensitiveEqual("Admin Issue") ? admcpp.IssueDate: null;
                    nEntity.PlateNo = admcpp.PlateNo;
                    nEntity.Remark = admcpp.Remark;
                    nEntity.Color = admcpp.Color;
                    nEntity.CPSN = admcpp.CPSN;
                    nEntity.YOP = admcpp.YOP;
                    adminCppRepository.Add(nEntity);
                } else {
                    oEntity.RequestHeaderId = RequestHeader.Id;
                    oEntity.Model = admcpp.Model;
                    oEntity.IssueDate = activityName.IsCaseInsensitiveEqual("Admin Issue") ? admcpp.IssueDate : oEntity.IssueDate;
                    oEntity.PlateNo = admcpp.PlateNo;
                    oEntity.Remark = admcpp.Remark;
                    oEntity.Color = admcpp.Color;
                    oEntity.CPSN = admcpp.CPSN;
                    oEntity.YOP = admcpp.YOP;
                    adminCppRepository.Update(oEntity);
                }
            } else {
                throw new Exception("Car Park Permit form have no instance");
            }
        }

        protected override string GetUserComment() {
            return "UserComment";
        }

        #endregion
    }
}
