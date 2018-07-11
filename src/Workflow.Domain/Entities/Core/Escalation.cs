

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class Escalation
    {

        public int Id { get; set; }

        public string AppCode { get; set; }

        public string ActName { get; set; }

        public string UserRoleName { get; set; }

        public string Type { get; set; }

        public int EscalateAfter { get; set; }

        public string DatePart { get; set; }

        public int Priority { get; set; }

        public int Repeat { get; set; }

        public bool Active { get; set; }

    }
}
