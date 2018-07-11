using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Workflow.DataObject.Dashboard {
    [DataContract]
    public class TaskDto {

        [DataMember(Name = "id")]
        public int? ID { get; set; }

        [DataMember(Name = "folio")]
        public string FOLIO { get; set; }

        [DataMember(Name = "requestCode")]
        public string REQUEST_CODE { get; set; }

        [DataMember(Name = "appName")]
        public string APP_NAME { get; set; }

        [DataMember(Name = "submittedBy")]
        public string SUBMITTED_BY { get; set; }

        [DataMember(Name = "submittedDate")]
        public DateTime? SUBMITTED_DATE { get; set; }

        [DataMember(Name = "lastActivity")]
        public string LAST_ACTIVITY { get; set; }

        [DataMember(Name = "lastActionDate")]
        public DateTime? LAST_ACTION_DATE { get; set; }

        [DataMember(Name = "lastActionBy")]
        public string LAST_ACTION_BY { get; set; }

        [DataMember(Name = "status")]
        public string STATUS { get; set; }

        [DataMember(Name = "processInstanceId")]
        public int? PROCESS_INSTANCE_ID { get; set; }

        [DataMember(Name = "formUrl")]
        public string FORM_URL { get; set; }

        [DataMember(Name = "requestor")]
        public string REQUESTOR { get; set; }

        [DataMember(Name = "noneSmartFromUrl")]
        public string NONE_SMART_FORM_URL { get; set; }

        [DataMember(Name = "noneK2")]
        public bool? NONE_K2 { get; set; }

        [DataMember(Name = "flowUrl")]
        public string FLOW_URL { get; set; }
    }
}
