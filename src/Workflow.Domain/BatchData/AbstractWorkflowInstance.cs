/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.DataObject;

namespace Workflow.Domain.Entities.BatchData
{
    public abstract class AbstractWorkflowInstance
    {

        public int RequestHeaderId { get; set; }
        public string RequestNo { get; set; }
        public string SerialNo { get; set; }
        public string LastActivity { get; set; }
        public string Activity { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }
        public IList<string> Actions { get; set; }
        public object ViewConfig { get; set; }
        public string Comment { get; set; }
        public short Priority { get; set; }
        public Employee Requestor { get; set; }
        public string CurrentUser { get; set; }
        public string SharedUser { get; set; }
        public string ManagedUser { get; set; }
        public string fullName { get; set; }
        public string loginName { get; set; }
        public string FileUploadSerial { get; set; }
        public string RuntimeURL { get; set; }

        public ICollection<ActivityHistory> ActivityHistories { get; set; }
        public IEnumerable<FileAttachement> UploadFiles { get; set; }
        public IEnumerable<FileAttachement> DelUploadFiles { get; set; }
        public IEnumerable<FileAttachement> EditUploadFiles { get; set; }
        public IEnumerable<FileAttachement> AddUploadFiles { get; set; }

        public FileAttachement Attachment { get; set; }

        public Type AttachmentType { get; set; }

        public string Message { get; set; }
    }
}
