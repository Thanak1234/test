using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Email {

    [DataContract]
    public class EmailItem {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "uniqueIdentifier")]
        public string UniqueIdentifier { get; set; }

        [DataMember(Name = "receipient")]
        public string Receipient { get; set; }

        [DataMember(Name = "originator")]
        public string Originator { get; set; }

        [DataMember(Name = "cc")]
        public string Cc { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "directory")]
        public string Directory { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "fileAttachements")]
        public ICollection<FileAttachement> FileAttachements { get; set; }

        public override string ToString() {
            return string.Format("Mail Item (Id={0}, UniqueIdentifier={1}, Receipient={2}, Subject={3})", Id, UniqueIdentifier, Receipient, Subject);
        }
    }

}
