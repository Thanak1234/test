/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.AvbRequestForm
{
    public class RequestItemViewModel
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int itemTypeId { get; set; }
        public string itemTypeName { get; set; }
        public string itemTypeDescription { get; set; }
        public int quantity { get; set; }
        public string comment { get; set; } 
    }
}
