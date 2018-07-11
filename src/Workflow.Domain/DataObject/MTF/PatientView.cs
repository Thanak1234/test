using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.MTF {
    public class PatientView {

        public string FOLIO { get; set; }

        public string PATIENT { get; set; }

        public DateTime? LAST_ACTION_DATE { get; set; }
    }
}
