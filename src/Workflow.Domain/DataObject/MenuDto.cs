using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class MenuDto
    {

        private List<MenuDto>  _children =null;

        public string text { get; set; }
        public string desc { get; set; }
        public string view { get; set; }
        public bool leaf { get; set; }
        public bool NoChild { get; set; }
        public string routeId { get; set; }
        public string iconCls { get; set; }
        public bool closableTab { get; set; }
        public List<MenuDto> children {
            get{
                return _children ?? (_children = new List<MenuDto>());
            }
            set
            {
                _children = value;
            }
        }
        public string url { get; set; }

    }
}
