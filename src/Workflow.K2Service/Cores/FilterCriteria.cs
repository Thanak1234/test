using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class FilterCriteria {
        public string Logical { get; set; }
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
