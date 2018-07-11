

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class UserRole
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public Nullable<int> RoleId { get; set; }

        public Nullable<bool> IsRoleRightAdmin { get; set; }

        public Nullable<bool> IsRoleRightAssign { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
