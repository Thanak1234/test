/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public class RequestHeader: AbstractBaseEntity
    {

        public RequestHeader()
        {
            ActivityHistories = new List<ActivityHistory>();
            NoneK2 = true;
        }

        [DataMember(Name = "ProcessInstanceId")]
        public int ProcessInstanceId { get; set; }

        [DataMember(Name = "RequestorId")]
        public int RequestorId { get; set; }

        [DataMember(Name = "Requestor")]
        public virtual Employee Requestor { get; set; }

        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [DataMember(Name = "RequestCode")]
        public string RequestCode { get; set; }

        [DataMember(Name = "Priority")]
        public short Priority { get; set; }

        [DataMember(Name = "SubmittedBy")]
        public string SubmittedBy { get; set; }

        //public Nullable<System.DateTime> SubmittedDate { get; set; }

        [DataMember(Name = "Status")]
        public string Status { get; set; }

        [DataMember(Name = "LastActivity")]
        public string LastActivity { get; set; }

        [DataMember(Name = "LastActionDate")]
        public Nullable<System.DateTime> LastActionDate { get; set; }

        [DataMember(Name = "LastActionBy")]
        public string LastActionBy { get; set; }

        [DataMember(Name = "Version")]
        public Nullable<int> Version { get; set; }

        [DataMember(Name = "ActivityHistories")]
        public virtual ICollection<ActivityHistory> ActivityHistories { get; set; }

        [DataMember(Name = "NoneK2")]
        public bool? NoneK2 { get; set; }

        [DataMember(Name = "CurrentActivity")]
        public string CurrentActivity { get; set; }
    }
}
