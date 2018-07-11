using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Reservation;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public class ComplimentaryItemRepository : RepositoryBase<ComplimentaryItem>, IComplimentaryItemRepository
    {
        public ComplimentaryItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
