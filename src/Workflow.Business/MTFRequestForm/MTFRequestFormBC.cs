/**
*@author : Yim Samaune
*/
using System;
using System.Collections.Generic;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.MTF;
using Workflow.Domain.Entities.BatchData;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.MTF;

namespace Workflow.Business
{
    public class MTFRequestFormBC :
        AbstractRequestFormBC<MTFRequestWorkflowInstance, IDataProcessing>,
        IMTFRequestFormBC
    {
        private ITreatmentRepository _treatmentRepository = null;
        private IPrescriptionRepository _prescriptionRepository = null;
        private IUnfitToWorkRepository _unfitToWorkRepository = null;


        public MTFRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            _treatmentRepository = new TreatmentRepository(dbFactory);
            _prescriptionRepository = new PrescriptionRepository(dbFactory);
            _unfitToWorkRepository = new UnfitToWorkRepository(dbFactory);
        }

        protected override void InitActivityConfiguration()
        {

            AddActivities(new ActivityEngine());
            AddActivities(new ActivityEngine(REQUESTOR_REWORKED));
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(() =>
            {
                IEnumerable<string> ccList = null;
                string subject = string.Empty;
                if (RequestHeader != null)
                {
                    ccList = requestHeaderRepository.GetMTFEmailNotification(RequestHeader.Id);
                    subject = _treatmentRepository.GetSubjectEmail(RequestHeader.Id);
                }

                return CreateEmailData("MODIFICATION", ccList, subject);
            },
            new FormDataProcessing()
            {
                IsAddNewRequestHeader = false,
                IsEditPriority = false,
                IsEditRequestor = false,
                IsSaveActivityHistory = true,
                IsUpdateLastActivity = true,
                IsSaveRequestData = true,
                IsSaveAttachments = true,
                TriggerWorkflow = false
            }));
        }

        protected override string GetRequestCode()
        {
            return PROCESSCODE.MTRQ;
        }

        #region Load/Save Form

        protected override void LoadFormData()
        {
            var treatment = _treatmentRepository.GetByRequestHeader(RequestHeader.Id);
            if (treatment != null)
            {
                WorkflowInstance.Treatment = treatment;
                WorkflowInstance.Prescriptions = _prescriptionRepository.GetByTreatmentId(WorkflowInstance.Treatment.Id);
                WorkflowInstance.UnfitToWorks = _unfitToWorkRepository.GetByRequestHeader(RequestHeader.Id);
            }
        }

        protected override bool IsAuthorizeRemoveAttachment(string activityCode)
        {
            if (CurrentActivityCode == "MODIFICATION" && activityCode == "DOCTOR_EXAMINE_AND_TREAT")
            {
                return true;
            }

            return (CurrentActivityCode == activityCode);
        }

        protected override void TakeFormAction()
        {
            var currentActvity = CurrentActivity();
            if (currentActvity.CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if (WorkflowInstance.Treatment != null)
                {
                    var treatment = WorkflowInstance.Treatment;
                    bool isUpdate = false;

                    if (RequestHeader.Id > 0)
                    {
                        var currentTreatment = _treatmentRepository.GetByRequestHeader(RequestHeader.Id);
                        if (currentTreatment != null)
                        {
                            currentTreatment.RequestHeaderId = treatment.RequestHeaderId;
                            currentTreatment.FitToWork = treatment.FitToWork;
                            currentTreatment.TimeArrived = treatment.TimeArrived;
                            currentTreatment.TimeDeparted = treatment.TimeDeparted;
                            currentTreatment.CheckOutDateTime = treatment.TimeDeparted; // treatment.CheckOutDateTime;
                            currentTreatment.Days = (treatment.FitToWork == 0) ? treatment.Days : null;
                            currentTreatment.Remark = (treatment.FitToWork == 0) ? treatment.Remark : string.Empty;
                            currentTreatment.Symptom = treatment.Symptom;
                            currentTreatment.Diagnosis = treatment.Diagnosis;
                            currentTreatment.Hours = (treatment.FitToWork == 1) ? treatment.Hours : null;
                            currentTreatment.Comment = treatment.Comment;
                            currentTreatment.WorkShift = treatment.WorkShift;

                            _treatmentRepository.Update(currentTreatment);
                            treatment.RequestHeaderId = RequestHeader.Id;
                            isUpdate = true;
                        }
                    }

                    if (!isUpdate)
                    {
                        WorkflowInstance.Treatment.RequestHeaderId = RequestHeader.Id;
                        _treatmentRepository.Add(treatment);
                    }

                    if (treatment.FitToWork == 0)
                    {
                        ProcessUnfitToWorkData(WorkflowInstance.AddUnfitToWorks, DataOP.AddNew, RequestHeader);
                        ProcessUnfitToWorkData(WorkflowInstance.EditUnfitToWorks, DataOP.EDIT, RequestHeader);
                        ProcessUnfitToWorkData(WorkflowInstance.DelUnfitToWorks, DataOP.DEL, RequestHeader);

                        if (RequestHeader != null && RequestHeader.Id > 0)
                        {
                            _treatmentRepository.UpdateDayLeave(RequestHeader.Id);
                        }
                    } else if (treatment.FitToWork == 1) { 
                        if (RequestHeader != null && RequestHeader.Id > 0)
                        {
                            _treatmentRepository.DeleteUniftTW(RequestHeader.Id);
                        }
                    }
                    else if (treatment.FitToWork == 1)
                    {
                        if (RequestHeader != null && RequestHeader.Id > 0)
                        {
                            _treatmentRepository.DeleteUniftTW(RequestHeader.Id);
                        }
                    }

                    // Process transaction data for request items
                    ProcessPrescriptionData(WorkflowInstance.AddPrescriptions, DataOP.AddNew, treatment);
                    ProcessPrescriptionData(WorkflowInstance.EditPrescriptions, DataOP.EDIT, treatment);
                    ProcessPrescriptionData(WorkflowInstance.DelPrescriptions, DataOP.DEL, treatment);

                    if (FORM_SUBMISSION.IsCaseInsensitiveEqual(CurrentActivity().ActivityName))
                    {
                        WorkflowInstance.Message = string.Format("Your request number is @FOLIO. <br/>{0}", _treatmentRepository.GetPendingTreamentMessage());
                    }
                }
                else
                {
                    throw new Exception("Medical treatment form has no request instance");
                }
            }
        }

        private void ProcessUnfitToWorkData(IEnumerable<UnfitToWork> unfitToWorks, DataOP op, RequestHeader requestHeader)
        {
            if (unfitToWorks == null) return;
            var leaveList = _unfitToWorkRepository.GetByRequestHeader(requestHeader.Id);
            foreach (var unfitToWork in unfitToWorks)
            {
                unfitToWork.RequestId = requestHeader.Id;
                if (DataOP.AddNew == op)
                {
                    bool allowAdd = true;
                    leaveList.Each(p =>
                    {
                        if (p.NoDay == unfitToWork.NoDay &&
                            p.Status == unfitToWork.Status &&
                            p.UtwDate == unfitToWork.UtwDate)
                        {
                            allowAdd = false;
                        }
                    });
                    if (allowAdd)
                    {
                        _unfitToWorkRepository.Add(unfitToWork);
                    }
                }
                else if (DataOP.EDIT == op)
                {
                    var updateRecord = _unfitToWorkRepository.GetById(unfitToWork.Id);
                    updateRecord.NoDay = unfitToWork.NoDay;
                    updateRecord.Status = unfitToWork.Status;
                    updateRecord.UtwDate = unfitToWork.UtwDate;

                    _unfitToWorkRepository.Update(updateRecord);
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _unfitToWorkRepository.GetById(unfitToWork.Id);
                    if (removeRecord != null)
                    {
                        _unfitToWorkRepository.Delete(removeRecord);
                    }
                }
            }
        }

        private void ProcessPrescriptionData(IEnumerable<Prescription> prescriptions, DataOP op, Treatment treatment)
        {
            if (prescriptions == null || treatment == null) return;
            
            foreach (var prescription in prescriptions)
            {
                prescription.TreatmentId = treatment.Id;
                if (DataOP.AddNew == op)
                {
                    _prescriptionRepository.Add(prescription);
                }
                else if (DataOP.EDIT == op)
                {
                    _prescriptionRepository.Update(prescription);   
                }
                else if (DataOP.DEL == op)
                {
                    var removeRecord = _prescriptionRepository.GetById(prescription.Id);
                    if (removeRecord != null)
                    {
                        _prescriptionRepository.Delete(removeRecord);
                    }
                }
            }
        }
        #endregion
    }
}
