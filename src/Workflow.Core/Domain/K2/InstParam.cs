using System.Collections.Generic;

namespace Workflow.DataContract.K2
{
    public class InstParam {
        
        public string ProcName { get; set; }
        public string Folio { get; set; }
        public IDictionary<string, object> DataFields { get; set; }
        public Priority Priority { get; set; }
        public string CurrentUser { get; set; }
    }

    public class ExecInstParam
    {
        public string SerialNo { get; set; }
        public string Action { get; set; }
        public IDictionary<string, object> DataFields { get; set; }
    }
}
