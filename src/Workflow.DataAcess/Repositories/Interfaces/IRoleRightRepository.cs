using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.RoleRights;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.Interfaces {

    public interface IRoleRightRepository: IRepository<ActivityRoleRight> {
        IEnumerable<FormDto> GetForms();
        IEnumerable<ActivityDto> GetActivities();
        IEnumerable<RoleDto> GetRoles(int actId);
        ResourceWrapper GetUsers(UserQueryParameter queryParameter);
        void UpdateUser(ActivityRoleRight entity);
        void AddUser(ActivityRoleRight entity);
        int DeleteUsers(IEnumerable<UserRightDto> users);
        IEnumerable<ActivityRightDto> GetRoleRights(int empId);
        int DeleteActivityRoleRights(IEnumerable<ActivityRightDto> rights);
    }

}
