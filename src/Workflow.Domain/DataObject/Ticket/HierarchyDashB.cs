using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{

    [DataContract]
    public class HierarchyDashB
    {

        public enum TIME_FRAME
        {
            DAILY=1,
            WEEKLY=2,
            MONTHLY = 3,
            QUATERLY =4,
            YEARLY=5
        }

        public enum TIME_FILTER
        {
            CURR_MONTH = 101,
            LAST_MONTH = 102,
            LAST_MONTH_TODAY = 103,

            CURR_QUATER = 201,
            LAST_QUATER = 202,
            LAST_QUATER_TODAY =203,

            CURR_YEAR=301,
            LAST_YEAR = 302

        }

        [DataMember(Name = "time")]
        public string Time { get; set; }
        [DataMember(Name = "keyPath")]
        public string KeyPath { get; set; }
        [DataMember(Name = "value")]
        public int Value { get; set; }
    }
}
