using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Queue {
    public class FingerPrint {
        public int Id { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public int MachineNo { get; set; }
        public string EnrolmentNo { get; set; }
        public int AttState { get; set; }
        public int VerifyMethod { get; set; }
        public int IsInvalid { get; set; }
        public int WorkCode { get; set; }
        public DateTime MachineDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public override string ToString() {
            return string.Format("Id={0},IP={1},Port={2},MachineNo={3},EnrolmentNo={4},AttState={5},VerifyMethod={6},IsInvalid={7},MachineDate={8},CreatedDate={9},Status={10}", Id, IP, Port, MachineNo, EnrolmentNo, AttState, VerifyMethod, IsInvalid, MachineDate, CreatedDate, Status);
        }
    }
}
