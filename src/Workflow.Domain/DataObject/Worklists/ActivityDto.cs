using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    public class ActivityDto : DtoBase {
        public string RequestCode { get; set; }
        public string RequestDesc { get; set; }
        public string ProcessPath { get; set; }
        public string ProcessName { get; set; }
        public int? WorkflowId { get; set; }
        public string Type { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ActCode { get; set; }
        public string Property { get; set; }
        public int? IconIndex { get; set; }
        public int? Sequence { get; set; }
        public bool? Active { get; set; }
    }
}
