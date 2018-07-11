using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class Criteria {

        public string ManagedUser {
            get;
            set;
        }
        public string Platform {
            get;
            set;
        }
        public long Count {
            get;
            set;
        }
        public long StartIndex {
            get;
            set;
        }
        public CriteriaFilterCollection Filters {
            get;
            set;
        }
        public CriteriaSortCollection Sorts {
            get;
            set;
        }
        public WorklistCriteria ToApi() {
            WorklistCriteria worklistCriteria = new WorklistCriteria();
            worklistCriteria.Count = (int)this.Count;
            worklistCriteria.StartIndex = (int)this.StartIndex;
            if (worklistCriteria.Count == 0) {
                worklistCriteria.Count = -1;
            }
            worklistCriteria.ManagedUser = this.ManagedUser;
            worklistCriteria.NoData = true;
            worklistCriteria.Platform = this.Platform;
            foreach (CriteriaFilter current in this.Filters) {
                object obj = current.Value;
                obj = K2Convert.FromString(current.Value, (DataType)current.ValueType);
                if (obj is string && (current.Comparison == CriteriaComparison.Like || current.Comparison == CriteriaComparison.NotLike)) {
                    obj = "*" + (string)obj + "*";
                }
                worklistCriteria.AddFilterField((WCLogical)current.Logical, (WCField)current.Field, current.SubField, (WCCompare)current.Comparison, obj);
            }
            foreach (CriteriaSort current2 in this.Sorts) {
                worklistCriteria.AddSortField((WCField)current2.Field, current2.SubField, (WCSortOrder)current2.SortOrder);
            }
            return worklistCriteria;
        }
    }

}
