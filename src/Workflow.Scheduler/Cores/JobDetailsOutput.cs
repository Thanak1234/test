using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Scheduler.Cores {
    public class JobDetailsOutput {
        public Property[] JobDataMap { get; set; }

        public Property[] JobProperties { get; set; }
    }
}
