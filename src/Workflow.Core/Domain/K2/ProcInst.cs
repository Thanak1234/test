using System;
using System.Collections.Generic;

namespace Workflow.DataContract.K2
{
    public class ProcInst
    {
        public ProcInst() {
            Actions = new List<string>();
        }
        private string _status;

        public string Folio { get; set; }
        public string Serial { get; set; }
        public string ActivityName { get; set; }
        public List<string> Actions { get; set; }
        public string AllocatedUser { get; set; }
        public string OpenedBy { get; set; }
        public string Status {
            get {
                //Available = 0,
                //Open = 1,
                //Allocated = 2,
                //Sleep = 3,
                //Completed = 4
                return _status;
            }
            set {
                _status = value;
            }
        }
        public string OpenFormUrl { get; set; }

        public int RequestHeaderId { get; set; }
        public string requestCode { get; set; }
        public long ProcInstId { get; set; }
    }
}
