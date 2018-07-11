

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class WorkflowRight
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string WorkflowName { get; set; }

        public string Activity { get; set; }

        public bool WorkflowAdmin { get; set; }

        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public Nullable<int> Version { get; set; }

    }
}
