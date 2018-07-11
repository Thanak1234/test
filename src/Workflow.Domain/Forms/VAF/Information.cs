using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.VAF {
    public class Information {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public string AdjType { get; set; }
        public string Remark { get; set; }
    }
}
