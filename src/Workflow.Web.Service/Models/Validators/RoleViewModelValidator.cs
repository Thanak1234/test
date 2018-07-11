using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Web.Models.Roles;

namespace Workflow.Web.Models.Validators {
    public class RoleViewModelValidator: AbstractValidator<RoleViewModel> {
        public RoleViewModelValidator() {
            RuleFor(p => p.User)
                .NotEmpty();
            RuleFor(p => p.Include)
                .NotNull();
            RuleFor(p => p.RoleName)
                .NotEmpty();
        }
    }
}
