using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Scheduler;

namespace Workflow.Scheduler.Jobs
{

    public class FormLongPendingJob : ScheduleJobBase, IScheduleJob {

        protected object _model = null;

        protected override string KeyValue {
            get {
                return "employee_no_dept_key";
            }
        }

        protected override object Model {
            get {
                return _model;
            }
        }

        protected override Type Type {
            get {
                return GetType();
            }
        }

        protected override void ExecuteJob(IJobExecutionContext context) {
            IEmployeeRepository employeeRepo = new EmployeeRepository(_DbFactory);
            IEnumerable<EmpNoDeptDto> forms = employeeRepo.GetEmployeesNoDept();
            if(forms != null && forms.Count() > 0) {
                _model = forms.ToList();
                SendEmail();
            }            
        }
    }
}
