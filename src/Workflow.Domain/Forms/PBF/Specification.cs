/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.PBF
{
    [DataContract]
    public class Specification
    {
        private string _name;

        [DataMember(Name = "id")]
        public int Id { get; set; }
        [IgnoreDataMember]
        public int ProjectBriefId { get; set; }
        [IgnoreDataMember]
        public virtual ProjectBrief ProjectBrief { get; set; }
        [DataMember(Name = "itemId")]
        public int ItemId { get; set; }
        [DataMember(Name = "item")]
        public virtual Lookup Item { get; set; }
        [DataMember(Name = "name")]
        public string Name {
            get {
                return Item != null ? Item.Value : _name;
            }
            set {
                _name = Item != null ? Item.Value : null;
            }
        }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
        [DataMember(Name = "itemRef")]
        public string ItemReference { get; set; }

    }
}
