using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketDepartment : AbstractEntityLevel2
    {
       [DataMember(Name = "deptName")]
        public string DeptName { get; set; }

        [DataMember(Name = "hodId")]
        public int HoDId { get; set; }

        [DataMember(Name = "defaultItemId")]
        public int DefaultItemId { get; set; }

        [DataMember(Name = "internalUserd")]
        public bool InternalUsed { get; set; }

        [DataMember(Name = "automationEmail")]
        public string AutomationEmail { get; set; }

        [DataMember(Name = "deptSignature")]
        public string DeptSignature { get; set; }

        [DataMember(Name = "isDefault")]
        public bool IsDefault { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
        
    }
}
