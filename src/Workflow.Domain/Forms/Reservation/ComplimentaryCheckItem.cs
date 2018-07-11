using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [DataContract]
    public class ComplimentaryCheckItem
    {        
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "requestheaderid")]
        public int? RequestHeaderId { get; set; }
        
        [DataMember(Name = "explid")]
        public int? ExplId { get; set; }

        [DataMember(Name = "createdate")]
        public DateTime? CreatedDate { get; set; }
    }
}
