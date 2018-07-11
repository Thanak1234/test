

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class BackupEmployee
    {

        public int Id { get; set; }

        public Nullable<int> DeptId { get; set; }

        public string LoginName { get; set; }

        public string EmpNo { get; set; }

        public string JobTitle { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string MobilePhone { get; set; }

        public string HomePhone { get; set; }

        public string IpPhone { get; set; }

        public string Address { get; set; }

        public Nullable<System.DateTime> HiredDate { get; set; }

        public string ReportTo { get; set; }

        public string DeptName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmpType { get; set; }

        public Nullable<bool> Active { get; set; }

    }
}
