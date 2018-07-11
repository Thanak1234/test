using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.INCIDENT;
namespace Workflow.DataAcess.Repositories.Incident
{
    public interface IICDIncidentEmployeeRepository : IRepository<IncidentEmployee>
    {
        IEnumerable<RequestUserExt> GetIncidentEmployeeList(int requestHeaderId);
    }
}
