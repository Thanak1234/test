

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class SpMenu
    {

        public int Id { get; set; }

        public Nullable<int> ParentId { get; set; }

        public Nullable<int> RightId { get; set; }

        public string MenuName { get; set; }

        public string MenuDesc { get; set; }

        public string WorkFlow { get; set; }

        public bool IsWorkFlow { get; set; }

        public string MenuUrl { get; set; }

        public string MenuIcon { get; set; }

        public string MenuClass { get; set; }

        public Nullable<int> Sequence { get; set; }

        public Nullable<bool> NoChild { get; set; }

        public Nullable<bool> Focus { get; set; }

        public Nullable<bool> Active { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
