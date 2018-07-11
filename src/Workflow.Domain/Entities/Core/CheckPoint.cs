

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class CheckPoint
    {

        public int Id { get; set; }

        public string Code { get; set; }

        public int Flag { get; set; }

        public bool Last { get; set; }

    }
}
