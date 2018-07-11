using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Email {

    [DataContract]
    public class MailList {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "uniqueIdentifier")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "receipient")]
        public string EmailPassword { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }
    }

}
