using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Queue {
    public class FingerPrintMachine {
        public int Id { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public int MachineNo { get; set; }
        public DateTime? ConnectedDate { get; set; }
        public DateTime? LastConnectedDate { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
