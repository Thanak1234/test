using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Scheduler.Cores;

namespace Workflow.Scheduler
{
    public interface ISchedulerCommand
    {
        bool DeleteGroup(InputParameter param);
        bool DeleteJob(InputParameter param);
        bool DeleteTrigger(InputParameter param);
        void ExecuteNow(InputParameter param);
        JobDetailsOutput GetJobDetails(InputParameter param);
        void PauseGroup(InputParameter param);
        void PauseJob(InputParameter param);
        void PauseTrigger(InputParameter param);
        void ResumeGroup(InputParameter param);
        void ResumeJob(InputParameter param);
        void ResumeTrigger(InputParameter param);
        void StartScheduler();
        void StopScheduler();
        SchedulerDataOutput GetSchedulerData();
        void Reschedule(InputParameter param);
    }
}
