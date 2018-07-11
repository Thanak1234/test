using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core.Upload;

namespace Workflow.Domain.Entities
{
    [DataContract]
    public class FileAttachement : FileUpload
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [IgnoreDataMember]
        public bool ReadOnlyRecord { get; set; }


    }
}
