using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Worklists {

    [DataContract]
    public class InstanceAuditDto {

        [DataMember(Name = "procInstId")]
        public int PROCESS_INSTANCE_ID { get; set; }

        [DataMember(Name = "actInstId")]
        public int ACTIVITY_INSTANCE_ID { get; set; }

        [DataMember(Name = "procName")]
        public string PROCESS_NAME { get; set; }

        [DataMember(Name = "actName")]
        public string ACTIVITY_NAME { get; set; }

        [DataMember(Name = "folio")]
        public string FOLIO { get; set; }

        [DataMember(Name = "user")]
        public string USER_NAME { get; set; }

        [DataMember(Name = "date")]
        public DateTime DATE { get; set; }

        [DataMember(Name = "auditDesc")]
        public string AUDIT_DESCRIPTION { get; set; }

    }
}
