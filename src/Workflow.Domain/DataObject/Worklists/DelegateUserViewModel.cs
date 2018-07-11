using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.Dto.Worklists {
    public class DelegateUserViewModel {
        public string SerialNumber { get; set; }
        public IList<DestinationDto> Destinations { get; set; }
    }
}
