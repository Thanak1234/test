using Quartz;

namespace Workflow.Scheduler.Jobs {
    public interface IScheduleJob: IJob {
        void Register(IScheduler scheduler);
    }
}