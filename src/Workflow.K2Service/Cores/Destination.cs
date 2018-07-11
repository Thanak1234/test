using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {

    public class Destination {

        public string Name {
            get;
            set;
        }

        public static Destination FromApi(SourceCode.Workflow.Client.Destination dest) {
            return new Destination {
                Name = dest.Name
            };
        }
    }
}
