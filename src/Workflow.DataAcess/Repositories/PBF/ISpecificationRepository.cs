using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.PBF;

namespace Workflow.DataAcess.Repositories.PBF
{
    public interface ISpecificationRepository : IRepository<Specification>
    {
        IList<Specification> GetByProjectId(int id);
    }
}
