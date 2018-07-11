using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

using BCJ = Workflow.Domain.Entities.BCJ;
using BCJRepo = Workflow.DataAcess.Repositories.BCJ;
using Workflow.Domain.Entities.BatchData;
using Workflow.Domain.Entities.Attachement;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjRequestFormBC : AbstractRequestFormBC<BcjRequestWorkflowInstance, IBcjFormDataProcessing>, IBcjRequestFormBC
    {
        public const string Line_Department = "Line of Department";
        public const string Department_Executive = "Department Executive";
        public const string Level1Approval = "Level 1 Approval";
        public const string Level2Approval = "Level 2 Approval";
        public const string REWORKED = "Requestor Rework";
        public const string Head_Department = "Department Head";
        public const string CFO= "CFO";
        public const string CFO_DyCFO = "CFO DyCFO";
        public const string CapexCommittee = "Capex Committee";
        public const string Finance_Group = "Finance Group";
        public const string Purchasing = "Purchasing";
        public const string EDIT = "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private BCJRepo.IRequestItemRepository _requestItemRepository = null;
        private BCJRepo.IProjectDetailRepository _projectDetailRepository = null;
        private BCJRepo.IAnalysisItemRepository _analysisItemRepository = null;
        private BCJRepo.IPurchaseOrderRepository _purchaseOrderRepository = null;
        private BCJRepo.IAttachmentRepository _attachmentRepository = null;

        public BcjRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _requestItemRepository = new BCJRepo.RequestItemRepository(dbFactory);
            _projectDetailRepository = new BCJRepo.ProjectDetailRepository(dbFactory);
            _analysisItemRepository = new BCJRepo.AnalysisItemRepository(dbFactory);
            _attachmentRepository = new BCJRepo.AttachmentRepository(dbDocFactory);
            _purchaseOrderRepository = new BCJRepo.PurchaseOrderRepository(dbFactory);

            AddActivities(new BcjDraftActivity());
            AddActivities(new BcjRequestSubmissionActivity());
            AddActivities(new BcjHoDApprovalActivity());
            // deprecated soon
            AddActivities(new BcjLoDApprovalActivity());
            AddActivities(new BcjExComApprovalActivity());
            // replace by bellow activities
            AddActivities(new Level1ApprovalAct());
            AddActivities(new Level2ApprovalAct());

            AddActivities(new BcjCFOApprovalActivity());
            AddActivities(new BcjDyCFOApprovalActivity());
            AddActivities(new CapexCommitteeAct());
            AddActivities(new BcjFinanceApprovalActivity());
            AddActivities(new BcjPurchasingActivity());
            AddActivities(new BcjReworkedActivity());

            AddActivities(new BcjEditFormAcitvity(() => {
                return CreateEmailData("MODIFICATION,FINANCE_PO");
            }));
        }

        private bool ExistsLastActivity(string activityName) {
            var activities = RequestHeader.ActivityHistories;
            var activity = RequestHeader.ActivityHistories.Where(t => t.Activity == activityName);
            return (activity.Count() > 0);
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new BcjRequestWorkflowInstance();
            }
        }
        
        protected override string getFullProccessName()
        {
            return _processFolderName + "Business Case Justification";
        }

        protected override string GetRequestCode()
        {
            return "BCJ_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "BCJ";
        }

        protected override void LoadFormData()
        {
            WorkflowInstance.ProjectDetail = _projectDetailRepository.GetByRequestHeader(RequestHeader.Id);
            if (WorkflowInstance.ProjectDetail != null) {
                WorkflowInstance.RequestItems = _requestItemRepository.GetByProjectDetail(WorkflowInstance.ProjectDetail.Id);
                WorkflowInstance.AnalysisItems = _analysisItemRepository.GetByProjectDetail(WorkflowInstance.ProjectDetail.Id);
                WorkflowInstance.PurchaseOrderItems = _purchaseOrderRepository.GetByRequestHeader(RequestHeader.Id);
            }
        }

        protected override void TakeFormAction()
        {
            if(CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData){
                if(WorkflowInstance.ProjectDetail != null){
                    
                    BCJ.ProjectDetail projectDetail = WorkflowInstance.ProjectDetail;
                    bool isUpdate = false;
                    if (RequestHeader.Id > 0)
                    {
                        var project = _projectDetailRepository.GetByRequestHeader(RequestHeader.Id);
                        if (project != null)
                        {
                            project.CapexCategoryId = projectDetail.CapexCategoryId;
                            project.ProjectName = projectDetail.ProjectName;
                            project.Reference = projectDetail.Reference;
                            project.CoporationBranch = projectDetail.CoporationBranch;
                            project.WhatToDo = projectDetail.WhatToDo;
                            project.WhyToDo = projectDetail.WhyToDo;
                            project.TotalAmount = projectDetail.TotalAmount;
                            project.Arrangement = projectDetail.Arrangement;
                            project.EstimateCapex = projectDetail.EstimateCapex;
                            project.IncrementalNetContribution = projectDetail.IncrementalNetContribution;
                            project.IncrementalNetEbita = projectDetail.IncrementalNetEbita;
                            project.PaybackYear = projectDetail.PaybackYear;
                            project.OutlineBenefit = projectDetail.OutlineBenefit;
                            project.Commencement = projectDetail.Commencement;
                            project.Completion = projectDetail.Completion;
                            project.Alternative = projectDetail.Alternative;
                            project.OutlineRisk = projectDetail.OutlineRisk;
                            project.CapitalRequired = projectDetail.CapitalRequired;

                            _projectDetailRepository.Update(project);
                            isUpdate = true;
                        }
                    }
                    
                    if (!isUpdate)
                    {
                        WorkflowInstance.ProjectDetail.RequestHeader = RequestHeader;
                        _projectDetailRepository.Add(projectDetail);
                        _dataField.Add("BCJId", projectDetail.Id);
                    }


                    // Process transaction data for request items
                    processData(WorkflowInstance.AddRequestItems, DataOP.AddNew, projectDetail);
                    processData(WorkflowInstance.EditRequestItems, DataOP.EDIT, projectDetail);
                    processData(WorkflowInstance.DelRequestItems, DataOP.DEL, projectDetail);

                    processData(WorkflowInstance.AddAnalysisItems, DataOP.AddNew, projectDetail);
                    processData(WorkflowInstance.EditAnalysisItems, DataOP.EDIT, projectDetail);
                    processData(WorkflowInstance.DelAnalysisItems, DataOP.DEL, projectDetail);
                    
                    // PO Processing
                    processData(WorkflowInstance.AddPurchaseOrderItems, DataOP.AddNew, RequestHeader.Id);
                    processData(WorkflowInstance.EditPurchaseOrderItems, DataOP.EDIT, RequestHeader.Id);
                    processData(WorkflowInstance.DelPurchaseOrderItems, DataOP.DEL, RequestHeader.Id);
                    
                    //_dataField.Add("CorpBranch", projectDetail.CoporationBranch);
                }
                else {
                    throw new Exception("Project detail have no instance");
                }   
            }
        }
        
        private void processData(IEnumerable<BCJ.BcjRequestItem> items, DataOP op, BCJ.ProjectDetail projectDetail)
        {
            if (items == null) return;

            foreach(var item in items)
            {
                item.ProjectDetailId = projectDetail.Id;
                item.Total = decimal.Multiply(Convert.ToDecimal(item.Quantity), item.UnitPrice);

                if (DataOP.AddNew == op)
                {
                    _requestItemRepository.Add(item);
                }
                else if (DataOP.EDIT == op)
                {
                    _requestItemRepository.Update(item);
                }
                else if (DataOP.DEL == op)
                {
                    var requestItem = _requestItemRepository.GetById(item.Id);
                    _requestItemRepository.Delete(requestItem);
                }
            } 
        }

        private void processData(IEnumerable<BCJ.PurchaseOrder> items, DataOP op, int requestHeaderId)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                item.RequestHeaderId = requestHeaderId;
                
                if (DataOP.AddNew == op)
                {
                    _purchaseOrderRepository.Add(item);
                }
                else if (DataOP.EDIT == op)
                {
                    _purchaseOrderRepository.Update(item);
                }
                else if (DataOP.DEL == op)
                {
                    var requestItem = _purchaseOrderRepository.GetById(item.Id);
                    if (requestItem != null) {
                        _purchaseOrderRepository.Delete(requestItem);
                    }
                }
            }
        }

        private void processData(IEnumerable<BCJ.AnalysisItem> items, DataOP op, BCJ.ProjectDetail projectDetail)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                item.ProjectDetailId = projectDetail.Id;
                
                item.Total = decimal.Multiply(Convert.ToDecimal(item.Quantity), item.Revenue);

                if (DataOP.AddNew == op)
                {
                    _analysisItemRepository.Add(item);
                }
                else if (DataOP.EDIT == op)
                {
                    _analysisItemRepository.Update(item);
                }
                else if (DataOP.DEL == op)
                {
                    var requestItem = _analysisItemRepository.GetById(item.Id);
                    _analysisItemRepository.Delete(requestItem);
                }
            }
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }

        /* UPLOAD MODULE */
        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {
            var uploadFiles = _attachmentRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);
            uploadFiles.Each(p => {
                p.ReadOnlyRecord = p.Status != getActivityforFileUpload();
            });
            return uploadFiles;
        }

        protected override void SaveAttachmentFiles()
        {
            AddAttachements();
            DelAttachements();
        }

        private void AddAttachements()
        {
            IList<string> serials = new List<string>();
            foreach (FileAttachement fileAttachement in WorkflowInstance.AddUploadFiles)
            {
                serials.Add(fileAttachement.Serial);
            }
            var uploadFiles = fileAttachementRepository.GetFileAttachementsBySerials(serials);
            if (uploadFiles != null)
            {
                foreach (FileTemp uploadFile in uploadFiles)
                {
                    var entity = new BCJ.BcjAttachment()
                    {
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
        private void DelAttachements()
        {
            foreach (var attachment in WorkflowInstance.DelUploadFiles)
            {
                var uploadFile = _attachmentRepository.GetById(attachment.Id);
                if (uploadFile != null && uploadFile.Status == getActivityforFileUpload())
                {
                    _attachmentRepository.Delete(uploadFile);
                }
            }
        }
    }
}
