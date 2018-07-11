using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Core {

    public class EmployeeView {

        public int Id { get; set; }
        public string LoginName { get; set; }
        public string EmpNo { get; set; }
        public string DisplayName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Manager { get; set; }
        public string Hod { get; set; }
        public string HodName { get; set; }
        public string GroupName { get; set; }
        public string DeptName { get; set; }
        public string TeamName { get; set; }
        public string DeptType { get; set; }
        public int DeptId { get; set; }
        public int TeamId { get; set; }
        public string EmpType { get; set; }

    }

}
