using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Ticket
{
    public class ActivityDto
    {
        [DataMember(Name = "ticketId")]
        public int TicketId { get; set; }


        [DataMember(Name = "mergedToTkId")]
        public int MergedToTkId { get; set; }

        public EmployeeDto CurrUser { get; set; }

        [DataMember(Name = "activityCode")]
        public string ActivityCode { get; set; }
        public string ActionCode { get; set; }

        //Assignment
        [DataMember(Name = "teamId")]
        public int TeamId { get; set; }

        [DataMember(Name = "assignee")]
        public int Assignee { get; set; }

        [DataMember(Name = "statusId")]
        public int StatusId { get; set; }

        [DataMember(Name = "closeK2Form")]
        public bool CloseK2Form { get; set; }

        [DataMember(Name = "rootCause")]
        public int RootCauseId { get; set; }
        //Activity data

        public bool IsNotify { get; set; } = false;
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        [DataMember(Name = "actualMinutes")]
        public decimal? ActualMinutes { get; set; }

        
        [DataMember(Name = "fileUploads")]
        public List<FileUploadInfo> FileUploads { get; set; }
    }
}
