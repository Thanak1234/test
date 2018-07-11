using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketRoutingRepository : RepositoryBase<TicketRouting>, ITicketRoutingRepository
    {
        public TicketRoutingRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public void releaseAssignedRouting(int ticketId)
        {
            var sql = "";
            DbContext.Database.ExecuteSqlCommand(sql, new SqlParameter("@ticketId",ticketId));
        }
    }
}
