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
    public class ItemRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<int> Version { get; set; }
    }
}
