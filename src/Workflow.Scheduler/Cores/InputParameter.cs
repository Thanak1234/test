using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Scheduler.Cores {
    public class InputParameter {
        public string Group { get; set; }
        public string Job { get; set; }
        public string Trigger { get; set; }
        public string CronExpression { get; set; }
        public string Method { get; set; }
    }
}
