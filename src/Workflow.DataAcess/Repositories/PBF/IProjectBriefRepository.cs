using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.PBF;

namespace Workflow.DataAcess.Repositories.PBF
{
    public interface IProjectBriefRepository : IRepository<ProjectBrief>
    {
        ProjectBrief GetByRequestHeader(int id);
    }
}
