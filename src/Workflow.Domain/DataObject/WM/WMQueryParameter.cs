using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.WM {
    public class WMQueryParameter: QueryParameter {
        public string EmpNo { get; set; }
        public string Folio { get; set; }
        public string ProcName { get; set; }
        public string ActivityName { get; set; }
        public string Destination { get; set; }
        public DateTime? WorklistDate { get; set; }
    }
}
