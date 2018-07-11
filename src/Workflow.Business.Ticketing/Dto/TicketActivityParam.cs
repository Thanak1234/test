using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class TicketActivityParam
    {

        public string ActionCode { get; set; }

        //Assignment

        public int TeamId { get; set; }
        public int Assignee { get; set; }


        public float EstimatedHours { get; set; }
        public DateTime DueDate { get; set; }
        //Activity data

        public bool IsNotify { get; set; } = false;
        public string Comment { get; set; }
        public List<string> AttachmentSerial { get; set; }
    }
}
