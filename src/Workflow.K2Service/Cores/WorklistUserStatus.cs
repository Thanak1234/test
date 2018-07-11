using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class WorklistUserStatus {
        public bool Status {
            get;
            set;
        }
        public System.Collections.Generic.List<OOFUser> users {
            get;
            set;
        }
    }
}
