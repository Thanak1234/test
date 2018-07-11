using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Dto.Worklists {
    public class WorklistCriteriaDto {
        
        public string ManagedUser { get; set; }
        public string Platform { get; set; }
        public bool NoData { get; set; }
        public int Count { get; set; }
        public int StartIndex { get; set; }
        public List<FilterDto> Filters { get; set; }

    }
}
