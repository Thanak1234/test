using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Domain.Entities
{
    [Table("TRANSACTION_HISTORY", Schema = "SYSTEM")]
    public class TransactionHistory
    {
        public int Id { get; set; }
        public string ObjectType { get; set; }
        public int? ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string JsonData { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
