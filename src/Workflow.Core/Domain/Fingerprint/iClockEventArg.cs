using System;

namespace Workflow.DataContract.Fingerprint
{
    public class iClockEventArg {
        public string IP { get; set; }
        public int Port { get; set; }
        public string EnrollNumber { get; set; }
        public int IsInValid { get; set; }
        public int AttState { get; set; }
        public int VerifyMethod { get; set; }
        public DateTime CreatedDate { get; set; }
        public int WorkCode { get; set; }
        public int MachineNo { get; set; }
    }
}
