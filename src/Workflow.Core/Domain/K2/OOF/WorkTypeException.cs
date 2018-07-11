using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataContract.K2
{

    public class WorkTypeExceptionDto {
        public string Name { get; set; }        
        public string Process { get; set; }
        public string ProcessPath { get; set; }
        public string Activity { get; set; }
        public string ActDisplayName { get; set; }
        public List<string> DestUsers { get; set; }
        public List<DestinationDto> Destinations { get; set; }
    }
}
