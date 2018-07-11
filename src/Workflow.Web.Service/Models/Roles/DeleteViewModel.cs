using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.Roles;

namespace Workflow.Web.Models.Roles {
    public class DeleteViewModel: BaseRoleViewModel {
        public string RoleName { get; set; }
        public IEnumerable<UserRoleDto> Users { get; set; }
        public string Description { get; set; }
    }
}
