using System;
using System.Collections.Generic;

namespace Workflow.DataObject.Ticket
{
    public abstract class AbstractActivityViewDto
    {
        public int id { get; set; }
        public string activityType { get; set; }
        public string action { get; set; }
        public string actionBy { get; set; }

        public string subject { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public int version { get; set; }

        public string team { get; set; }
        public string assignee { get; set; }
        public bool assignedExpired { get; set; }
        public string empNoAssignee { get; set; }
        public string activityName { get; set; }

        public string moreData { get; set; }

        public object addData { get; set; }

        public List<FileUploadInfo> fileUpload { get; set; }
    }
}
