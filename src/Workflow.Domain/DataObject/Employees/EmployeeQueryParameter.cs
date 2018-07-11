using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.DataObject.Employees {
    public class EmployeeQueryParameter: QueryParameter {
        public bool Integrated { get; set; }
        public string LoginName { get; set; }
        public bool ExcludeOwner { get; set; }
        public bool IncludeInactive { get; set; }
        public bool IncludeGenericAcct { get; set; }
        public int EmpId { get; set; }
    }
}
