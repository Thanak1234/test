using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class SchedulerService: ISchedulerService {

        private IJobRepository _JobRepository;

        public SchedulerService() {
            IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _JobRepository = new JobRepository(dbFactory);
        }

        public void UpdateCronExpression(string jobName, string cronExprs) {
            _JobRepository.UpdateCronExpression(jobName, cronExprs);
        }

        public void UpdateStatus(string jobName, int status) {
            _JobRepository.UpdateStatus(jobName, status);
        }
    }
}
