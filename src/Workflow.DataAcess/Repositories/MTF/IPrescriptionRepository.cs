using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        IList<Prescription> GetByTreatmentId(int id);
    }
}
