using System.Collections.Specialized;
using CrystalQuartz.Core;
using CrystalQuartz.Core.SchedulerProviders;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Scheduler.Cores;

namespace Workflow.Scheduler {

    public abstract class AbstractSchedulerCommand {

        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISchedulerDataProvider _schedulerDataProvider;

        protected AbstractSchedulerCommand(ISchedulerProvider schedulerProvider, ISchedulerDataProvider schedulerDataProvider) {
            _schedulerProvider = schedulerProvider;
            _schedulerDataProvider = schedulerDataProvider;
        }

        protected IScheduler Scheduler {
            get { return _schedulerProvider.Scheduler; }
        }

        protected ISchedulerDataProvider SchedulerDataProvider {
            get { return _schedulerDataProvider; }
        }

        protected void HandleError(Exception exception, ErrorOutput output) {
            var schedulerProviderException = exception as SchedulerProviderException;
            if (schedulerProviderException != null) {
                NameValueCollection properties = schedulerProviderException.SchedulerInitialProperties;
                output.ErrorDetails = properties
                    .AllKeys
                    .Select(key => new Property(key, properties.Get(key)))
                    .ToArray();
            }
        }
    }

}
