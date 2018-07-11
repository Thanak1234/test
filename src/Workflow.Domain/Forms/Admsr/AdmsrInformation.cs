using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Admsr {
    public class AdmsrInformation {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public string Dr { get; set; }
        public string Dsrj { get; set; }
        public string Adc { get; set; }
        public bool? Efinance { get; set; }
        public bool? ECC { get; set; }
        public bool? Epurchasing { get; set; }
        public bool? Salod { get; set; }
        public bool? Slod { get; set; }
    }
}
