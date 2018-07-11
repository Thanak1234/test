using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Repositories {

    using Infrastructure;
    using Interfaces;
    using DataObject.RoleRights;
    using Domain.Entities.Core;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;
    using DataObject;

    public class RoleRightRepository: RepositoryBase<ActivityRoleRight>, IRoleRightRepository {

        public RoleRightRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }

        public void AddUser(ActivityRoleRight entity) {
            var context = DbContext as WorkflowContext;

            var oEntity = context.ActivityRoleRights.SingleOrDefault(p => p.EmployeeId == entity.EmployeeId && p.DeptApprovalRoleId == entity.DeptApprovalRoleId);

            if (oEntity != null)
                throw new Exception("Role right has this employee already.");

            context.ActivityRoleRights.Add(entity);

            if (context.ChangeTracker.HasChanges()) {
                context.SaveChanges();
            }
        }

        public void UpdateUser(ActivityRoleRight entity) {

            var context = DbContext as WorkflowContext;

            if (entity == null || entity.Id == 0)
                throw new Exception("Can not update because it has not id.");

            var oEntity = context.ActivityRoleRights.SingleOrDefault(p => p.Id == entity.Id);
            
            if(oEntity == null) {
                throw new Exception("activity role right not found.");
            }

            oEntity.ModifiedBy = entity.ModifiedBy;
            oEntity.ModifiedDate = entity.ModifiedDate;
            oEntity.Description = entity.Description;
            oEntity.Active = entity.Active;

            if(context.ChangeTracker.HasChanges()) {
                context.SaveChanges();
            }
        }

        public IEnumerable<ActivityDto> GetActivities() {

            var context = DbContext as WorkflowContext;

            var result = context.Activities.Where(p => p.Active == true)
                .OrderBy(o => o.Sequence)
                .Select(p => new ActivityDto() {
                    Id = p.Id,
                    FormId = p.WorkflowId,
                    Name = p.Name,
                    Description = p.DisplayName
                });

            return result;
        }

        public IEnumerable<FormDto> GetForms() {

            var context = DbContext as WorkflowContext;

            var result = context.RequestApplications.Where(p => p.Active == true)
                .Select(p => new FormDto() {
                    Id = p.Id,
                    Name = p.ProcessName,
                    Description = p.RequestDesc,
                    Path = p.ProcessPath
                });

            return result;
        }

        public IEnumerable<RoleDto> GetRoles(int actId) {
            var context = DbContext as WorkflowContext;
            string sql = @" SELECT DR.ID id, 
                            ACT_ID, 
                            DR.DEPT_APPROVAL_ROLE [role], 
                            COALESCE(D.FULL_DEPT_NAME COLLATE DATABASE_DEFAULT, DR.DESCRIPTION) displayName, 
                            DR.[DESCRIPTION] [description]
                            FROM BPMDATA.DEPT_APPROVAL_ROLE DR 
                            LEFT JOIN HR.VIEW_DEPARTMENT D 
                            ON DR.DEPT_ID = D.TEAM_ID 
                            WHERE DR.ACTIVE = 1 AND ACT_ID=@actId";

            var results = SqlQuery<RoleDto>(sql, new object[] {
                new SqlParameter("@actId", actId)
            });

            return results;
        }

        public IEnumerable<ActivityRightDto> GetRoleRights(int empId) {

            string sqlQuery = @"SELECT
	                                R.ID Id, 
	                                R.EMPLOYEE_ID EmployeeId, 
	                                A.REQUEST_DESC Form, 
	                                AC.DISPLAY_NAME Activity, 
	                                COALESCE(VD.FULL_DEPT_NAME COLLATE DATABASE_DEFAULT, DA.DESCRIPTION) Role
                                FROM BPMDATA.REQUEST_APPLICATION A 
                                INNER JOIN ADMIN.ACTIVITY AC ON A.ID = AC.WORKFLOW_ID
                                INNER JOIN BPMDATA.DEPT_APPROVAL_ROLE DA ON AC.ID = DA.ACT_ID
                                INNER JOIN ADMIN.ACTIVITY_ROLE_RIGHT R ON DA.ID = R.DEPT_APPROVAL_ROLE_ID
                                LEFT JOIN HR.VIEW_DEPARTMENT VD ON DA.DEPT_ID = VD.TEAM_ID
                                WHERE R.EMPLOYEE_ID = @EmployeeID
                                ORDER BY A.REQUEST_DESC, AC.SEQUENCE ASC";

            IEnumerable<ActivityRightDto> result = SqlQuery<ActivityRightDto>(sqlQuery, new object[] {
                new SqlParameter("@EmployeeID", empId)
            });

            return result;
        }

        public ResourceWrapper GetUsers(UserQueryParameter queryParameter) {
            string orderBy = GetOrderBy(queryParameter);
            var totalRecords = new SqlParameter("@TotalRecords", System.Data.SqlDbType.Int);
            totalRecords.Direction = System.Data.ParameterDirection.Output;

            string sqlQuery = @"EXEC ADMIN.GET_USER_RIGHT @Query=@Query, 
                                                            @Start=@Start, 
                                                            @DarId=@DarId, 
                                                            @Limit=@Limit, 
                                                            @Order=@Order, 
                                                            @TotalRecords=@TotalRecords OUT";

            var result = SqlQuery<UserRightDto>(sqlQuery, new object[] {
                new SqlParameter("@Query", queryParameter.query?? string.Empty),
                new SqlParameter("@Start", queryParameter.start),
                new SqlParameter("@Limit", queryParameter.limit),
                new SqlParameter("@Order", orderBy),
                new SqlParameter("@DarId", queryParameter.DarId),
                totalRecords
            });

            ResourceWrapper wrapper = new ResourceWrapper();
            wrapper.Page = queryParameter.page;
            wrapper.Size = queryParameter.limit;
            wrapper.Records = result;
            wrapper.TotalRecords = (int)totalRecords.Value;

            return wrapper;
        }

        public int DeleteUsers(IEnumerable<UserRightDto> users) {
            string sql = string.Format("DELETE ADMIN.ACTIVITY_ROLE_RIGHT WHERE ID IN ({0})", string.Join(",", users.Select(p => p.RoleRightId)));
            return DbContext.Database.ExecuteSqlCommand(sql);
        }

        public int DeleteActivityRoleRights(IEnumerable<ActivityRightDto> rights) {
            string sql = string.Format("DELETE ADMIN.ACTIVITY_ROLE_RIGHT WHERE ID IN ({0})", string.Join(",", rights.Select(p => p.Id)));
            return DbContext.Database.ExecuteSqlCommand(sql);
        }

        public string GetOrderBy(UserQueryParameter queryParameter) {
            string order = string.Empty;

            var sorts = queryParameter.GetSorts();

            if (sorts == null || sorts.Count() == 0)
                return string.Empty;

            sorts.Each((p) => {
                order += string.Format("{0} {1},", p.Property, p.Direction);
            });

            if (order.Length > 0) {
                order = order.Substring(0, order.Length - 1);
            }

            return string.Format("ORDER BY {0}", order);
        }
    }
}