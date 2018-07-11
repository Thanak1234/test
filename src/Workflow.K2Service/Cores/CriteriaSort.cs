using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class CriteriaSort {
        public CriteriaField Field {
            get;
            set;
        }
        public CriteriaSortOrder SortOrder {
            get;
            set;
        }
        public string SubField {
            get;
            set;
        }
    }
}
