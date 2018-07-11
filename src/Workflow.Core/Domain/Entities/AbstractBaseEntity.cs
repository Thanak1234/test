using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Core.Entities
{
    [DataContract]
    public abstract class AbstractBaseEntity
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }

        public AbstractBaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
