using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Dto.Worklists {

    public class WorkTypeDto {
        public WorkTypeDto() {
            Destinations = new List<DestinationDto>();
            WorklistCriteria = new WorklistCriteriaDto();
            WorkTypeExceptions = new List<WorkTypeExceptionDto>();
        }
        public string Name { get; set; }
        public List<DestinationDto> Destinations { get; set; }
        public WorklistCriteriaDto WorklistCriteria { get; set; }
        public List<WorkTypeExceptionDto> WorkTypeExceptions { get; set; }
    }

}
