using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public interface IComplimentaryRepository : IRepository<Complimentary>
    {
        Complimentary GetByRequestHeader(int id);
    }
}
