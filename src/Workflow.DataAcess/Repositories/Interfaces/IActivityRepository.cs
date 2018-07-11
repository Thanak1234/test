using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IActivityRepository: IRepository<Activity> {
        string GetSubmissionConfig(string req, string activty);
    }
}
