

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class DeptAccessRight
    {

        public int Id { get; set; }

        public int DeptId { get; set; }

        public int UserId { get; set; }

        public string ReqApp { get; set; }

        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string ModifiedBy { get; set; }

        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Nullable<bool> Sync { get; set; }

        public string Status { get; set; }

    }
}
