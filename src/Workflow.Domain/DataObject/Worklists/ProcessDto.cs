using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    public class ProcessDto : DtoBase {

        public string RequestCode { get; set; }

        public string RequestDesc { get; set; }

        public string ProcessName { get; set; }

        public string ProcessCode { get; set; }

        public string ProcessPath { get; set; }

        public string FormUrl { get; set; }

        public int GenId { get; set; }

        public Nullable<int> IconIndex { get; set; }

        public Nullable<bool> Active { get; set; }
    }

}
