using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.DepartmentRight;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.DepartmentRight;
using Workflow.DataObject.RoleRights;
using Workflow.Domain.Entities.Core;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{

    public class DepartmentRightService : IDepartmentRightService
    {

        private IDeptRightAssignmentRepository _repository;

        public DepartmentRightService()
        {
            _repository = new DeptRightAssignmentRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }
        
        public IEnumerable<DepartmentRightDto> GetDepartments()
        {
            return _repository.GetDepartments();
        }

        public IEnumerable<DepartmentRightDto> GetDepartments(int id)
        {
            return _repository.GetDepartments(id);
        }

        public IEnumerable<DepartmentRightDto> GetDepartments(string query)
        {
            return _repository.GetDepartments(query);
        }

        public IEnumerable<FormDto> GetForms()
        {
            return _repository.GetForms();
        }

        public IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId)
        {
            return _repository.GetDeptAccessRight(FormId, DeptId);
        }

        public IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId, string query)
        {
            if (query == null || string.IsNullOrEmpty(query.Trim()))
                return _repository.GetDeptAccessRight(FormId, DeptId);

            var result = _repository.GetDeptAccessRight(FormId, DeptId).Where(m => m.DISPLAY_NAME.ToLower().Contains(query.ToLower()));
            return result.ToList();
        }

        public void Add(DeptAccessRight entity)
        {
            _repository.Add(entity);
        }

        public void Update(DeptAccessRight entity)
        {
            _repository.Update(entity);
        }

        public DeptAccessRight GetDeptAccessRightById(int id)
        {
            return _repository.GetDeptAccessRightById(id);
        }

        public void RoleManagementSync()
        {
            _repository.RoleManagementSync();
        }
    }
}
