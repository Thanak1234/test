using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Ticket
{
    public class TicketAgent : AbstractEntityLevel2
    {
       [DataMember(Name = "empId")]
        public int EmpId { get; set; }

        [DataMember(Name = "accountType")]
        public string AccountType { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "deptId")]
        public int DeptId { get; set; }

        [DataMember(Name = "groupPolicyId")]
        public int GroupPolicyId { get; set; }

        [DataMember(Name = "timeZone")]
        public int TimeZone { get; set; }

        [DataMember(Name = "limitAccess")]
        public bool LimitedAccess { get; set; }

        [DataMember(Name = "directoyListing")]
        public bool DirectoryListing { get; set; }

        [DataMember(Name = "vocationMode")]
        public bool VocationMode { get; set; }
    }
}

