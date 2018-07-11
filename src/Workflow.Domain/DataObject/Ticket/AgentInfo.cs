using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class AgentInfo
    {
        public int AgentId { get; set; }
        public int EmpId { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string AssignedNotify { get; set; }
        public string ChangeStatusNofify { get; set; }
        public string NewTicketNotify { get; set; }
        public string ReplyNotify { get; set; }
    }
}
