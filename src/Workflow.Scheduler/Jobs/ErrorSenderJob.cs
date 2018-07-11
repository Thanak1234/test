using Quartz;
using System;

namespace Workflow.Scheduler.Jobs
{

    public class ErrorSenderJob : ScheduleJobBase, IScheduleJob {

        protected object _model = null;

        protected override string KeyValue {
            get {
                return "error_sender_key";
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
            // TODO: Read error from K2 database
            //BlackpearlServer bs = new BlackpearlServer();
            //var models = new List<ErrorLog>();
            //var errors = bs.GetWorkflowErrors();
            //if(errors != null && errors.Count > 0) {
            //    _model = errors;
            //    SendEmail();
            //}            
        }
    }
}
