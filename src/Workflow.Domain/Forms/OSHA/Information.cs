using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.OSHA
{
    public class OSHAInformation
    {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public string NTA { get; set; }
        public string LAI { get; set; }
        public DateTime? DTA { get; set; }
        public string CA { get; set; }
        public bool? DIEGSD { get; set; }
        public string DF { get; set; }
        public bool? E1 { get; set; }
        public bool? E2 { get; set; }
        public bool? G3 { get; set; }
        public bool? G4 { get; set; }
        public string G5 { get; set; }
        public string HSC { get; set; }
        public bool? HCAT { get; set; }
        public string YESDONE { get; set; }
        public string NODONE { get; set; }
        public string ACNR { get; set; }
        public string AT { get; set; }
    }
}
