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
    public interface ITicketCategoryRepository : IRepository<TicketCategory>
    {   
        IEnumerable<TicketCategoryDto> getCategories(string query = null);
        Boolean isCategoryExisted(TicketCategory instance);
        IEnumerable<TicketCategoryDto> getCategories(TicketSettingCriteria criteria);
    }
}
