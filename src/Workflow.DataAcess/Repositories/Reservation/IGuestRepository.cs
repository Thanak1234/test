using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public interface IGuestRepository : IRepository<Guest>
    {
        //IEnumerable<Guest> GetByRequestHeader(int id);
    }
}
