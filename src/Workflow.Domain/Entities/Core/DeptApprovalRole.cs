

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class DeptApprovalRole
    {

        public int Id { get; set; }

        public string RequestCode { get; set; }

        public string RoleCode { get; set; }

        public Nullable<int> ActId { get; set; }

        public Nullable<int> DeptId { get; set; }

        public string DeptApprovalRole1 { get; set; }

        public Nullable<System.Guid> RoleGuid { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Active { get; set; }

    }
}
