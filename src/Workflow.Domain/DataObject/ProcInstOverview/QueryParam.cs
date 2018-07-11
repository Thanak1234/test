using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.ProcInstOverview {
    public class ProcInstOverviewQueryParam: QueryParameter {
        public int procInstId { get; set; }
        public int actInstId { get; set; }
        public string actName { get; set; }
        public string procFullName { get; set; }
    }
}
