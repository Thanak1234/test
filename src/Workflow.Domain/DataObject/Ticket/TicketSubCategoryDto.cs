using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class TicketSubCategoryDto
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        
        [DataMember(Name = "subCateName")]
        public string subCateName { get; set; }
                
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime createdDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime modifiedDate { get; set; }

        
        [DataMember(Name = "cateDescription")]
        public string cateDescription { get; set; }
        
        [DataMember(Name = "cateName")]
        public string cateName { get; set; }

        [DataMember(Name = "cateId")]
        public int cateId { get; set; }
        
        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "statusId")]
        public int statusId { get; set; }

    }
}
