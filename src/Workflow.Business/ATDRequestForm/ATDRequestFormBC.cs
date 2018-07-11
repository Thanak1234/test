using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.EGMAttandance;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using System;
using System.Collections.Generic;

namespace Workflow.Business.ATDRequestForm
{
    public class ATDRequestFormBC : AbstractRequestFormBC<ATDRequestWorkflowInstance, IATDFormDataProcessing>, IATDRequestFormBC
    {
        public const string EDIT = "Modification";
        
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IATDRepository _ATDRepository = null;
        private IATDAttachmentRepository _ATDAttachmentRepository = null;

        public ATDRequestFormBC(IDbFactory dbWorkflow, IDbFactory docDbWorkflow) : base(dbWorkflow,docDbWorkflow)
        {
            _ATDRepository = new ATDRepository(dbWorkflow);
            _ATDAttachmentRepository = new ATDAttachmentRepository(docDbWorkflow);

            this.AddActivities(new ATDSubmissionActivity());
            this.AddActivities(new ATDEditFormActivity(() => {
                return this.CreateEmailData("NOTIFICATION");
                })
            );
            IsRemoveableAttachment = true;
        }
        
        protected override void LoadFormData()
        {
            WorkflowInstance.Attandance = _ATDRepository.Get(p => p.request_header_id == RequestHeader.Id);
        }
        
        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if(WorkflowInstance.Attandance != null)
                {
                    Attandance _attandance = WorkflowInstance.Attandance;
                    bool _isUpdate = false;

                    Attandance _CurrentAttandance = _ATDRepository.Get(p => p.request_header_id == RequestHeader.Id);

                    if(_CurrentAttandance != null)
                    {
                        _CurrentAttandance.detail = _attandance.detail;
                        _CurrentAttandance.remarks = _attandance.remarks;                        

                        _CurrentAttandance.modified_by = WorkflowInstance.CurrentUser;
                        _CurrentAttandance.modified_date = DateTime.Now;

                        _ATDRepository.Update(_CurrentAttandance);
                        _attandance.request_header_id = RequestHeader.Id;                        

                        _isUpdate = true;
                    }

                    if(!_isUpdate)
                    {
                        WorkflowInstance.Attandance.request_header_id = RequestHeader.Id;
                        _ATDRepository.Add(WorkflowInstance.Attandance);
                    }
                }
                else
                {
                    throw new Exception("EGM Attandance Rquest ATD have no Instance");
                }
            }
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }

        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new ATDRequestWorkflowInstance();
            }
        }

        protected override string getFullProccessName()
        {
            //return "NagaworldDev\\Slot-Attendance";
            return _processFolderName + "Slot-Attendance";
        }

        protected override string GetRequestCode()
        {
            return "EGMATT_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "EGMATT";
        }
    }
}
