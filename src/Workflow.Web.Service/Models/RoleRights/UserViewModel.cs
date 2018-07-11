using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models.RoleRights {

    public class UserViewModel {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int DeptApprovalRoleId { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
    }

}
