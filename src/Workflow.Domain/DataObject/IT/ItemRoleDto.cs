using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.IT
{
    public class ItemRoleDto
    {
        public int id { get; set; }
        public string roleName { get; set; }
        public string description { get; set; }
        public Nullable<bool> isAdmin { get; set; }
    }
}
