using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    public class WlCriteriaDto {
        public byte[] Criteria { get; set; }
        public int ActionerId { get; set; }
        public int Flag { get; set; }
    }
}
