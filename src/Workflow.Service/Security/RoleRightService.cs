using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.RoleRights;
using Workflow.Domain.Entities.Core;
using Workflow.Service.Interfaces;

namespace Workflow.Service {

    public class RoleRightService : IRoleRightService {

        private IRoleRightRepository _Repository;

        public RoleRightService() {
            _Repository = new RoleRightRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }

        public void Add(ActivityRoleRight entity) {
            _Repository.AddUser(entity);
        }

        public void Update(ActivityRoleRight entity) {
            _Repository.UpdateUser(entity);
        }

        public IEnumerable<ActivityDto> GetActivities() {
            return _Repository.GetActivities();
        }

        public IEnumerable<FormDto> GetForms() {
            return _Repository.GetForms();
        }

        public IEnumerable<RoleDto> GetRoles(int actId) {
            return _Repository.GetRoles(actId);
        }

        public ResourceWrapper GetUsers(UserQueryParameter queryParameter) {
            return _Repository.GetUsers(queryParameter);
        }

        public int DeleteUsers(IEnumerable<UserRightDto> users) {
            return _Repository.DeleteUsers(users);
        }

        public IEnumerable<ActivityRightDto> GetRoleRights(int empId) {
            return _Repository.GetRoleRights(empId);
        }

        public int DeleteActivityRoleRights(IEnumerable<ActivityRightDto> rights) {
            return _Repository.DeleteActivityRoleRights(rights);
        }
    }

}
