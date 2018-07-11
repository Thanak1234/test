using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public interface ITicketingRepository : IRepository<TicketItem>
    {
        object GetReportResult(TicketingSearchParamsDto ticketingSearchDto);
    }
}
