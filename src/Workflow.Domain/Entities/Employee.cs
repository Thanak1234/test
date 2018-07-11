/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities
{
    public partial class Employee
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

        public virtual Department Department { get; set; }
    }
}
