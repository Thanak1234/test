using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Core
{
    [Table("PROFILER", Schema = "BPMDATA")]
    public class Profiler
    {
        public int Id { get; set; }
        public string LoginName { get; set;}
        public DateTime? LastLoginDateUtc { get; set; }

    }
}
