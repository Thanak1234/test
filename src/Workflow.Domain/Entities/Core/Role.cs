

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class Role
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
