using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Workflow.Domain.Entities.BCJ
{
    [DataContract]
    [Table("BCJ_PO", Schema = "FINANCE")]
    public partial class PurchaseOrder
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        [DataMember(Name = "poNumber")]
        public string PoNumber { get; set; }
        [DataMember(Name = "poAmount")]
        public decimal PoAmount { get; set; }
        [DataMember(Name = "poDate")]
        public DateTime PoDate { get; set; }
    }
}