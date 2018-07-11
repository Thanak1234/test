/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.ItRequestForm
{
    public class RequestItemViewModel
    {
        public int id { get; set; }
        public int itemId { get; set; }
        public int? sessionId { get; set; }
        public string session { get; set; }
        public string itemName { get; set; }
        public Nullable<int> itemTypeId { get; set; }
        public string itemTypeName { get; set; }
        public Nullable<int> itemRoleId { get; set; }
        public string itemRoleName { get; set; }
        public int? qty { get; set; }
        public string comment { get; set; }
     }
}
