using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Scheduler;

namespace Workflow.Scheduler.Jobs
{

    public class EmployeeNoDeptJob : ScheduleJobBase, IScheduleJob {

        protected object _model = null;

        protected override string KeyValue {
            get {
                return "form_long_pending_key";
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
            IRequestHeaderRepository RequestHeaderRepository = new RequestHeaderRepository(_DbFactory);
            IEnumerable<FormLongPending> forms = RequestHeaderRepository.GetFormLongPending(3, "BCJ_REQ");
            if(forms != null && forms.Count() > 0) {
                _model = forms.ToList();
                SendEmail();
            }            
        }
    }
}
