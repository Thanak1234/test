using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketSite : AbstractEntityLevel2
    {

        [DataMember(Name = "siteName")]
        public string SiteName { get; set; }
    }
}
