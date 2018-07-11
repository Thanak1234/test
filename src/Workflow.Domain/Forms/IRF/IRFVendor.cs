using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.IRF {
    public class IRFVendor {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public string Vendor { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
