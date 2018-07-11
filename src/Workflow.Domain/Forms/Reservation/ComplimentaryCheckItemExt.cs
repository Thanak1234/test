using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [NotMapped]
    [DataContract]
    public class ComplimentaryCheckItemExt : ComplimentaryCheckItem
    {        
        [DataMember(Name = "checkname")]
        public  string CheckName { get; set; }
        
        [DataMember(Name = "check")]
        public bool Check { get; set; }
        
        [DataMember(Name = "uncheck")]
        public bool Uncheck { get { return !this.Check; } }
    }
}
