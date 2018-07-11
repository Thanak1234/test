

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class AdGroup
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
