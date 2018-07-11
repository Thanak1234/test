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
    public class Item
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public bool HodRequired { get; set; }
    }
}
