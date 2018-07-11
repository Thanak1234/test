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

namespace Workflow.Business.AvdrForm {
    public class AvdrFormBC : AbstractRequestFormBC<AvdrFormWorkflowInstance, IAvdrFormDataProcessing>, IAvdrFormBC {

        public const string EDIT = "Modification";

        protected IAvdrRepository _AvdrRepository;


        public AvdrFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory)
        {
            AddActivities(new AvdrFormSubmissionActivity());
            AddActivities(new AvdrFormEditActivity());
            _AvdrRepository = new AvdrRepository(dbFactory);
        }

        protected override void TakeFormAction()
        {
            if( CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData )
            {
                SaveRequestHeader();
            }

            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveBusinessData) {
                SaveBusinessData();
            }
        }

        private void SaveRequestHeader() {

            if (RequestHeader != null && RequestHeader.Id > 0) {
                var oEntity = requestHeaderRepository.Get(p => p.Id == RequestHeader.Id);
                if (oEntity != null) {
                    oEntity.LastActionBy = "k2:" + WorkflowInstance.CurrentUser;
                    oEntity.LastActivity = CurrentActivity().ActivityName;
                    oEntity.LastActionDate = DateTime.Now;
                    oEntity.Priority = WorkflowInstance.Priority;
                    oEntity.RequestorId = WorkflowInstance.FormRequestData.ReporterId;
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
                requestHeader.RequestorId = WorkflowInstance.FormRequestData.ReporterId;

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
            if(RequestHeader.Id > 0) {
                var oEntity = _AvdrRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if(oEntity != null) {
                    var nEntity = WorkflowInstance.FormRequestData.TypeAs<Avdr>();
                    
                    oEntity.ReporterId = nEntity.ReporterId;
                    oEntity.IncidentDate = nEntity.IncidentDate;
                    oEntity.SDL = nEntity.SDL;
                    oEntity.ELocation = nEntity.ELocation;
                    oEntity.DLE = nEntity.DLE;
                    oEntity.EIN = nEntity.EIN;
                    oEntity.HEDL = nEntity.HEDL;
                    oEntity.AT = nEntity.AT;
                    oEntity.ECRR = nEntity.ECRR;
                    oEntity.DCIRS = nEntity.DCIRS;

                    _AvdrRepository.Update(oEntity);
                } else {
                    var nEntity = WorkflowInstance.FormRequestData.TypeAs<Avdr>();
                    nEntity.RequestHeaderId = RequestHeader.Id;
                    _AvdrRepository.Add(nEntity);
                }
            }
        }

        protected override void SaveForm()
        {
            throw new NotImplementedException();
        }

        protected override void LoadFormData()
        {
            WorkflowInstance.FormRequestData = _AvdrRepository.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance== null)
            {
                WorkflowInstance =  new AvdrFormWorkflowInstance();
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
            return "AVDR_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "AVDR";
        }

        protected override string getFullProccessName()
        {
            return _processFolderName + "AV Equipment Damage";
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
