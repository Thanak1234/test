namespace Workflow.Service.Interfaces
{
    public interface ISchedulerService {
        void UpdateCronExpression(string jobName, string cronExprs);
        void UpdateStatus(string jobName, int status);
    }
}
