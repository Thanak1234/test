/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public class Menu : AbstractBaseEntity
    {
        public Menu(): base() {}
        public int ParentId { get; set; }
        public int RightId { get; set; }
        public string MenuName { get; set; }
        public string MenuDesc { get; set; }
        public string Workflow { get; set; }
        public bool IsWorkflow { get; set; }
        public string MenuUrl { get; set; }
        public string MenuIcon { get; set; }
        public string MenuClass { get; set; }
        public int Sequence { get; set; }
        public bool NoChild { get; set; }
        public bool Active { get; set; }
        public string RouteId { get; set; }
        public string ViewClass { get; set; }
        public bool ClosableTab { get; set; }
        public string IconCls { get; set; }
    }
}
