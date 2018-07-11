/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.IT
{
    public class RequestItem
    {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }

        public virtual RequestHeader RequestHeader { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; } 

        public Nullable<int> ItemTypeId { get; set; }
        public virtual ItemType ItemType { get; set; }

        public Nullable<int> ItemRoleId { get; set; }
        public virtual ItemRole ItemRole { get; set; }

        public Nullable<int> Qty { get; set; }
        public string Comment { get; set; }
        public Nullable<int> Version { get; set; }
    }
}
