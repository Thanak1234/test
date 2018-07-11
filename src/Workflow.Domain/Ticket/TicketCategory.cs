using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketCategory : AbstractEntityLevel2
    {
        [DataMember(Name ="deptId")]
        public int DeptId { get; set; }
        [DataMember(Name = "cateName")]
        public string CateName { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
