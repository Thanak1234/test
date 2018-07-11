using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject {

    public class ResourceWrapper {
        public ResourceWrapper() {
            Records = new List<object>();
        }

        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public object Records { get; set; }
        public object Callback { get; set; }
    }
}
