using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Domain.Entities.INCIDENT;
namespace Workflow.DataAcess.Repositories.Incident
{
    public interface IICDRepository : IRepository<Workflow.Domain.Entities.INCIDENT.Incident>
    {

    }
}
