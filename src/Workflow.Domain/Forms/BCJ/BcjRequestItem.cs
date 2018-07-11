using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.BCJ
{
    [DataContract]
    public class BcjRequestItem
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public int ProjectDetailId { get; set; }

        [IgnoreDataMember]
        public virtual ProjectDetail ProjectDetail { get; set; }

        [DataMember(Name = "item")]
        public string Item { get; set; }

        [DataMember(Name = "unitPrice")]
        public decimal UnitPrice { get; set; }

        [DataMember(Name = "quantity")]
        public double Quantity { get; set; }

        [IgnoreDataMember]
        public decimal Total { get; set; }
    }
}
