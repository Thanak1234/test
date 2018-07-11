

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class WorkflowRole
    {

        public int Id { get; set; }

        public Nullable<int> ParentId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<int> IconIndex { get; set; }

    }
}
