using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    
    public class Action {
        
        public string Name {
            get;
            set;
        }
        public bool Batchable {
            get;
            set;
        }

        internal static Action FromApi(SourceCode.Workflow.Client.Action act) {
            return new Action {
                Name = act.Name,
                Batchable = act.Batchable
            };
        }
    }
}
