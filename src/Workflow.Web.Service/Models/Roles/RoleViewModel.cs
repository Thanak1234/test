using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Web.Models.Validators;

namespace Workflow.Web.Models.Roles {
    
    public class RoleViewModel: BaseRoleViewModel {
        public string RoleName { get; set; }
        public string User { get; set; }
        public bool Include { get; set; }
        public int EmpId { get; set; }
    }

}
