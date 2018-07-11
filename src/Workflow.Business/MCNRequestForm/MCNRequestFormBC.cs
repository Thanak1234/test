using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.EGMMachine;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;

namespace Workflow.Business.MCNRequestForm
{
    public class MCNRequestFormBC : AbstractRequestFormBC<MCNRequestWorkflowInstance,IMCNFormDataProcessing>,IMCNRequestFormBC
    {
        public const string EDIT = "Modification";

        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        private IMCNRepository _machineRepository = null;
        private IMCNMachineAttachmentRepository _attachmentRepository = null;
        private IMCNMachineEmployeeRepository _machineEmployeeReposity = null;

        public MCNRequestFormBC(IDbFactory dbWorkFlow, IDbFactory docDbWorkFlow) : base(dbWorkFlow,docDbWorkFlow)
        {
            _machineRepository = new MCNRepository(dbWorkFlow);
            _machineEmployeeReposity = new MCNMachineEmployeeRepository(dbWorkFlow);
            _attachmentRepository = new MCNMachineAttachmentRepository(docDbWorkFlow);

            this.AddActivities(new MCNSubmissionActivity());
            this.AddActivities(new MCNEditFormActivity(() => { return this.CreateEmailData("NOTIFICATION"); }));
            IsRemoveableAttachment = true;
        }
        
        protected override void CreateWorkflowInstance()
        {
            if (WorkflowInstance == null)
            {
                WorkflowInstance = new MCNRequestWorkflowInstance();
            }
        }

        protected override string getFullProccessName()
        {
            //return "NagaworldDev\\Slot-MachineReport";
            return _processFolderName + "Slot-MachineReport";
        }

        protected override string GetRequestCode()
        {
            return "EGMMR_REQ";
        }

        protected override string GetRequestCodePrefix()
        {
            return "EGMMR";
        }

        protected override Dictionary<string, object> GetDataField()
        {
            return _dataField;
        }
        
        protected override void LoadFormData()
        {
            WorkflowInstance.Machine = _machineRepository.Get(p => p.request_header_id == RequestHeader.Id);
            WorkflowInstance.MachineEmployeeList = _machineEmployeeReposity.GetMachineEmployeeList(RequestHeader.Id);            
        }
        
        protected override void TakeFormAction()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData)
            {
                if(WorkflowInstance.Machine != null)
                {
                    Machine machine = WorkflowInstance.Machine;

                    bool _isUpdate = false;

                    Machine _currentMachine = _machineRepository.Get(p => p.request_header_id == RequestHeader.Id);

                    if(_currentMachine != null)
                    {
                        _currentMachine.mcid = machine.mcid;
                        _currentMachine.gamename = machine.gamename;
                        _currentMachine.type = machine.type;
                        _currentMachine.remarks = machine.remarks;
                        _currentMachine.zone = machine.zone;

                        _currentMachine.modified_by = WorkflowInstance.CurrentUser;
                        _currentMachine.modified_date = DateTime.Now;

                        _isUpdate = true;

                        _machineRepository.Update(_currentMachine);
                        machine.request_header_id = RequestHeader.Id;
                    }

                    if (!_isUpdate)
                    {
                        WorkflowInstance.Machine.request_header_id = RequestHeader.Id;
                        _machineRepository.Add(WorkflowInstance.Machine);
                    }

                    this.ProcessRequestEmployeeList(WorkflowInstance.AddMachineEmployeeList, DataOP.AddNew);
                    this.ProcessRequestEmployeeList(WorkflowInstance.DelMachineEmployeeList, DataOP.DEL);
                    this.ProcessRequestEmployeeList(WorkflowInstance.EditMachineEmployeeList, DataOP.EDIT);
                }
                else
                {
                    throw new Exception("Machine Rquest MCN have no Instance");
                }
            }
        }
        
        private void ProcessRequestEmployeeList(IEnumerable<RequestUser> EmployeeList, DataOP op)
        {
            if (EmployeeList == null) return;

            MachineEmployee me = new MachineEmployee();

            foreach(RequestUser u in EmployeeList)
            {
                if(op == DataOP.AddNew)
                {
                    me.request_header_id = RequestHeader.Id;
                    me.empno = u.EmpNo;

                    _machineEmployeeReposity.Add(me);
                }

                if(op == DataOP.EDIT)
                {
                    MachineEmployee mee = _machineEmployeeReposity.Get(p => p.empno == u.EmpNo && p.request_header_id == RequestHeader.Id);

                    mee.empno = u.EmpNo;
                    mee.request_header_id = RequestHeader.Id;

                    _machineEmployeeReposity.Update(mee);
                }

                if(op == DataOP.DEL)
                {
                    _machineEmployeeReposity.Delete(_machineEmployeeReposity.Get(p => p.empno == u.EmpNo && p.request_header_id == RequestHeader.Id));
                }
            }
        }

    }
}
