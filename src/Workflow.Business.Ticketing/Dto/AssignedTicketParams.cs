using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class AssignedTicketParams : AbstractTicketParam
    {
        
        public int TeamId { get; set; }
        public int Assignee { get; set; }

        //Activity data

        public bool IsNotify { get; set; } = false;


    }
}
