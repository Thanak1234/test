using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    public class WorklistItemDto {

        public int ProcessInstanceID { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string Folio { get; set; }
        public string ActionerName { get; set; }
        public string Workflow { get; set; }
        public string ActivityName { get; set; }
        public string OpenedBy { get; set; }
        public DateTime TaskStartDate { get; set; }
        public string Priority { get; set; }
        public string SeriaNumber { get; set; }
        public string SharedUser { get; set; }
        public string ManagedUser { get; set; }
        public string ActionName { get; set; }
        public bool Batchable { get; set; }
        public bool Denied { get; set; }
        public bool Execute { get; set; }
        public bool NoneK2 { get; set; }        
        public int Status { get; set; }
        public string ViewFLow { get; set; }
        public string ViewUrl { get; set; }
        public string OpenUrl { get; set; }
        public int ActInstDestID { get; set; }
        public string Originator { get; set; }
    }
}
