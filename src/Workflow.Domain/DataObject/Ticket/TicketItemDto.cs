using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketItemDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "subCateId")]
        public int subCateId { get; set; }

        [DataMember(Name = "itemName")]
        public string itemName { get; set; }

        public string itemDisplayName { get; set; }

        [DataMember(Name = "teamId")]
        public int teamId { get; set; }

        [DataMember(Name = "teamName")]
        public string teamName { get; set; }

        [DataMember(Name = "slaId")]
        public int slaId { get; set; }
        
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        [DataMember(Name = "subCateName")]
        public string subCateName { get; set; }

        [DataMember(Name = "subCateDescription")]
        public string subCateDescription { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

    }
}
