using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class MergedTicketActParam : AbstractTicketParam
    {
        public int MergedToTkId { get; set; }
    }
}
