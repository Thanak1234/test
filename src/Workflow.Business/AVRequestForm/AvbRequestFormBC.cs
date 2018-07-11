

/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BatchData;
using Workflow.MSExchange;
using Workflow.MSExchange.Core;
using Workflow.ReportingService.Report;

namespace Workflow.Business.AVRequestForm
{
    public class AvbRequestFormBC : AbstractRequestFormBC<AvbRequestWorkflowInstance, IAvbFormDataProcessing>, IAvbRequestFormBC
    {
        public const string AV_APPROVAL = "AV Approval";
        public const string AV_TECH = "AV Technician";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";
        


        private IAvbRequestItemRepository _avbRequestItemRepository = null;
        private IAvbJobDetailRepository _avbJobDetailRepository = null;
        private IAVJBRequestFilesRepository _avbRequestFilesRepository = null;

      
        public AvbRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _avbRequestItemRepository = new AvbRequestItemRepository(dbFactory);
            _avbJobDetailRepository = new AvbJobDetailRepository(dbFactory);
            _avbRequestFilesRepository = new AVJBRequestFilesRepository(dbDocFactory);

            AddActivities(new AvbRequestSubmissionActivity());
            AddActivities(new AvbDeptHoDApprovalActivity());
            AddActivities(new AvbApprovalActivity());
            AddActivities(new AvbTechnicianActivity());
            AddActivities(new AvbReworkedActivity());
   
            AddActivities(new AvbEditFormAcitvity(()=> {
                return CreateEmailData();
            }));

        }

        private IEmailData CreateEmailData()
        {
            IEmailData data = new DefaultEmailData();

            //AV Notification (Ref:AVJ - 000172) (Amended by: Chea Veasna)

            var body = "<font style='font - family:Verdana; font - size:16px;'>Dear {0},<br/><br/> " +
                        "AV Job Brief process has been amended by: {3}.<br/><br/> " +
                        "Summary:<br/><br/> " +
                        "Ref: {1} <br/> " +
                        "Comment: {2} <br/><br/><br/> " +
                        "Thanks & Regards, <br/> " +
                        "Process Automation.<br/> " +
                        "</font > " +
                        "<b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> Internet E - mail Confidentiality Footer</span></b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> </span><o:p></o:p></p ><p class=MsoNormal style = 'mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><i><span style='font-family:\"ITC Stone Sans Std Medium\";color:navy'>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged.&nbsp; If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</span></I> " ; 

            data.Subject = String.Format("Notification (Ref:{0}) (Amended by: {1})", RequestHeader.Title, WorkflowInstance.CurrentUser);
            var recepients = new List<string>();
            RequestHeaderRepository.Originator originator = requestHeaderRepository.GetRequestorEmail(RequestHeader.SubmittedBy);
            recepients.Add(originator.EMAIL);
            data.Body = String.Format(body, originator.DISPLAY_NAME, RequestHeader.Title, WorkflowInstance.Comment, WorkflowInstance.fullName);
            data.Recipients = recepients;
            string reportPath = "/K2Report/Event/AVJobBrief";
            
            IGenericFormRpt genericForm = new GenericFormRpt();

            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, reportPath, ExportType.Pdf);
            var FileName = string.Concat("AVJobBrief_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            EmailFileAttachment fileAttachments = new EmailFileAttachment(FileName, buffer);

            data.AttachmentFiles.Add(fileAttachments);

            return data;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new AvbRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "AV Job Brief";
        }

        protected override string GetRequestCode()
        {
            return "AVJ_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "AVJB";
        }

        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {
            var uploadFiles = _avbRequestFilesRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);

            uploadFiles.Each(p => {
                p.ReadOnlyRecord = p.Status != getActivityforFileUpload();
            });
            return uploadFiles;

        }

        protected override void LoadFormData()
        {
            WorkflowInstance.AvbJobHistory = _avbJobDetailRepository.GetByRequestHeader(RequestHeader.Id);
            WorkflowInstance.AvbRequestItems = _avbRequestItemRepository.GetByRequestHeader(RequestHeader.Id);
        }

        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void TakeFormAction()
        {
            if(CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData){
                if(WorkflowInstance.AvbJobHistory != null){


                    AvbJobHistory jobHistory = null;
                    if (RequestHeader.Id > 0)
                    {
                        jobHistory = _avbJobDetailRepository.GetByRequestHeader(RequestHeader.Id);
                    }

                    bool isUpdate = false;
                    if (jobHistory != null)
                    {
                        if (!jobHistory.Locaiton.Equals(WorkflowInstance.AvbJobHistory.Locaiton))
                        {
                            isUpdate = true;
                            jobHistory.Locaiton = WorkflowInstance.AvbJobHistory.Locaiton;
                        }
                        if (jobHistory.ProjectName != WorkflowInstance.AvbJobHistory.ProjectName)
                        {
                            isUpdate = true;
                            jobHistory.ProjectName = WorkflowInstance.AvbJobHistory.ProjectName;
                        }
                        /* Additional Field Start */
                        if (jobHistory.Other != WorkflowInstance.AvbJobHistory.Other)
                        {
                            isUpdate = true;
                            jobHistory.Other = WorkflowInstance.AvbJobHistory.Other;
                        }

                        if (jobHistory.ProjectBrief != WorkflowInstance.AvbJobHistory.ProjectBrief)
                        {
                            isUpdate = true;
                            jobHistory.ProjectBrief = WorkflowInstance.AvbJobHistory.ProjectBrief;
                        }
                        /* Additional Field End */

                        if (!jobHistory.SetupDate.Equals(WorkflowInstance.AvbJobHistory.SetupDate))
                        {
                            isUpdate = true;
                            jobHistory.SetupDate = WorkflowInstance.AvbJobHistory.SetupDate;
                        }

                        if (!jobHistory.ActualDate.Equals(WorkflowInstance.AvbJobHistory.ActualDate))
                        {
                            isUpdate = true;
                            jobHistory.ActualDate = WorkflowInstance.AvbJobHistory.ActualDate;
                        }
                    }

                    if (isUpdate)
                    {
                        _avbJobDetailRepository.Update(jobHistory);
                    }
                    
                    if(jobHistory == null && !isUpdate)
                    {
                        WorkflowInstance.AvbJobHistory.RequestHeader = RequestHeader;
                        _avbJobDetailRepository.Add(WorkflowInstance.AvbJobHistory);
                    }

                }else {
                    throw new Exception("Job history have no instance");
                }


                processData(WorkflowInstance.AddAvbRequestItems, DataOP.AddNew);
                processData(WorkflowInstance.EditAvbRequestItems, DataOP.EDIT);
                processData(WorkflowInstance.DelAvbRequestItems, DataOP.DEL);
            }
        }
        
        private void processData(IEnumerable<AvbRequestItem> items, DataOP op)
        {
            if (items == null) return;

            foreach(AvbRequestItem item in items)
            {

                item.RequestHeader = RequestHeader;
                item.RequestHeaderId = RequestHeader.Id;

                if (DataOP.AddNew == op)
                {
                    _avbRequestItemRepository.Add(item);
                }else if (DataOP.EDIT == op)
                {
                    var requestItem = _avbRequestItemRepository.GetById(item.Id);
                    requestItem.ItemId = item.ItemId;
                    requestItem.Comment = item.Comment;
                    requestItem.Quantity = item.Quantity;
                    _avbRequestItemRepository.Update(requestItem);
                }
                else if (DataOP.DEL == op)
                {
                    var requestItem = _avbRequestItemRepository.GetById(item.Id);
                    _avbRequestItemRepository.Delete(requestItem);
                }
            }

            
        }

        protected override void SaveAttachmentFiles()
        {
            AddAttachements();
            //UpdateAttachements();
            DelAttachements();
        }

        private void AddAttachements() {

            IList<string> serials = new List<string>();

            foreach(FileAttachement fileAttachement in WorkflowInstance.AddUploadFiles) {
                serials.Add(fileAttachement.Serial);
            }

            var uploadFiles = fileAttachementRepository.GetFileAttachementsBySerials(serials);

            if (uploadFiles == null || uploadFiles.Count() == 0) {
                return;
            }

            ///Add database

            foreach (FileTemp uploadFile in uploadFiles) {
                var entity = new AvbUploadFile() {
                    RequestHeaderId = RequestHeader.Id,
                    Name = uploadFile.Name,
                    Comment = uploadFile.Comment,
                    FileContent = uploadFile.FileContent,
                    Status = getActivityforFileUpload()
                };

                if(entity.Status == getActivityforFileUpload())
                {
                    _avbRequestFilesRepository.Add(entity);
                    fileAttachementRepository.Delete(uploadFile);
                }
            }
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || REWORKED.Equals(CurrentActivity().ActivityName))
            {
                return "REQUESTOR";
            }
            else
            {
                string activityName = CurrentActivity().ActivityName;
                return activityName.Replace(" ", "_").ToUpper();
            }
        }



        private void DelAttachements() {
            foreach (var attachment in WorkflowInstance.DelUploadFiles)
            {
                var uploadFile = _avbRequestFilesRepository.GetById(attachment.Id);
                if (uploadFile != null && uploadFile.Status == getActivityforFileUpload())
                {
                    _avbRequestFilesRepository.Delete(uploadFile);
                }
                    
            }
        }

        //private void UpdateAttachements() {
        //    foreach (AvbUploadFile uploadFile in WorkflowInstance.EditUploadFiles) {
        //        if (uploadFile.Status == getActivityforFileUpload())
        //        {
        //            _avbRequestFilesRepository.Update(uploadFile);
        //        }
                   
        //    }
        //}
    }
}
