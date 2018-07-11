

using Workflow.MSExchange;
/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.ReportingService.Report;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Reservation;
using Repositories = Workflow.DataAcess.Repositories.Reservation;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.MSExchange.Core;
using Workflow.Domain.Entities.Reservation;
using Workflow.DataObject.Reservation;

namespace Workflow.Business.FnFRequestForm
{
    public class FnFRequestFormBC : AbstractRequestFormBC<FnFRequestWorkflowInstance, IFnFFormDataProcessing>, IFnFRequestFormBC
    {
        
        public const string HOD_APPROVAL = "HoD Approval";
        public const string FNF_REVIEW = "Reservation Review";
        public const string FNF_APPROVAL = "Reservation Approval";
        public const string FNF_FINAL_REVIEW = "Final Reservation Review";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IBookingRepository _bookingRepository = null;
        private Repositories.IAttachmentRepository _attachmentRepository = null;
        
        public FnFRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            
            _bookingRepository = new BookingRepository(dbFactory);
            _attachmentRepository = new Repositories.AttachmentRepository(dbDocFactory);
            
            AddActivities(new FnFSubmissionActivity());
            AddActivities(new HoDApprovalActivity());
            AddActivities(new FnFReviewActivity());
            AddActivities(new FnFApprovalActivity());
            AddActivities(new FnFFinalReviewActivity());
            AddActivities(new FnFReworkedActivity());

            AddActivities(new FnFEditFormAcitvity(() => {
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
                        "FnF Booking Request process has been " + status + " by: {3}.<br/><br/> " +
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

            data.Body = String.Format(body, originator.DISPLAY_NAME,RequestHeader.Title, WorkflowInstance.Comment, WorkflowInstance.fullName);
            data.Recipients = recepients;
            data.Ccs = requestHeaderRepository.GetEmailsByRole(new string[] { "RSVNFF RESERVATION REVIEW", "RSVNFF FINAL RESERVATION REVIEW" });
            string reportPath = "/K2Report/Reservation/FnFBookingRequest";
            
            IGenericFormRpt genericForm = new GenericFormRpt();

            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, reportPath, ExportType.Pdf);
            var FileName = string.Concat("FnFBookingRequest_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            EmailFileAttachment fileAttachments = new EmailFileAttachment(FileName, buffer);

            data.AttachmentFiles.Add(fileAttachments);

            return data;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new FnFRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "FnF Booking Request";
        }

        protected override string GetRequestCode()
        {
            return "RSVNFF_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "RSVNFF";
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || REWORKED.Equals(CurrentActivity().ActivityName))
            {
                return "ORIGINATOR";
            }
            else
            {
                return "RESERVATION";
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

        protected override void LoadFormData()
        {
            var reservation = _bookingRepository.GetByRequestHeader(RequestHeader.Id);
            if (reservation != null) {
                WorkflowInstance.Reservation = reservation;
                WorkflowInstance.Occupancies = _bookingRepository.ExecOccupacies(
                       reservation.CheckInDate, 
                       reservation.CheckOutDate, 
                       reservation.RequestHeaderId, 0, 0, true);
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
                if (WorkflowInstance.Reservation != null)
                {

                    Booking booking = WorkflowInstance.Reservation;
                    bool isUpdate = false;
                    if (RequestHeader.Id > 0)
                    {
                        int roomNightToken = _bookingRepository.GetTotalRoomNightTaken(RequestHeader.Id);

                        if("Reservation Approval".IsCaseInsensitiveEqual(CurrentActivity().ActivityName) && roomNightToken < 0)
                        {
                            throw new Exception("You can't approved because the total room night taken is over limit please take another action: Reworked or Rejected.");
                        }

                        var currentBooking = _bookingRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentBooking != null)
                        {
                            currentBooking.RoomCategoryId = booking.RoomCategoryId;
                            currentBooking.GuestFullName = booking.GuestFullName;
                            currentBooking.PassportNo = booking.PassportNo;
                            currentBooking.Relationship = booking.Relationship;
                            currentBooking.ExtraBed = booking.ExtraBed;
                            currentBooking.NumberOfRoom = booking.NumberOfRoom;
                            currentBooking.PaxsAdult = booking.PaxsAdult;
                            currentBooking.PaxsChild = booking.PaxsChild;
                            currentBooking.Remark = booking.Remark;
                            currentBooking.Agree = booking.Agree;
                            currentBooking.TermConditionId = booking.TermConditionId;
                            currentBooking.ConfirmationNumber = booking.ConfirmationNumber;
                            currentBooking.CheckOutDate = booking.CheckOutDate;
                            currentBooking.CheckInDate = booking.CheckInDate;
                            currentBooking.ReceiveDate = booking.ReceiveDate;
                            
                            _bookingRepository.Update(currentBooking);
                            booking.RequestHeaderId = RequestHeader.Id;
                            isUpdate = true;

                            _bookingRepository.ExecOccupacies(booking.CheckInDate, booking.CheckOutDate, booking.RequestHeaderId, -1, null, false);
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.Reservation.RequestHeader = RequestHeader;
                        _bookingRepository.Add(booking);
                        _bookingRepository.ExecOccupacies(booking.CheckInDate, booking.CheckOutDate, booking.RequestHeaderId, 0, null, false);
                    }

                    processData(WorkflowInstance.EditOccupancies, DataOP.EDIT, booking);

                    _dataField.Add("confirmationNumber", booking.ConfirmationNumber);
                }
                else
                {
                    throw new Exception("Booking FnF have no instance");
                }
            }
        }

        private void processData(IEnumerable<OccupancyDto> items, DataOP op, Booking booking)
        {
            if (items != null) {
                foreach (var item in items)
                {
                    if (DataOP.EDIT == op)
                    {
                        _bookingRepository.ExecOccupacies(booking.CheckInDate, booking.CheckOutDate, booking.RequestHeaderId, item.Id, item.Occupancy, false);
                    }
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
                    var entity = new FnFAttachment()
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
