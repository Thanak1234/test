using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.WM {

    [DataContract]
    public class WLItemDto {
        [DataMember(Name = "rowNumber")]
        public int RowNumber { get; set; }

        [DataMember(Name = "id")]
        public int ID { get; set; }

        [DataMember(Name = "procName")]
        public string ProcName { get; set; }

        [DataMember(Name = "eventName")]
        public string EventName { get; set; }

        [DataMember(Name = "activityName")]
        public string ActivityName { get; set; }

        [DataMember(Name = "folio")]
        public string Folio { get; set; }

        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "procInstID")]
        public int ProcInstID { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        [DataMember(Name = "procInstStatus")]
        public int ProcInstStatus { get; set; }

        [DataMember(Name = "employeeNo")]
        public string EmployeeNo { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

    }

}
