using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.Worklists {
    public class SleepViewModel {
        public bool Basic { get; set; }
        public string SerialNumber { get; set; }
        public string SharedUser { get; set; }
        public string ManagedUser { get; set; }
        public dynamic Duration { get; set; }
    }
}
