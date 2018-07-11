using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class DashboardService : IDashboardService {

        private readonly ITaskRepository _taskRepository;

        public DashboardService(string userAccount = null) {
            IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _taskRepository = new TaskRepository(dbFactory);
        }

        public ResourceWrapper GetTasksByLoginName(TaskQueryParameter queryParameter) {
            return _taskRepository.GetTasksByLoginName(queryParameter);
        }
    }
}
