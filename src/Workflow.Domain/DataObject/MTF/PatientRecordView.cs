using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.MTF {
    public class PatientRecordView {

        public int ID { get; set; }

        public int REQUEST_HEADER_ID { get; set; }

        public int PROCESS_INSTANCE_ID { get; set; }

        public int SEQUENCE_ID { get; set; }

        public int CHECK_IN_ID { get; set; }

        public int CHECK_OUT_ID { get; set; }

        public DateTime? CHECK_IN_DATE { get; set; }

        public DateTime? CHECK_OUT_DATE { get; set; }

        public string QUEUE_STATUS { get; set; }

        public int PRIORITY { get; set; }

        public string FOLIO { get; set; }

        public int REQUESTOR { get; set; }

        public string PATIENT { get; set; }

        public string REQUESTOR_NO { get; set; }

        public string PATIENT_NAME { get; set; }

        public DateTime? LAST_ACTION_DATE { get; set; }

        public DateTime? LAST_MODIFIED_DATE { get; set; }
    }
}
