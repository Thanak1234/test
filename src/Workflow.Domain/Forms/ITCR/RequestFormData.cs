using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.ITCR
{
    public class RequestFormData
    {
        public int Id { get; set; }
        public int? RequestHeaderId { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime TargetDate { get; set; }
        public string Session { get; set; }
        public string ChangeType { get; set; }
        public string RequestChange { get; set; }
        public string Justification { get; set; }
        public string Implmentation { get; set; }
        public string Failback { get; set; }
        public string Intervention { get; set; }
        public string RestorationLavel { get; set; }
        public string DireedResult { get; set; }
        public string TestParameters { get; set; }
        public string ActualResult { get; set; }
        public string AdditionalNotes { get; set; }
        public string AkResult { get; set; }
        public string AkRemark { get; set; }
    }
}
