using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.Roles;

namespace Workflow.Web.Models.Roles {
    public class DeleteMultiRoleViewModel {
        public string LoginName { get; set; }
        public int EmpId { get; set; }
        public IEnumerable<WorkflowRoleDto> Roles { get; set; }
    }
}
