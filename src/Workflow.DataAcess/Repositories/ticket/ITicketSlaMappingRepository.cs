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
    public interface ITicketSlaMappingRepository : IRepository<TicketSLAMapping>
    {   
        
        IEnumerable<TicketSlaMappingDto> getSlasMapping(string query = null);
        Boolean isSlaMappingExisted(TicketSLAMapping instance);
    }
}
