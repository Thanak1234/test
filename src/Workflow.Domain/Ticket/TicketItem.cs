using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketItem : AbstractEntityLevel2
    {
         [DataMember(Name = "subCateId")]
        public int SubCateId { get; set; }

        [DataMember(Name = "itemName")]
        public string ItemName { get; set; }

        public int TicketTypeId { get; set; }

        [DataMember(Name = "slaId")]
        public int SLAId { get; set; }

        [DataMember(Name = "teamId")]
        public int TeamId { get; set; }
        
        public Boolean IsDefault { get; set; }
        
        public Boolean IsActive { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
