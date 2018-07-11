using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Dto.Worklists {

    public class OOFWrapper {

        public OOFWrapper() {
            WorkType = new WorkTypeDto();
        }

        public bool Status { get; set; }
        public int ShareType { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public WorkTypeDto WorkType { get; set; }
    }

}
