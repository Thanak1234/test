using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketSubCategory : AbstractEntityLevel2
    {
        [DataMember(Name ="cateId")]
        public int CateId { get; set; }

        [DataMember(Name = "subCateName")]
        public string SubCateName { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
