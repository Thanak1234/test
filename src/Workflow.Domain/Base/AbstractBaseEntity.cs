/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Base
{
    [DataContract]
    public abstract class AbstractBaseEntity {

        //[Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
