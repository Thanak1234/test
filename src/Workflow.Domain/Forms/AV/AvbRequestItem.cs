/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.AV
{
    public class AvbRequestItem
    {

        public int Id { get; set; }

        public int RequestHeaderId { get; set; }

        public virtual RequestHeader RequestHeader { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }
        
        public virtual AvbItem Item { get; set; }

        public string Comment { get; set; }

    }
}
