

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class RoleRight
    {

        public int Id { get; set; }

        public Nullable<int> RoleId { get; set; }

        public Nullable<int> RihgtId { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
