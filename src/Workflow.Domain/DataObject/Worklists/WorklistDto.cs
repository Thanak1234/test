using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    public class WorklistDto {
        public string RequestorId { get; set; }
        public string Requestor { get; set; }
        public string Serial { get; set; }
        public string Originator { get; set; }
        public string ActivityName { get; set; }
        public string Folio { get; set; }
        public string WorkflowName { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ActivityStartDate { get; set; }
        public string Status { get; set; }
        public bool NoneK2Form { get; set; }
        public dynamic Actions { get; set; }
        public string Data { get; set; }
        public string ViewFlow { get; set; }
        public string ViewFromUrl { get; set; }
        public string OpenedBy { get; set; }
        public string AllocatedUser { get; set; }
        public string SharedUser { get; set; }
        public string ManagedUser { get; set; }
        public bool IsShared { get; set; }
        
        public int RequestHeaderId { get; set; }
        public string requestCode { get; set; }
        public long ProcInstId { get; set; }
    }
}
