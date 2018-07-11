using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Dto.Worklists {

    public class FilterDto {
        public object Value { get; set; }
        public int Compare { get; set; }
        public string SubField { get; set; }
        public int Field { get; set; }
        public int Logical { get; set; }
    }

}
