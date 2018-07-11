using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.MTF {
    public class GraphView {
        public int YEAR { get; set; }
        public int MONTH { get; set; }
        public int FIT_TO_WORK { get; set; }
        public int UNFIT_TO_WORK { get; set; }
        public int ALL_PATIENTS { get; set; }
        public string DATEPATH { get; set; }
    }
}
