using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.DataObject.Roles;

namespace Workflow.Service.Interfaces {

    public interface IRoleService {
        TreeItemDto GetTreeItems(string loginName);
        ResourceWrapper GetUserRoles(string rolename);
        bool AddUserRole(string roleName, string loginName, bool include);
        bool UpdateUserRole(string roleName, string loginName, bool include);
        ResourceWrapper GetRoles(string loginName, string identity);
        ResourceWrapper GetUsers(string roleName, bool isDbRole);
        bool UpdateDbUserRole(string roleName, int empId, bool include);
        bool AddDbUserRole(string roleName, int empId, bool include);
        bool RemoveUserRole(string roleName, string user, string desc = "");
        bool RemoveUserRoles(string roleName, IEnumerable<string> users, string desc = "");
        bool RemoveDbUserRole(string roleName, int empId, string desc = "");
        bool RemoveDbUserRoles(string roleName, int[] empIds, string desc = "");
        bool RemoveUserByRoles(string loginName, IEnumerable<WorkflowRoleDto> roles);
        bool RemoveDbUserByRoles(int empId, IEnumerable<WorkflowRoleDto> roles);
    }

}
