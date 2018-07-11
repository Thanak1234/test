/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Business.Interfaces;
using Workflow.Business.ITRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.IT;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Business.AvirForm {
    public class AvirFormBC : AbstractRequestFormBC<AvirFormWorkflowInstance, IAvirFormDataProcessing>, IAvirFormBC {

        public const string EDIT = "Modification";
        protected IAvirRepository _AvirRepository;

        public AvirFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            AddActivities(new AvirFormSubmissionActivity());
            AddActivities(new AvirFormEditActivity());
            _AvirRepository = new AvirRepository(dbFactory);
        }

        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                SaveRequestHeader();
            }

            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveBusinessData) {
                SaveBusinessData();
            }

        }

        private void SaveRequestHeader() {
            if(RequestHeader != null && RequestHeader.Id > 0) {
                var oEntity = requestHeaderRepository.Get(p => p.Id == RequestHeader.Id);
                if(oEntity != null) {
                    oEntity.LastActionBy = "k2:" + WorkflowInstance.CurrentUser;
                    oEntity.LastActivity = CurrentActivity().ActivityName;
                    oEntity.LastActionDate = DateTime.Now;
                    oEntity.Priority = WorkflowInstance.Priority;
                    oEntity.RequestorId = WorkflowInstance.FormRequestData.ReceiverId;
                    oEntity.SubmittedBy = "k2:" + WorkflowInstance.CurrentUser;
                    oEntity.CurrentActivity = CurrentActivity().ActivityName;
                    oEntity.Status = "Done";
                    oEntity.RequestCode = GetRequestCode();
                    oEntity.NoneK2 = true;
                    requestHeaderRepository.Update(oEntity);
                    _requestHeader = oEntity;
                }
            } else {
                var requestHeader = new RequestHeader();
                requestHeader.LastActionBy = "k2:" + WorkflowInstance.CurrentUser;
                requestHeader.LastActivity = CurrentActivity().ActivityName;
                requestHeader.LastActionDate = DateTime.Now;
                requestHeader.Priority = WorkflowInstance.Priority;
                requestHeader.RequestorId = WorkflowInstance.FormRequestData.ReceiverId;

                requestHeader.SubmittedBy = "k2:" + WorkflowInstance.CurrentUser;
                requestHeader.CurrentActivity = CurrentActivity().ActivityName;
                requestHeader.Status = "Done";
                requestHeader.RequestCode = GetRequestCode();
                requestHeader.Title = requestHeaderRepository.GetRequestNo(GetRequestCodePrefix(), GetRequestCode());
                requestHeader.NoneK2 = true;
                requestHeader.ProcessInstanceId = -1;
                requestHeaderRepository.Add(requestHeader);
                requestHeader.ProcessInstanceId = requestHeader.Id;
                requestHeaderRepository.Update(requestHeader);
                _requestHeader = requestHeader;
            }
        }

        private void SaveBusinessData() {
            if (RequestHeader.Id > 0) {
                var oEntity = _AvirRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if (oEntity != null) {
                    var nEntity = WorkflowInstance.FormRequestData.TypeAs<Avir>();

                    oEntity.ReceiverId = nEntity.ReceiverId;
                    oEntity.Location = nEntity.Location;
                    oEntity.IncidentDate = nEntity.IncidentDate;
                    oEntity.ReporterId = nEntity.ReporterId;
                    oEntity.ComplaintRegarding = nEntity.ComplaintRegarding;
                    oEntity.Complaint = nEntity.Complaint;

                    _AvirRepository.Update(oEntity);
                } else {
                    var nEntity = WorkflowInstance.FormRequestData.TypeAs<Avir>();
                    nEntity.RequestHeaderId = RequestHeader.Id;
                    _AvirRepository.Add(nEntity);
                }
            }
        }
        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void LoadFormData()
        {
            WorkflowInstance.FormRequestData = _AvirRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance== null)
            {
                WorkflowInstance =  new AvirFormWorkflowInstance();
            }
        }

        protected override IEnumerable<FileAttachement> GetUploadFiles()
        {            
            return null;
        }

        protected override string getActivityforFileUpload()
        {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName))
            {
                return "REQUESTOR";
            }
            else
            {
                return CurrentActivity().ActivityName.ToUpper();
            }
        }

        protected override string GetRequestCode()
        {
            return "AVIR_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "AVIR";
        }

        protected override string getFullProccessName()
        {
            return _processFolderName + "AV Incident Report";
        }

        protected override void SaveAttachmentFiles() {
            AddAttachements();
            DelAttachements();
        }

        private void AddAttachements() {

            
        }

        private void DelAttachements() {
            
        }
    }
}
