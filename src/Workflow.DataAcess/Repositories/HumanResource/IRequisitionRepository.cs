using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.HumanResource;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public interface IRequisitionRepository : IRepository<Requisition>
    {
        Requisition GetByRequestHeader(int id);
    }
}
