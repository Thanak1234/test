

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class ActivityRoleRight
    {

        public int Id { get; set; }

        public Nullable<int> EmployeeId { get; set; }

        public Nullable<int> DeptApprovalRoleId { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

    }
}
