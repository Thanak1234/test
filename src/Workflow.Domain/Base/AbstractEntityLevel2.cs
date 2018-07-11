using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Base
{
    public abstract class AbstractEntityLevel2 : AbstractBaseEntity
    {
        [DataMember(Name = "modifiedDate")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
