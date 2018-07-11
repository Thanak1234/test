using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.DataObject.Dashboard {
    public class TaskQueryParameter: QueryParameter {
        public bool IsAssigned { get; set; }
        public string ContributedBy { get; set; }
        public string SubmittedBy { get; set; }
    }
}
