using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.DataObject.Roles {
    public class TreeItemDto {

        private List<TreeItemDto> _children = null;

        public int key { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public bool leaf { get; set; }
        public string iconCls { get; set; }

        public String value { get; set; }
        public bool isDbRole { get; set; }

        public List<TreeItemDto> children {
            get {
                return _children ?? (_children = new List<TreeItemDto>());
            }
            set {
                _children = value;
            }
        }

        public bool ShouldSerializechildren() {
            return (children != null && children.Count() > 0);
        }
    }
}
