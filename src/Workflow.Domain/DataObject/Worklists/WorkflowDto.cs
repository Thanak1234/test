using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {
    public class WorkflowDto {
        public string RequestDescription { get; set; }
        public string ProcessName { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDisplayName { get; set; }
    }
}
