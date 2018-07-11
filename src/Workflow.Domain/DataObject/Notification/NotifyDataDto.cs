using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.Notification
{

    [DataContract]
    public class NotifyDataDto
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "activityId")]
        public int ActivityId { get; set; }

        [DataMember(Name = "activityCode")]
        public string ActivityCode { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "metaData")]
        public object MetaData { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }

    }
}
