using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Web.Models.RoleRights;
using Workflow.Web.Models.Roles;

namespace Workflow.Web.Models.Validators {
    public class UserViewModelValidator: AbstractValidator<UserViewModel> {
        public UserViewModelValidator() {
            RuleFor(p => p.Active)
                .NotEmpty();
            RuleFor(p => p.DeptApprovalRoleId)
                .NotNull();
            RuleFor(p => p.EmployeeId)
                .NotEmpty();
        }
    }
}
