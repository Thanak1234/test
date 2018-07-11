using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.RoleRights;
using Workflow.Domain.Entities.Core;

namespace Workflow.Service.Interfaces {
    public interface IRoleRightService {
        IEnumerable<FormDto> GetForms();
        IEnumerable<ActivityDto> GetActivities();
        IEnumerable<RoleDto> GetRoles(int actId);
        ResourceWrapper GetUsers(UserQueryParameter queryParameter);
        void Add(ActivityRoleRight entity);
        void Update(ActivityRoleRight entity);
        int DeleteUsers(IEnumerable<UserRightDto> users);
        IEnumerable<ActivityRightDto> GetRoleRights(int empId);
        int DeleteActivityRoleRights(IEnumerable<ActivityRightDto> rights);
    }
}
