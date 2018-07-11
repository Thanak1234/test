using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class TicketUserDto
    {
        int Id { get; set; }
        string fullName { get; set; }
        string Email { get; set; }
        string phone { get; set; }
    }
}
