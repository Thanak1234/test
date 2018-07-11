using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class CriteriaFilter {
        public CriteriaComparison Comparison {
            get;
            set;
        }
        public CriteriaField Field {
            get;
            set;
        }
        public CriteriaLogical Logical {
            get;
            set;
        }
        public string SubField {
            get;
            set;
        }
        public string Value {
            get;
            set;
        }
        public ValueType ValueType {
            get;
            set;
        }
        public string DateType {
            get;
            set;
        }
        public string StartDate {
            get;
            set;
        }
        public string EndDate {
            get;
            set;
        }
    }
}
