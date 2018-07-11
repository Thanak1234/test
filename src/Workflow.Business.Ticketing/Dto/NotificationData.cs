using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public class NotificationData
    {
        public List<TicketUserDto> Receipients { get; set; }
        public List<TicketUserDto> CarbonCopy { get; set; }
        public List<TicketUserDto> BlindCarbonCopy { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
