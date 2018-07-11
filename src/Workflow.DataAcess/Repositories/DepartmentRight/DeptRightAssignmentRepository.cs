using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.DepartmentRight;
using Workflow.DataObject.RoleRights;
using Workflow.Domain.Entities.Core;

namespace Workflow.DataAcess.Repositories.DepartmentRight
{
    public class DeptRightAssignmentRepository : RepositoryBase<DeptAccessRight>,IDeptRightAssignmentRepository
    {

        public DeptRightAssignmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public DeptAccessRight GetDeptAccessRightById(int id)
        {
            var context = DbContext as WorkflowContext;
            return context.DeptAccessRight.Where(n => n.Id == id).SingleOrDefault();
        }

        public IEnumerable<FormDto> GetForms()
        {
            var context = DbContext as WorkflowContext;
            
            var result = context.RequestApplications.Where(p => p.Active == true)
                .Select(p => new FormDto()
                {
                    Id = p.Id,
                    Name = p.ProcessName,
                    Description = p.RequestDesc,
                    Path = p.ProcessPath
                }).OrderBy(p => p.Name);

            return result;
            
        }

        public IEnumerable<DepartmentRightDto> GetDepartments()
        {
            string sql = @"SELECT D.TEAM_ID id,D.TEAM_CODE DeptCode,
                            D.FULL_DEPT_NAME Deptname,D.DEPT_TYPE DeptType
                            FROM HR.VIEW_DEPARTMENT D
                            ORDER BY Deptname
                            ";

            var results = SqlQuery<DepartmentRightDto>(sql);

            return results.ToList();
        }

        public IEnumerable<DepartmentRightDto> GetDepartments(int id)
        {
            string sql = @"SELECT D.TEAM_ID id,D.TEAM_CODE DeptCode, 
                            D.FULL_DEPT_NAME Deptname,D.DEPT_TYPE DeptType 
                            FROM HR.VIEW_DEPARTMENT D 

                            WHERE id =  @id 
                            ORDER BY Deptname 
                            ";

            var results = SqlQuery<DepartmentRightDto>(sql, new object[] {
                new SqlParameter("@id", id)
            });

            return results.ToList();
        }

        public IEnumerable<DepartmentRightDto> GetDepartments(string query)
        {
            string sql = @"SELECT D.TEAM_ID id,D.TEAM_CODE DeptCode, 
                            D.FULL_DEPT_NAME Deptname,D.DEPT_TYPE DeptType 
                            FROM HR.VIEW_DEPARTMENT D 

                            WHERE (DEPT_NAME LIKE '%{0}%' OR DEPT_CODE LIKE '%{1}%') 
                            ORDER BY Deptname 
                            ";

            sql = string.Format(sql, query, query);

            var results = SqlQuery<DepartmentRightDto>(sql);

            return results.ToList();
        }

        public IEnumerable<DeptAccessRightDto> GetDeptAccessRight(int FormId, int DeptId)
        {
            if (FormId < 1 || DeptId < 1)
                return new List<DeptAccessRightDto>();

            var context = DbContext as WorkflowContext;

            var reqapp = context.RequestApplications.Where(n => n.Id == FormId && n.Active == true).SingleOrDefault();

            string sql = @"SELECT * FROM BPMDATA.V_DEPT_ACCESS_RIGHT
                           WHERE DEPT_ID = {0}
                           AND REQ_APP = '{1}'";

            sql = string.Format(sql, DeptId, reqapp.RequestCode);

            var result = SqlQuery<DeptAccessRightDto>(sql);

            return result.ToList(); ;
        }

        public override void Add(DeptAccessRight entity)
        {
            var context = DbContext as WorkflowContext;

            var _dar = context.DeptAccessRight.Where(n => n.DeptId == entity.DeptId && n.UserId == entity.UserId && n.ReqApp == entity.ReqApp).SingleOrDefault();

            if(_dar != null)
                throw new Exception("Dept Right has this employee already.");            
            
            base.Add(entity);            
        }

        public override void Update(DeptAccessRight entity)
        {
            if(entity == null)
            {
                throw new Exception("Dept Right is not found.");
            }
                                    
            base.Update(entity);            
        }

        public void RoleManagementSync()
        {
            executeSqlCommand("EXEC BPMDATA.APPLY_DEPT_ACCESS_BY_ROLE_MNGT");            
        }

    }
}
