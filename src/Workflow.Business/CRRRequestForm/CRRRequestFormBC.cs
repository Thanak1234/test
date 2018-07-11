

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

namespace Workflow.Business.CRRRequestForm
{
    public class CRRRequestFormBC : AbstractRequestFormBC<CRRRequestWorkflowInstance, ICRRFormDataProcessing>, ICRRRequestFormBC
    {
        
        public const string HOD_APPROVAL = "HoD Approval";
        public const string RESERVATION_REVIEW = "Reservation Review";
        public const string GM_APPROVAL = "GM Approval";
        public const string REWORKED = "Requestor Rework";
        public const string EDIT= "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IComplimentaryRepository _complimentaryRepository = null;
        private Attachment.IComplimentaryRepository _attachmentRepository = null;
        private IGuestRepository _guestReposity = null;
        private IComplimentaryCheckItemRepository _CheckItemRepository = null;
        
        public CRRRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            
            _complimentaryRepository = new ComplimentaryRepository(dbFactory);
            _attachmentRepository = new Attachment.ComplimentaryRepository(dbDocFactory);
            _guestReposity = new GuestRepository(dbFactory);
            _CheckItemRepository = new ComplimentaryCheckItemRepository(dbFactory);
            
            AddActivities(new CRRSubmissionActivity());
            AddActivities(new HoDApprovalActivity());
            AddActivities(new CRRReservationReviewActivity());
            AddActivities(new GMApprovalActivity());
            AddActivities(new CRRReworkedActivity());

            AddActivities(new CRREditFormAcitvity(() => {
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
                        "Complimentary Room Request process has been " + status + " by: {3}.<br/><br/> " +
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
            //data.Ccs = requestHeaderRepository.GetEmailsByRole(new string[] { "CRR RESERVATION REVIEW"});
            data.Ccs = requestHeaderRepository.GetEmailNotification(RequestHeader.Id, "RSVNCR_REQ", "HOD,CRSR",false);
            //string reportPath = "/K2Report/Reservation/CRoomRequest";
            string reportPath = "/FORMS/FORM_RSVNCR";


            IGenericFormRpt genericForm = new GenericFormRpt();

            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, reportPath, ExportType.Pdf);
            var FileName = string.Concat(RequestHeader.Title + "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            EmailFileAttachment fileAttachments = new EmailFileAttachment(FileName, buffer);

            data.AttachmentFiles.Add(fileAttachments);

            return data;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new CRRRequestWorkflowInstance();
            }
        }
        protected override string getFullProccessName()
        {
            return _processFolderName + "Complimentary Room Request";
        }

        protected override string GetRequestCode()
        {
            return "RSVNCR_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "RSVNCR";
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
            var reservation = _complimentaryRepository.GetByRequestHeader(RequestHeader.Id);
            var guests = _guestReposity.GetMany(p => p.RequestHeaderId == RequestHeader.Id);

            var checkitemlist = _CheckItemRepository.GetComplimentaryCheckItem(RequestHeader.Id);
            var checkitem = _CheckItemRepository.GetPivotComplimentaryCheckItem(RequestHeader.Id);

            if (reservation != null) {
                WorkflowInstance.Complimentary = reservation;                
            }

            if (guests != null)
            {
                WorkflowInstance.Guests = guests;
            }

            if (checkitemlist != null)
            {
                WorkflowInstance.CheckExpenseItem = checkitemlist;
            }

            if(checkitem != null)
            {
                WorkflowInstance.CheckExpense = checkitem;
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
                if (WorkflowInstance.Complimentary != null)
                {

                    Complimentary Complimentary = WorkflowInstance.Complimentary;
                    
                    bool isUpdate = false;
                    if (RequestHeader.Id > 0)
                    {
                        var currentComplimentary = _complimentaryRepository.GetByRequestHeader(RequestHeader.Id);
                        IList<ComplimentaryCheckItemExt> checkitem = _CheckItemRepository.GetComplimentaryCheckItem(RequestHeader.Id);

                        if(checkitem != null)
                        {

                            foreach (ComplimentaryCheckItemExt ci in checkitem) {

                                ComplimentaryCheckItem i = null;
                                int explId = 0;
                                
                                i = _CheckItemRepository.GetComplimentaryCheckItemByRequestHeader_TypeId(RequestHeader.Id, ci.Id);
                                
                                switch (ci.Id)
                                {
                                    case 1:
                                        if (WorkflowInstance.CheckExpense.MealExcludingAlcohol)
                                            explId = 1;
                                        break;
                                    case 2:
                                        if (WorkflowInstance.CheckExpense.Alcohol)
                                            explId = 2;
                                        break;
                                    case 3:
                                        if (WorkflowInstance.CheckExpense.Tobacco)
                                            explId = 3;
                                        break;
                                    case 4:
                                        if (WorkflowInstance.CheckExpense.Spa)
                                            explId = 4;
                                        break;
                                    case 5:
                                        if (WorkflowInstance.CheckExpense.SouvenirShop)
                                            explId = 5;
                                        break;
                                    case 6:
                                        if (WorkflowInstance.CheckExpense.AirportTransfers)
                                            explId = 6;
                                        break;
                                    case 7:
                                        if (WorkflowInstance.CheckExpense.OtherTransportwithinCity)
                                            explId = 7;
                                        break;
                                    case 8:
                                        if (WorkflowInstance.CheckExpense.ExtraBeds)
                                            explId = 8;
                                        break;
                                    default:
                                        explId = 0;
                                        break;
                                }
                                
                                if(explId > 0)
                                {
                                    if(i == null)
                                    {
                                        i = new ComplimentaryCheckItem()
                                        {                                            
                                            CreatedDate = DateTime.Now,
                                            ExplId = explId,
                                            RequestHeaderId = RequestHeader.Id                                            
                                        };

                                        _CheckItemRepository.Add(i);
                                    }                                    
                                }
                                else
                                {
                                    if(i != null)
                                    {
                                        _CheckItemRepository.Delete(i);
                                    }
                                }
                                                                
                            }
                        }

                        if (currentComplimentary != null)
                        {
                            currentComplimentary.RoomCategoryId = Complimentary.RoomCategoryId;
                            currentComplimentary.RoomSubCategoryId = Complimentary.RoomSubCategoryId;
                            
                            currentComplimentary.ConfirmationNumber = Complimentary.ConfirmationNumber;

                            currentComplimentary.ArrivalDate = Complimentary.ArrivalDate;
                            currentComplimentary.ArrivalTransfer = Complimentary.ArrivalTransfer;
                            currentComplimentary.ArrivalFlightDetail = Complimentary.ArrivalFlightDetail;
                            currentComplimentary.ArrivalVehicleTypeId = Complimentary.ArrivalVehicleTypeId;

                            currentComplimentary.DepartureDate = Complimentary.DepartureDate;
                            currentComplimentary.DepartureFlightDetail = Complimentary.DepartureFlightDetail;
                            currentComplimentary.DepartureTransfer = Complimentary.DepartureTransfer;
                            currentComplimentary.DepartureVehicleTypeId = Complimentary.DepartureVehicleTypeId;
                            
                            currentComplimentary.ExtraBed = Complimentary.ExtraBed;
                            currentComplimentary.Remark = Complimentary.Remark;
                            currentComplimentary.ConfirmationNumber = Complimentary.ConfirmationNumber;

                            currentComplimentary.RoomCharge = Complimentary.RoomCharge;
                            currentComplimentary.SpecialRequest = Complimentary.SpecialRequest;
                            currentComplimentary.Adult = Complimentary.Adult;
                            currentComplimentary.Childrent = Complimentary.Childrent;
                            currentComplimentary.DepartmentIncharge = Complimentary.DepartmentIncharge;
                            currentComplimentary.ExtraBed = Complimentary.ExtraBed;
                            currentComplimentary.Id = Complimentary.Id;
                            currentComplimentary.PurposeId = Complimentary.PurposeId;
                            currentComplimentary.Remark = Complimentary.Remark;
                            
                            currentComplimentary.Room = Complimentary.Room;
                            currentComplimentary.VipStatusId = Complimentary.VipStatusId;                                
                            
                            _complimentaryRepository.Update(currentComplimentary);
                            Complimentary.RequestHeaderId = RequestHeader.Id;
                            isUpdate = true;
                        }
                        
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.Complimentary.RequestHeader = RequestHeader;
                        _complimentaryRepository.Add(Complimentary);
                    }

                    ProcessRequestGuestData(WorkflowInstance.AddRequestGuests, DataOP.AddNew);
                    ProcessRequestGuestData(WorkflowInstance.DelRequestGuests, DataOP.DEL);
                    ProcessRequestGuestData(WorkflowInstance.EditRequestGuests, DataOP.EDIT);

                }
                else
                {
                    throw new Exception("Complimentary RSVNCR have no instance");
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
                    var entity = new AttachmentEntity.Complimentary()
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

        private void ProcessRequestGuestData(IEnumerable<Guest> requestGuests, DataOP op)
        {

            if (requestGuests == null) return;

            foreach (Guest g in requestGuests)
            {   
                if (DataOP.AddNew == op)
                {
                    g.RequestHeaderId = RequestHeader.Id;
                    _guestReposity.Add(g);
                }

                if (DataOP.DEL == op)
                {
                    var user = _guestReposity.GetById(g.Id);
                    _guestReposity.Delete(user);
                }

                if (DataOP.EDIT == op)
                {  
                    _guestReposity.Update(g);
                }
            }

        }

    }
}
