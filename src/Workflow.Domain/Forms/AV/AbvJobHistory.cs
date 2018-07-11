/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.AV
{
    public class AvbJobHistory
    {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public RequestHeader RequestHeader { get; set; }
        public string ProjectName { get; set; }
        public string Locaiton { get; set; }
        public DateTime SetupDate { get; set; }
        public DateTime ActualDate { get; set; }
        public string ProjectBrief { get; set; }
        public string Other { get; set; }
    }
}
