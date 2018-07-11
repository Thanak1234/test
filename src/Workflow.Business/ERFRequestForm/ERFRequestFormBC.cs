

using Workflow.MSExchange;
/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.ReportingService.Report;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Attachment = Workflow.DataAcess.Repositories.Attachment;
using AttachmentEntity = Workflow.Domain.Entities.Attachment;
using Workflow.DataAcess.Repositories.HumanResource;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.MSExchange.Core;
using Workflow.Domain.Entities.HumanResource;
using Workflow.DataAcess;

namespace Workflow.Business.ERFRequestForm
{
    public class ERFRequestFormBC : AbstractRequestFormBC<ERFRequestWorkflowInstance, IERFFormDataProcessing>, IERFRequestFormBC
    {
        
        public const string HOD_APPROVAL = "HoD Approval";
        public const string HR_RECRUITMENT = "HR Recruitment";
        public const string DEPT_EXCOM = "Department Executive";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IRequisitionRepository _requisitionRepository = null;
        private Attachment.IRequisitionRepository _attachmentRepository = null;
        
        public ERFRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            
            _requisitionRepository = new RequisitionRepository(dbFactory);
            _attachmentRepository = new Attachment.RequisitionRepository(dbDocFactory);
            
            AddActivities(new ERFSubmissionActivity());
            AddActivities(new HoDApprovalActivity());
            AddActivities(new ERFHRRecuitmentActivity());
            AddActivities(new ERFExComActivity());
            AddActivities(new ERFReworkedActivity());

            AddActivities(new ERFEditFormAcitvity(() => {
                return CreateEmailData();
            }));
        }

        private IEmailData CreateEmailData()
        {
            IEmailData data = new DefaultEmailData();
            
            if (RequestHeader.Status != "Cancelled") {
                RequestHeader.Status = "Amended";
            }
            
            var body = "<font style='font - family:Verdana; font - size:16px;'>Dear {0},<br/><br/> " +
                        "ERF Requisition Request process has been " + RequestHeader.Status.ToLower() + " by: {3}.<br/><br/> " +
                        "Summary:<br/><br/> " +
                        "Ref: {1} <br/> " +
                        "Comment: {2} <br/><br/><br/> " +
                        "Thanks & Regards, <br/> " +
                        "Process Automation.<br/> " +
                        "</font > " +
                        "<b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> Internet E - mail Confidentiality Footer</span></b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> </span><o:p></o:p></p ><p class=MsoNormal style = 'mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><i><span style='font-family:\"ITC Stone Sans Std Medium\";color:navy'>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged.&nbsp; If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</span></I> " ; 

            data.Subject = String.Format("Notification (Ref:{0}) (" + RequestHeader.Status + " by: {1})", RequestHeader.Title, WorkflowInstance.fullName);
            var recepients = new List<string>();
            RequestHeaderRepository.Originator originator = requestHeaderRepository.GetRequestorEmail(RequestHeader.SubmittedBy);
            recepients.Add(originator.EMAIL);

            data.Body = String.Format(body, originator.DISPLAY_NAME,RequestHeader.Title, WorkflowInstance.Comment, WorkflowInstance.fullName);
            data.Recipients = recepients;
            data.Ccs = requestHeaderRepository.GetEmailsByRole(new string[] { "ERF HRR FRON 1000310 10001" });
            string reportPath = "/K2Report/HR/EmployeeRequisition";
            
            IGenericFormRpt genericForm = new GenericFormRpt();

            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, reportPath, ExportType.Pdf);
            var FileName = string.Concat("EmployeeRequisition_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            EmailFileAttachment fileAttachments = new EmailFileAttachment(FileName, buffer);

            data.AttachmentFiles.Add(fileAttachments);

            return data;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new ERFRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "Employee Requisition";
        }

        protected override string GetRequestCode()
        {
            return "ERF_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "ERF";
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || REWORKED.Equals(CurrentActivity().ActivityName))
            {
                return "ORIGINATOR";
            }
            else
            {
                return "REQUISITION";
            }
        }

        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {
            var uploadFiles = _attachmentRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);
            

            uploadFiles.Each(p => {
                p.ReadOnlyRecord = p.Status != getActivityforFileUpload();
            });
            return uploadFiles;

        }

        protected string GetReferenceNo()
        {
            ILookupRepository lookupReposity = new LookupRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));

            var lookup = lookupReposity.GetLookup("[HR].[EMPLOYEE_REQUISITION].[REFERENCE_NO]");
            
            if ((HR_RECRUITMENT.Equals(CurrentActivity().ActivityName)) && lookup != null)
            {
                int stockNum = Convert.ToInt32(lookup.Value);
                lookup.Value = (++stockNum).ToString();
                lookupReposity.Update(lookup);
                return string.Concat(DateTime.Now.ToString("yyyyMMdd"), "-", stockNum.ToString("0000"));
            }
            else
            {
                return null;
            }

        }

        protected override void LoadFormData()
        {
            var requisition = _requisitionRepository.GetByRequestHeader(RequestHeader.Id);
            if (requisition != null) {
                WorkflowInstance.Requisition = requisition;
            }
        }

        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if (WorkflowInstance.Requisition != null)
                {

                    Requisition Requisition = WorkflowInstance.Requisition;
                    bool isUpdate = false;
                    if (RequestHeader.Id > 0)
                    {
                        var currentRequisition = _requisitionRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentRequisition != null)
                        {
                            string referenceNo = GetReferenceNo();
                            if (!string.IsNullOrEmpty(referenceNo)) {
                                currentRequisition.ReferenceNo = referenceNo;
                                WorkflowInstance.Message = string.Format("Reference number is {0}.", referenceNo);
                            }
                            
                            currentRequisition.Position = Requisition.Position;
                            currentRequisition.ReportingTo = Requisition.ReportingTo;
                            currentRequisition.SalaryRange = Requisition.SalaryRange;
                            currentRequisition.RequestTypeId = Requisition.RequestTypeId;
                            currentRequisition.ShiftTypeId = Requisition.ShiftTypeId;
                            currentRequisition.LocationTypeId = Requisition.LocationTypeId;
                            currentRequisition.Private = Requisition.Private;
                            currentRequisition.RequisitionNumber = Requisition.RequisitionNumber;
                            currentRequisition.Justification = Requisition.Justification;
                            
                            _requisitionRepository.Update(currentRequisition);
                            Requisition.RequestHeaderId = RequestHeader.Id;
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.Requisition.RequestHeader = RequestHeader;
                        _requisitionRepository.Add(Requisition);
                    }
                }
                else
                {
                    throw new Exception("Requisition ERF have no instance");
                }
            }
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
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
                    var entity = new AttachmentEntity.Requisition()
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
                if (uploadFile != null && uploadFile.Status == getActivityforFileUpload()) {
                    _attachmentRepository.Delete(uploadFile);
                }
            }
        }
    }
}
