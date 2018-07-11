using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TKNotifyDto
    {
        public int NotifyId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityType { get; set; }
        public string ActivityName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
