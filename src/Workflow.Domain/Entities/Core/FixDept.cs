

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class FixDept
    {

        public int EmpId { get; set; }

        public int CurDeptId { get; set; }

        public int CorrectDeptId { get; set; }

        public bool Active { get; set; }

    }
}
