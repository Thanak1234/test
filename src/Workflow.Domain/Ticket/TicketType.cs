using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketType : AbstractEntityLevel2
    {

        [DataMember(Name = "typeName")]
        public string TypeName { get; set; }

        [DataMember(Name ="icon")]
        public string Icon { get; set; }
    }
}
