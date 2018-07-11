using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Admsr {
    public class AdmsrCompany {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string Name { get; set; }
        public DateTime? DateIssued { get; set; }
        public decimal? ValidDay { get; set; }
        public decimal? Price { get; set; }
    }
}
