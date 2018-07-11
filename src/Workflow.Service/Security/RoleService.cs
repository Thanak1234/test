using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Roles;
using Workflow.Service.Interfaces;

namespace Workflow.Service {

    public class RoleService : IRoleService {

        private IRoleRepository _Repository;

        public RoleService() {
            _Repository = new RoleRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }

        public bool AddDbUserRole(string roleName, int empId, bool include) {
            return _Repository.AddDbUserRole(roleName, empId, include);
        }

        public bool AddUserRole(string roleName, string loginName, bool include) {
            return _Repository.AddUserRole(roleName, loginName, include);
        }

        public ResourceWrapper GetRoles(string loginName, string identity) {
            ResourceWrapper wrapper = new ResourceWrapper();
            IEnumerable<WorkflowRoleDto> records = _Repository.GetRoles(loginName, identity);
            wrapper.TotalRecords = records.Count();
            wrapper.Records = records;
            return wrapper;
        }

        public TreeItemDto GetTreeItems(string loginName) {
            return _Repository.GetTreeItems(loginName);
        }

        public ResourceWrapper GetUserRoles(string rolename) {
            ResourceWrapper wrapper = new ResourceWrapper();

            IEnumerable<UserRoleDto> users = _Repository.GetUserRoles(rolename);
            wrapper.TotalRecords = users.Count();
            wrapper.Records = users;

            return wrapper;

        }

        public ResourceWrapper GetUsers(string roleName, bool isDbRole) {
            ResourceWrapper wrapper = new ResourceWrapper();

            IEnumerable<UserRoleDto> users = _Repository.GetUsers(roleName, isDbRole);
            wrapper.TotalRecords = users.Count();
            wrapper.Records = users;

            return wrapper;
        }

        public bool RemoveDbUserByRoles(int empId, IEnumerable<WorkflowRoleDto> roles) {
            return _Repository.RemoveDbUserByRoles(empId, roles);
        }

        public bool RemoveDbUserRole(string roleName, int empId, string desc = "") {
            return _Repository.RemoveDbUserRole(roleName, empId, desc);
        }

        public bool RemoveDbUserRoles(string roleName, int[] empIds, string desc = "") {
            return _Repository.RemoveDbUserRoles(roleName, empIds, desc);
        }

        public bool RemoveUserByRoles(string loginName, IEnumerable<WorkflowRoleDto> roles) {
            return _Repository.RemoveUserByRoles(loginName, roles);
        }

        public bool RemoveUserRole(string roleName, string user, string desc = "") {
            return _Repository.RemoveUserRole(roleName, user, desc);
        }

        public bool RemoveUserRoles(string roleName, IEnumerable<string> users, string desc = "") {
            return _Repository.RemoveUserRoles(roleName, users, desc);
        }

        public bool UpdateDbUserRole(string roleName, int empId, bool include) {
            return _Repository.UpdateDbUserRole(roleName, empId, include);
        }

        public bool UpdateUserRole(string roleName, string loginName, bool include) {
            return _Repository.UpdateUserRole(roleName, loginName, include);
        }
    }

}
