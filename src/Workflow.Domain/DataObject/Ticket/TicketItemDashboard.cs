using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{

    [DataContract]
    public class TicketItemDashboard
    {

        public enum TICKET_ITEM_DHB { AGENT=1, TEAM=2, CATE =3, SUB_CATE = 4, ITEM =5, SOURCE=6 };

        [DataMember(Name = "itemName")]
        public string ItemName { get; set; }
        [DataMember(Name = "opened")]
        public int Opened { get; set; }
        [DataMember(Name = "onHold")]
        public int OnHold { get; set; }
        [DataMember(Name = "overDue")]
        public int OverDue { get; set; }
        [DataMember(Name = "unAssigned")]
        public int UnAssigned { get; set; }
    }
}
