using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.N2MWO
{
    public class N2MWOInformation
    {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public string Mode { get; set; }
        public string RequestType { get; set; }
        public string ReferenceNumber { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public int? CcdId { get; set; }
        public string Remark { get; set; }
        public string Wrjd { get; set; }
        public string Instruction { get; set; }
        public DateTime? JaDate { get; set; }
        public int? JaTechnician { get; set; }
        public string WorkType { get; set; }
        public string TcDesc { get; set; }
    }
}
