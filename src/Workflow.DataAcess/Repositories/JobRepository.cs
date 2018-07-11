using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Scheduler;

namespace Workflow.DataAcess.Repositories {
    public class JobRepository : RepositoryBase<Job>, IJobRepository {

        public JobRepository(IDbFactory dbFactory): base(dbFactory) {

        }

        public void UpdateCronExpression(string jobName, string cronExprs) {
            var entity = DbContext.Set<Job>().Where(p => p.Name.ToLower() == jobName.ToLower()).FirstOrDefault();
            if(entity != null && !string.IsNullOrEmpty(cronExprs)) {
                entity.CronExpression = cronExprs;
                Update(entity);
            }
        }

        public void UpdateStatus(string jobName, int status) {
            var entity = DbContext.Set<Job>().Where(p => p.Name.ToLower() == jobName.ToLower()).FirstOrDefault();
            if (entity != null) {
                entity.Status = status;
                Update(entity);
            }
        }

    }
}
