using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public interface ITicketDeptRepository : IRepository<TicketDepartment>
    {
        IEnumerable<TicketDepartmentDto> getDepartments(string query = null);
        Boolean isDepartmentExisted(TicketDepartment instance);
        IEnumerable<TicketDepartmentDto> getDepartments(TicketSettingCriteria criteria);
    }
}
