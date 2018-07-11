using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Scheduler;

namespace Workflow.DataAcess.Repositories.Interfaces {

    public interface IJobRepository: IRepository<Job> {
        void UpdateCronExpression(string jobName, string cronExprs);
        void UpdateStatus(string jobName, int status);
    }

}
