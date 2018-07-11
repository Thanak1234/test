

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
using Workflow.DataAcess.Repositories.Reservation;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.MSExchange.Core;
using Workflow.Domain.Entities.Reservation;
using Workflow.DataObject.Reservation;
using Workflow.DataAcess.Repositories.HumanResource;
using Workflow.Domain.Entities.HR;

namespace Workflow.Business.ATTRequestForm
{
    public class ATTRequestFormBC : AbstractRequestFormBC<ATTRequestWorkflowInstance, IATTFormDataProcessing>, IATTRequestFormBC
    {
        
        public const string HOD_APPROVAL = "HoD Approval";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";

        public const string EXCO_APPROVAL = "EXCO Approval";
        
        public const string HR_REVIEW = "HR Review";
        public const string HR_APPROVAL = "HR Approval";

        public const string NAGA_TRAVEL = "NAGA Travel";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private ITravelDetailRepository _travelDetailRepository = null;
        private Attachment.ITravelDetailRepository _attachmentRepository = null;
        private IDestinationRepository _destinationRepository = null;
        private IFlightDetailRepository _flightDetailRepository = null;

        public ATTRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _travelDetailRepository = new TravelDetailRepository(dbFactory);
            _attachmentRepository = new Attachment.TravelDetailRepository(dbDocFactory);
            _destinationRepository = new DestinationRepository(dbFactory);
            _flightDetailRepository = new FlightDetailRepository(dbFactory);

            AddActivities(new ATTSubmissionActivity());
            AddActivities(new HoDApprovalActivity());
            AddActivities(new ATTReworkedActivity());

            AddActivities(new ATTEXCOApprovalActivity());
            AddActivities(new ATTHRReviewActivity());
            AddActivities(new ATTHRApprovalActivity());
            AddActivities(new ATTNAGATravelActivity());

            AddActivities(new ATTEditFormAcitvity(() => {
                return CreateEmailData();
            }));
        }

        private IEmailData CreateEmailData()
        {
            IEmailData data = new DefaultEmailData();

            string status = "amended";
            if (RequestHeader.Status == "Cancelled") {
                status = "cancelled";
            }
            
            var body = "<font style='font - family:Verdana; font - size:16px;'>Dear {0},<br/><br/> " +
                        "Authorisation To Travel Request process has been " + status + " by: {3}.<br/><br/> " +
                        "Summary:<br/><br/> " +
                        "Ref: {1} <br/> " +
                        "Comment: {2} <br/><br/><br/> " +
                        "Thanks & Regards, <br/> " +
                        "Process Automation.<br/> " +
                        "</font > " +
                        "<b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> Internet E - mail Confidentiality Footer</span></b><span style = 'font-family:\"ITC Stone Sans Std Medium\";color:navy'> </span><o:p></o:p></p ><p class=MsoNormal style = 'mso-margin-top-alt:auto;mso-margin-bottom-alt:auto'><i><span style='font-family:\"ITC Stone Sans Std Medium\";color:navy'>This E-mail is confidential and intended only for the use of the individual(s) or entity named above and may contain information that is privileged.&nbsp; If you are not the intended recipient, you are advised that any dissemination, distribution or copying of this E-mail is strictly prohibited. If you have received this E-mail in error, please notify us immediately by return E-mail or telephone and destroy the original message.</span></I> " ; 

            data.Subject = String.Format("Notification (Ref:{0}) (" + status  + " by: {1})", RequestHeader.Title, WorkflowInstance.fullName);
            var recepients = new List<string>();
            RequestHeaderRepository.Originator originator = requestHeaderRepository.GetRequestorEmail(RequestHeader.SubmittedBy);
            recepients.Add(originator.EMAIL);

            data.Body = String.Format(body, originator.DISPLAY_NAME, RequestHeader.Title, WorkflowInstance.Comment, WorkflowInstance.fullName);
            data.Recipients = recepients;
            
            data.Ccs = requestHeaderRepository.GetEmailNotification(RequestHeader.Id, "ATT_REQ", "FT,HRR",false);
            string reportPath = "/K2Report/HR/AuthorisationToTravel";
            
            
            IGenericFormRpt genericForm = new GenericFormRpt();

            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, reportPath, ExportType.Pdf);
            var FileName = string.Concat("AuthorisationToTravel_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            EmailFileAttachment fileAttachments = new EmailFileAttachment(FileName, buffer);

            data.AttachmentFiles.Add(fileAttachments);

            return data;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new ATTRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "Authorisation To Travel";
        }

        protected override string GetRequestCode()
        {
            return "ATT_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "ATT";
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || REWORKED.Equals(CurrentActivity().ActivityName))
            {
                return "ORIGINATOR";
            }            
            else
            {
                return  CurrentActivity().ActivityName.ToUpper();
            }
        }

        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {
            var uploadFiles = _attachmentRepository.GetMany(p => p.RequestHeaderId==RequestHeader.Id);
            
            uploadFiles.Each(p => {
                p.ReadOnlyRecord = p.Status != getActivityforFileUpload();
            });
            return uploadFiles;

        }

        protected override void LoadFormData()
        {
            var travelDetail = _travelDetailRepository.GetByRequestHeader(RequestHeader.Id);
            

            var destinations = _destinationRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);
            var flightDetails = _flightDetailRepository.GetMany(p => p.RequestHeaderId == RequestHeader.Id);
            if (travelDetail != null) {
                WorkflowInstance.TravelDetail = travelDetail;                
            }

            if (destinations != null)
            {
                WorkflowInstance.Destinations = destinations;
            }

            if (flightDetails != null)
            {
                WorkflowInstance.FlightDetails = flightDetails;
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
                if (WorkflowInstance.TravelDetail != null)
                {

                    TravelDetail td = WorkflowInstance.TravelDetail;
                    bool isUpdate = false;
                    if (RequestHeader.Id > 0)
                    {
                        var currentTravelDetail = _travelDetailRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentTravelDetail != null)
                        {
                            currentTravelDetail.ClassTravelEntitlement = td.ClassTravelEntitlement;
                            currentTravelDetail.ClassTravelRequest = td.ClassTravelRequest;
                            currentTravelDetail.ReasonException = td.ReasonException;
                            currentTravelDetail.PurposeTravel = td.PurposeTravel;
                            currentTravelDetail.EstimatedCostTicket = td.EstimatedCostTicket;
                            currentTravelDetail.costTicket = td.costTicket;
                            currentTravelDetail.ReasonTravel = td.ReasonTravel;
                            currentTravelDetail.NoRequestTaken = td.NoRequestTaken;
                            currentTravelDetail.ExtraCharge = td.ExtraCharge;
                            currentTravelDetail.Remark = td.Remark;

                            _travelDetailRepository.Update(currentTravelDetail);
                            td.RequestHeaderId = RequestHeader.Id;

                            if (!EDIT.Equals(CurrentActivity().ActivityName))
                            {
                                //update data field
                                _dataField.Add("Purpose of Travel", td.PurposeTravel);
                            }
                            

                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        td.RequestHeaderId = RequestHeader.Id;
                        _travelDetailRepository.Add(td);
                        _dataField.Add("Purpose of Travel", td.PurposeTravel);
                    }
                    
                    ProcessRequestDestinationData(WorkflowInstance.AddRequestDestinations, DataOP.AddNew);
                    ProcessRequestDestinationData(WorkflowInstance.DelRequestDestinations, DataOP.DEL);
                    ProcessRequestDestinationData(WorkflowInstance.EditRequestDestinations, DataOP.EDIT);
                                        
                    ProcessRequestFlightDetailData(WorkflowInstance.AddRequestFlightDetails, DataOP.AddNew);
                    ProcessRequestFlightDetailData(WorkflowInstance.DelRequestFlightDetails, DataOP.DEL);
                    ProcessRequestFlightDetailData(WorkflowInstance.EditRequestFlightDetails, DataOP.EDIT);

                }
                else
                {
                    throw new Exception("Authorisation To Travel have no instance");
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
                    var entity = new AttachmentEntity.TravelDetail()
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

        private void ProcessRequestDestinationData(IEnumerable<Destination> list, DataOP op)
        {
            if (list == null) return;

            foreach (Destination g in list)
            {   
                if (DataOP.AddNew == op)
                {
                    g.RequestHeaderId = RequestHeader.Id;
                    _destinationRepository.Add(g);
                }

                if (DataOP.DEL == op)
                {
                    var user = _destinationRepository.GetById(g.Id);
                    _destinationRepository.Delete(user);
                }

                if (DataOP.EDIT == op)
                {  
                    _destinationRepository.Update(g);
                }
            }

        }

        private void ProcessRequestFlightDetailData(IEnumerable<FlightDetail> list, DataOP op)
        {
            if (list == null) return;

            foreach (FlightDetail g in list)
            {
                if (DataOP.AddNew == op)
                {
                    g.RequestHeaderId = RequestHeader.Id;
                    _flightDetailRepository.Add(g);
                }

                if (DataOP.DEL == op)
                {
                    var user = _flightDetailRepository.GetById(g.Id);
                    _flightDetailRepository.Delete(user);
                }

                if (DataOP.EDIT == op)
                {
                    _flightDetailRepository.Update(g);
                }
            }

        }

    }
}
