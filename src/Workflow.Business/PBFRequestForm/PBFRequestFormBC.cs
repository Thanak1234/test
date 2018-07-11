/**
*@author : Yim Samaune
*/
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

namespace Workflow.Business.PBFRequestForm {
    public class PBFRequestFormBC : AbstractRequestFormBC<PBFRequestWorkflowInstance, IDataProcessing>, IPBFRequestFormBC
    {
        public const string MARCOM_TECHNICAL = "MARCOM Technical Briefing";
       
        private IProjectBriefRepository _projectBriefRepository = null;
        private ISpecificationRepository _specificationRepository = null;
        private Repositories.IAttachmentRepository _attachmentRepository = null;
        
        public PBFRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _projectBriefRepository = new ProjectBriefRepository(dbFactory);
            _specificationRepository = new SpecificationRepository(dbFactory);
            _attachmentRepository = new Repositories.AttachmentRepository(dbDocFactory);
        }

        protected override void InitActivityConfiguration() 
        {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(() => {
                return CreateEmailData("MODIFICATION");
            })); 
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.PB;
        }

        #region Load/Save Form
        protected string GetProjectReference(bool hasRef) {
            ILookupRepository lookupReposity = new LookupRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));

            var lookup = lookupReposity.GetLookup("[EVENT].[PROJECT_BRIEF].[PROJECT_REFERENCE]");

            if ((MARCOM_TECHNICAL.Equals(CurrentActivity().ActivityName)) && lookup != null && !hasRef) {
                int stockNum = Convert.ToInt32(lookup.Value);
                lookup.Value = (++stockNum).ToString();
                lookupReposity.Update(lookup);
                return string.Concat(DateTime.Now.ToString("yy"), stockNum.ToString("0000"));
            } else {
                return null;
            }
        }

        protected override void LoadFormData() {
            var projectBrief = _projectBriefRepository.GetByRequestHeader(RequestHeader.Id);
            if (projectBrief != null) {
                WorkflowInstance.ProjectBrief = projectBrief;
                WorkflowInstance.Specifications = _specificationRepository.GetByProjectId(WorkflowInstance.ProjectBrief.Id);
            }
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                bool bGenerate = (CurrentActivity().CurrAction.ActionName == "Submitted") && CurrentActivity().ActivityName == MARCOM_TECHNICAL;
                if (CurrentActivity().ActivityName == REQUESTOR_REWORKED) {
                    WorkflowInstance.Comment = string.Empty;
                }

                if (WorkflowInstance.ProjectBrief != null) {

                    var project = WorkflowInstance.ProjectBrief;
                    bool isUpdate = false;
                    string referenceNo = null;

                    if (RequestHeader.Id > 0) {
                        var currentProject = _projectBriefRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentProject != null) {
                            referenceNo = GetProjectReference(!string.IsNullOrEmpty(currentProject.ProjectReference));
                            currentProject.ProjectName = project.ProjectName;
                            currentProject.BusinessUnit = project.BusinessUnit;
                            currentProject.ProjectLead = project.ProjectLead;
                            currentProject.AccountManager = project.AccountManager;
                            currentProject.Budget = project.Budget;
                            currentProject.BillingInfo = project.BillingInfo;
                            currentProject.SubmissionDate = project.SubmissionDate;
                            currentProject.RequiredDate = project.RequiredDate;
                            currentProject.ActualEventDate = project.ActualEventDate;
                            currentProject.Introduction = project.Introduction;
                            currentProject.TargetMarket = project.TargetMarket;
                            currentProject.Usage = project.Usage;
                            currentProject.Briefing = project.Briefing;
                            currentProject.DesignDuration = project.DesignDuration;
                            currentProject.ProductDuration = project.ProductDuration;
                            currentProject.Dateline = project.Dateline;
                            currentProject.BrainStorm = project.BrainStorm;
                            currentProject.ProjectStart = project.ProjectStart;
                            currentProject.FirstRevision = project.FirstRevision;
                            currentProject.SecondRevision = project.SecondRevision;
                            currentProject.FinalApproval = project.FinalApproval;
                            currentProject.Completion = project.Completion;
                            currentProject.DraftComment = IsSaveDraft() ? project.DraftComment : null;

                            if (!string.IsNullOrEmpty(referenceNo) && bGenerate) {
                                currentProject.ProjectReference = referenceNo;
                                project.ProjectReference = referenceNo;
                            }

                            _projectBriefRepository.Update(currentProject);
                            project.RequestHeaderId = RequestHeader.Id;
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate) {
                        WorkflowInstance.ProjectBrief.RequestHeader = RequestHeader;
                        _projectBriefRepository.Add(project);
                    }

                    // Process transaction data for request items
                    processData(WorkflowInstance.AddSpecifications, DataOP.AddNew, project);
                    processData(WorkflowInstance.EditSpecifications, DataOP.EDIT, project);
                    processData(WorkflowInstance.DelSpecifications, DataOP.DEL, project);

                    // Item reference setup and classify item's category
                    if (bGenerate) {
                        WorkflowInstance.Message = string.Format("Project reference number is {0}.", project.ProjectReference);
                    }
                } else {
                    throw new Exception("Project brief form have no instance");
                }
            }
        }

        private void processData(IEnumerable<Specification> specifications, DataOP op, ProjectBrief project) {
            if (specifications == null) return;



            foreach (var specification in specifications) {


                specification.ProjectBriefId = project.Id;
                if (DataOP.AddNew == op) {
                    _specificationRepository.Add(specification);
                } else if (DataOP.EDIT == op) {
                    specification.Item = null;
                    _specificationRepository.Update(specification);
                } else if (DataOP.DEL == op) {
                    var requestItem = _specificationRepository.GetById(specification.Id);
                    _specificationRepository.Delete(requestItem);
                }
            }
        } 
        #endregion

        #region Method - Attachment File
        protected override IEnumerable<FileAttachement> GetUploadFiles() {
            var uploadFiles = _attachmentRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);


            uploadFiles.Each(p => {
                p.ReadOnlyRecord = p.Status != getActivityforFileUpload();
            });
            return uploadFiles;

        }

        protected override void SaveAttachmentFiles() {
            AddAttachements();
            DelAttachements();
        }

        private void AddAttachements() {

            IList<string> serials = new List<string>();

            foreach (FileAttachement fileAttachement in WorkflowInstance.AddUploadFiles) {
                serials.Add(fileAttachement.Serial);
            }

            var uploadFiles = fileAttachementRepository.GetFileAttachementsBySerials(serials);

            if (uploadFiles != null) {
                foreach (FileTemp uploadFile in uploadFiles) {
                    var entity = new AttachmentEntity.ProjectBrief() {
                        RequestHeaderId = RequestHeader.Id,
                        Name = uploadFile.Name,
                        Comment = uploadFile.Comment,
                        FileContent = uploadFile.FileContent,
                        Status = getActivityforFileUpload()
                    };
                    _attachmentRepository.Add(entity);
                    fileAttachementRepository.Delete(uploadFile);
                }
            }
        }

        private void DelAttachements() {
            foreach (var attachment in WorkflowInstance.DelUploadFiles) {
                var uploadFile = _attachmentRepository.GetById(attachment.Id);
                if (uploadFile != null && uploadFile.Status == getActivityforFileUpload()) {
                    _attachmentRepository.Delete(uploadFile);
                }
            }
        } 
        #endregion
    }
}
