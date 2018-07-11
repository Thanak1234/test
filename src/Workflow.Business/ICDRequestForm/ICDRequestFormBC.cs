using Workflow.ReportingService.Report;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Attachment = Workflow.DataAcess.Repositories.Incident;
using AttachmentEntity = Workflow.Domain.Entities.Attachment;
using Workflow.DataAcess.Repositories.Incident;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.MSExchange.Core;
using Workflow.Domain.Entities.INCIDENT;
using Workflow.DataAcess;
using Workflow.MSExchange;
using Workflow.Domain.Entities.BatchData.IncidentInstance;
using System;
using System.Collections.Generic;


namespace Workflow.Business.ICDRequestForm
{
    public class ICDRequestFormBC : AbstractRequestFormBC<ICDRequestWorkflowInstance, IICDFormDataProcessing>, IICDRequestFormBC
    {
        public const string EDIT = "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IICDRepository _incidentRepository = null;
        private IICDAttachmentRepository _attachmentRepository = null;
        private IICDIncidentEmployeeRepository _incidentEmployeeReposity = null;

        public ICDRequestFormBC(IDbFactory dbWorkflow, IDbFactory docDbWorkflow) : base(dbWorkflow,docDbWorkflow)
        {
            _incidentRepository = new ICDRepository(dbWorkflow);
            _attachmentRepository = new ICDAttachmentRepository(docDbWorkflow);
            _incidentEmployeeReposity = new ICDIncidentEmployeRepository(dbWorkflow);

            AddActivities(new ICDSubmissionActivity());
            AddActivities(new ICDEditFormActivity(() => { return this.CreateEmailData("NOTIFICATION"); }));
            IsRemoveableAttachment = true;
        }

        
        protected override void CreateWorkflowInstance()
        {
            if(WorkflowInstance == null)
            {
                WorkflowInstance = new ICDRequestWorkflowInstance();
            }
        }

        protected override string getFullProccessName()
        {
            //return "NagaworldDev\\Slot-IncidentReport";
            return _processFolderName + "Slot-IncidentReport";
        }

        protected override string GetRequestCode()
        {
            return "EGMIR_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "EGMIR";
        }
        
        protected override void LoadFormData()
        {
            WorkflowInstance.Incident = _incidentRepository.Get(p => p.requestheaderid == RequestHeader.Id);
            WorkflowInstance.IncidentEmployeeList = _incidentEmployeeReposity.GetIncidentEmployeeList(RequestHeader.Id);
        }
        
        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if(WorkflowInstance.Incident != null)
                {                    
                    Incident _incident = WorkflowInstance.Incident;

                    bool _isUpdate = false;

                    Incident _currentIncident = _incidentRepository.Get(p => p.requestheaderid == RequestHeader.Id);

                    if(_currentIncident != null)
                    {
                        _currentIncident.cctv = _incident.cctv;
                        _currentIncident.customername = _incident.customername;
                        _currentIncident.gamename = _incident.gamename;
                        _currentIncident.mcid = _incident.mcid;
                        _currentIncident.outline = _incident.outline;
                        _currentIncident.remarks = _incident.remarks;
                        _currentIncident.subject = _incident.subject;
                        _currentIncident.zone = _incident.zone;
                        _currentIncident.modified_by = WorkflowInstance.CurrentUser;
                        _currentIncident.modified_date = DateTime.Now;

                        _isUpdate = true;

                        _incidentRepository.Update(_currentIncident);
                        _incident.requestheaderid = RequestHeader.Id;

                    }

                    if (!_isUpdate)
                    {
                        WorkflowInstance.Incident.requestheaderid = RequestHeader.Id;
                        _incidentRepository.Add(WorkflowInstance.Incident);
                    }

                    this.ProcessRequestEmployeeList(WorkflowInstance.AddIncidentEmployee, DataOP.AddNew);
                    this.ProcessRequestEmployeeList(WorkflowInstance.DelIncidentEmployee, DataOP.DEL);
                    this.ProcessRequestEmployeeList(WorkflowInstance.EditIncidentEmployee, DataOP.EDIT);
                }
                else
                {
                    throw new Exception("Incident Rquest ICD have no Instance");
                }
            }
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }
        
        private void ProcessRequestEmployeeList(IEnumerable<RequestUser> EmployeeList, DataOP op)
        {

            if (EmployeeList == null) return;

            IncidentEmployee ie = new IncidentEmployee();

            foreach (RequestUser u in EmployeeList)
            {
                if (op == DataOP.AddNew)
                {
                    ie.requestheaderid = RequestHeader.Id.ToString();
                    ie.employeeno = u.EmpNo;

                    _incidentEmployeeReposity.Add(ie);
                }

                if (op == DataOP.EDIT)
                {
                    IncidentEmployee eIE = _incidentEmployeeReposity.Get(p => p.employeeno == u.EmpNo &&
                                                                              p.requestheaderid == RequestHeader.Id.ToString()
                                                                        );
                    eIE.employeeno = u.EmpNo;
                    eIE.requestheaderid = RequestHeader.Id.ToString();

                    _incidentEmployeeReposity.Update(eIE);
                }

                if (op == DataOP.DEL)
                {
                    _incidentEmployeeReposity.Delete(
                                                    _incidentEmployeeReposity.Get(p => p.requestheaderid == RequestHeader.Id.ToString() &&
                                                                                       p.employeeno == u.EmpNo
                                                                                 )
                                                    );
                }

            }
        }        
    }
}
