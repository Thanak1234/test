using CrystalQuartz.Core;
using CrystalQuartz.Core.SchedulerProviders;
using log4net;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Scheduler.Cores;

namespace Workflow.Scheduler {
    public class SchedulerCommand: AbstractSchedulerCommand, ISchedulerCommand {

        protected ILog _Logger = LogManager.GetLogger("SchedulerCommand");

        public SchedulerCommand(ISchedulerProvider schedulerProvider): 
            base(schedulerProvider, new DefaultSchedulerDataProvider(schedulerProvider)) {
        }

        public bool DeleteGroup(InputParameter param) {
            var keys = Scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(param.Group));
            return Scheduler.DeleteJobs(keys.ToList());
        }

        public bool DeleteJob(InputParameter param) {
            return Scheduler.DeleteJob(new JobKey(param.Job, param.Group));
        }

        public bool DeleteTrigger(InputParameter param) {
            var triggerKey = new TriggerKey(param.Trigger, param.Group);
            return Scheduler.UnscheduleJob(triggerKey);
        }

        public void ExecuteNow(InputParameter param) {
            _Logger.Info(string.Format("{0} execute command.", param.Job));
            Scheduler.TriggerJob(new JobKey(param.Job, param.Group));
        }

        public JobDetailsOutput GetJobDetails(InputParameter param) {
            var output = new JobDetailsOutput();
            var detailsData = SchedulerDataProvider.GetJobDetailsData(param.Job, param.Group);

            output.JobDataMap = detailsData
                .JobDataMap
                .Select(pair => new Property(pair.Key.ToString(), pair.Value))
                .ToArray();

            output.JobProperties = detailsData
                .JobProperties
                .Select(pair => new Property(pair.Key, pair.Value))
                .ToArray();
            return output;
        }

        public SchedulerDataOutput GetSchedulerData() {
            var output = new SchedulerDataOutput();
            SchedulerDataProvider.Data.MapToOutput(output);
            return output;
        }

        public void PauseGroup(InputParameter param) {
            Scheduler.PauseJobs(GroupMatcher<JobKey>.GroupEquals(param.Group));
        }

        public void PauseJob(InputParameter param) {
            Scheduler.PauseJob(new JobKey(param.Job, param.Group));
        }

        public void PauseTrigger(InputParameter param) {
            var triggerKey = new TriggerKey(param.Trigger, param.Group);
            Scheduler.PauseTrigger(triggerKey);
        }

        public void ResumeGroup(InputParameter param) {
            Scheduler.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(param.Group));
        }

        public void ResumeJob(InputParameter param) {
            Scheduler.ResumeJob(new JobKey(param.Job, param.Group));
        }

        public void ResumeTrigger(InputParameter param) {
            var triggerKey = new TriggerKey(param.Trigger, param.Group);
            Scheduler.ResumeTrigger(triggerKey);
        }

        public void Reschedule(InputParameter param) {
            var triggerKey = new TriggerKey(param.Trigger, param.Group);
            var oTrigger = Scheduler.GetTrigger(triggerKey);
            var nTrigger = oTrigger.GetTriggerBuilder().WithCronSchedule(param.CronExpression).Build();
            Scheduler.RescheduleJob(triggerKey, nTrigger);
        }

        public void StartScheduler() {
            Scheduler.Start();
        }

        public void StopScheduler() {
            Scheduler.Shutdown(false);
        }
    }
}
