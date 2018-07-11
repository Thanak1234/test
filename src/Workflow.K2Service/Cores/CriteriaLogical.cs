using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public enum CriteriaLogical {
        And,
        Or,
        AndBracket,
        OrBracket = 4,
        StartBracket = 8,
        EndBracket = 16
    }
}
