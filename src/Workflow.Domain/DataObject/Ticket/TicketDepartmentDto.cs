using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketDepartmentDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "deptName")]
        public string deptName { get; set; }

        [DataMember(Name = "hodId")]
        public int hodId { get; set; }

        [DataMember(Name = "defaultItemId")]
        public int defaultItemId { get; set; }

        [DataMember(Name = "internalUsed")]
        public bool internalUsed { get; set; }

        [DataMember(Name = "automationEmail")]
        public string automationEmail { get; set; }

        [DataMember(Name = "deptSignature")]
        public string deptSignature { get; set; }

        [DataMember(Name = "isDefault")]
        public bool isDefault { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

    }
}
