using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.IT
{
    public class ItemDto
    {
        public int id { get; set; }
        public int sessionId { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public bool hodRequired { get; set; }
    }
}
