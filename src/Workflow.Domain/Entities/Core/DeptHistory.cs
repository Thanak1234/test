

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class DeptHistory
    {

        public int Id { get; set; }

        public Nullable<int> RequestHeaderId { get; set; }

        public Nullable<int> ParticipantActionId { get; set; }

        public Nullable<int> DeptId { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}
