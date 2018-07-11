using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    [NotMapped]
    public class RequestUserExt : RequestUser
    {
        public string teamName { get; set; }
    }
}
