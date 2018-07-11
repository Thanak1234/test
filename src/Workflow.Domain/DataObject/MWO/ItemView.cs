using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.MWO {

    [DataContract]
    public class ItemView {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "itemCode")]
        public string ItemCode { get; set; }

        [DataMember(Name = "itemDescription")]
        public string ItemDescription { get; set; }

        [DataMember(Name = "unit")]
        public string Unit { get; set; }

        [DataMember(Name = "requestHeaderId")]
        public int RequestHeaderId { get; set; }

        [DataMember(Name = "partType")]
        public string PartType { get; set; }

        [DataMember(Name = "itemId")]
        public int ItemId { get; set; }

        [DataMember(Name = "unitPrice")]
        public double UnitPrice { get; set; }

        [DataMember(Name = "qtyRequested")]
        public double QtyRequested { get; set; }

        [DataMember(Name = "qtyIssued")]
        public double QtyIssued { get; set; }

        [DataMember(Name = "qtyReturn")]
        public double QtyReturn { get; set; }

        [DataMember(Name = "amount")]
        public double Amount { get; set; }

        [DataMember(Name = "modifiedBy")]
        public string ModifiedBy { get; set; }

        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime ModifiedDate { get; set; }
    }
}
