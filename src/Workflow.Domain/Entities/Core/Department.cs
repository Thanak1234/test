

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class Department
    {

        public int Id { get; set; }

        public int DeptId { get; set; }

        public string Package { get; set; }

        public Nullable<int> Value { get; set; }

    }
}
