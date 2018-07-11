using CrystalQuartz.Core.SchedulerProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz;
using Workflow.Scheduler.Jobs;
using Workflow.Domain.Entities.Scheduler;
using Workflow.DataAcess;
using System.Reflection;
using System.Web.Compilation;

namespace Workflow.Scheduler {

    public class SchedulerProvider : StdSchedulerProvider {
        private static ISchedulerProvider _instance = null;
        //private string _jobnamespace = "Workflow.Quartz.Jobs";

        public static ISchedulerProvider Instance {
            get {
                if (_instance == null)
                    _instance = new SchedulerProvider();
                return _instance;
            }
        }

        protected SchedulerProvider() {
        }

        protected override NameValueCollection GetSchedulerProperties() {
            var properties = base.GetSchedulerProperties();

            return properties;
        }

        protected override void InitScheduler(IScheduler scheduler) {
            var jobs = GetJobAssemblies();
            if (jobs != null && jobs.Count() > 0) {
                foreach (var jType in jobs) {
                    IScheduleJob job = Activator.CreateInstance(jType) as IScheduleJob;
                    if (job != null) {
                        job.Register(scheduler);
                    }
                }
            }
        }

        public IEnumerable<Type> GetJobAssemblies() {
            
            return new Type[] {
                typeof(ErrorSenderJob),
                typeof(FormLongPendingJob),
                typeof(EmployeeNoDeptJob),
                typeof(CourseRegistrationJob),
                typeof(RestartiClockServiceJob),
                typeof(RmdNotification)
            };
        }
    }
}
