using System;
using System.Runtime.Serialization;
using Workflow.DataObject.Attributes;

namespace Workflow.DataObject.Reports
{
    [DataContract]
    //[Report(Name ="ITIRF Process Instance", Path = "K2Report/Reports/PROC_INST_ITIRF")]
    public class ITIRFProcInst : ProcInst
    {
        [DataMember(Name = "ItemName")]
        public string ITEM_NAME { get; set; }

        [DataMember(Name = "ItemModel")]
        public string ITEM_MODEL { get; set; }

        [DataMember(Name = "SerialNo")]
        public string SERIAL_NO { get; set; }

        [DataMember(Name = "PartNo")]
        public string PART_NO { get; set; }

        [DataMember(Name = "Qty")]
        public int? QTY { get; set; }
        
        [DataMember(Name = "SendDate")]
        public DateTime? SEND_DATE { get; set; }
        
        [DataMember(Name = "ReturnDate")]
        public DateTime? RETURN_DATE { get; set; }

        [DataMember(Name = "Vendor")]
        public string VENDOR { get; set; }

        [DataMember(Name = "ContactNumber")]
        public string CONTACT_NUMBER { get; set; }

        [DataMember(Name = "Email")]
        public string EMAIL { get; set; }

        [DataMember(Name = "Address")]
        public string ADDRESS { get; set; }

        [DataMember(Name = "Remark")]
        public string REMARK { get; set; }      
    }
}
