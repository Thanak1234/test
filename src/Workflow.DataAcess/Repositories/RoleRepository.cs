using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Cores.Utils;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Roles;
using Workflow.Domain.Entities.Core;
using Management = SourceCode.Security.UserRoleManager.Management;
using Workflow.Framework;

namespace Workflow.DataAcess.Repositories {

    public class RoleRepository : RepositoryBase<Role>, IRoleRepository {

        public const string ACTIVE = "ACTIVE";
        public const string IN_ACTIVE = "IN_ACTIVE";

        IProcInstProvider server;

        public RoleRepository(IDbFactory dbFactory) : base(dbFactory) {
            server = new ProcInstProvider("");
        }

        public TreeItemDto GetTreeItems(string loginName) {

            TreeItemDto root = new TreeItemDto();
            root.text = "Nagaworld";
            root.leaf = false;
            root.key = 0;
            root.type = "Root";           

            string sql = @"SELECT 
                ID [key], 
                (ISNULL(M.MENU_NAME + ' - ','') + REQUEST_DESC) [text], 
                'WF' type, 
                PROCESS_PATH value, 
                CAST(0 as bit) leaf, 
                [IS_DB_ROLE] isDbRole 
            FROM BPMDATA.REQUEST_APPLICATION A
            LEFT JOIN(
                SELECT DISTINCT M1.MENU_NAME, M.WORK_FLOW FROM [ADMIN].[MENU] M 
                INNER JOIN [ADMIN].[MENU] M1 ON M1.ID = M.PARENT_ID
                WHERE M.IS_WORK_FLOW = 1
            ) M ON M.WORK_FLOW = A.PROCESS_PATH
            WHERE A.ACTIVE = 1
            ORDER BY M.MENU_NAME, REQUEST_DESC ";

            var children = SqlQuery<TreeItemDto>(sql).ToList();

            root.children = children;

            if (children != null && children.Count() > 0) {
                foreach (TreeItemDto child in children) {
                    sql = string.Format(
                        @"SELECT ID [key], DISPLAY_NAME [text], 'activity' type, NAME value, CAST(0 as bit) leaf, @IsDbRole isDbRole 
                        FROM ADMIN.ACTIVITY A WHERE WORKFLOW_ID = @id AND ACTIVE = 1 
                        AND EXISTS(SELECT TOP 1 1 FROM BPMDATA.DEPT_APPROVAL_ROLE WHERE [ACTIVE] = 1 AND ACT_ID = A.ID)
                        ORDER BY [SEQUENCE], [NAME]");

                    child.children = SqlQuery<TreeItemDto>(sql, new object[] {
                         new SqlParameter("id", child.key),
                         new SqlParameter("IsDbRole", child.isDbRole)
                    }).ToList();

                    if (child.children != null && child.children.Count() > 0) {
                        List<TreeItemDto> removedItems = new List<TreeItemDto>();

                        foreach (TreeItemDto ch in child.children) {
                            sql = "EXEC [ADMIN].[GET_USER_ROLE_TREEITEMS] @loginName, @activityId, @IsDbRole";
                            var roles = SqlQuery<TreeItemDto>(sql, new object[] {
                                new SqlParameter("loginName", loginName),
                                new SqlParameter("activityId", ch.key),
                                new SqlParameter("IsDbRole", ch.isDbRole)
                            }).ToList();
                            if (roles != null && roles.Count() > 0)
                            {
                                ch.children = roles;
                            }
                            else {
                                removedItems.Add(ch);
                                ch.children = null;
                            }
                        }

                        foreach (var removedItem in removedItems) {
                            child.children.Remove(removedItem);
                        }
                    }
                }
            }

            return root;
        }

        public IEnumerable<UserRoleDto> GetUserRoles(string rolename) {

            var context = DbContext as WorkflowContext;

            if (context == null)
                throw new ArgumentException("Can not cast DbContext to WorkflowContext");

            IList<UserRoleDto> users = new List<UserRoleDto>();

            IList<UserRoleDto> excludes = new List<UserRoleDto>();
            IList<UserRoleDto> includes = new List<UserRoleDto>();

            users.AddRange(includes);
            users.AddRange(excludes);

            return users;
        }

        public IEnumerable<UserRoleDto> GetUsers(string roleName, bool isDbRole) {

            string sql = @"EXEC BPMDATA.GET_USER_ROLE @RoleName, @IsDbRole";

            var results = SqlQuery<UserRoleDto>(sql, new object[] {
                new SqlParameter("@RoleName", roleName),
                new SqlParameter("@IsDbRole", isDbRole)
            });

            return results;
        }

        private IList<UserRoleDto> GetIncludeOrExclude(Management.Role role, bool isInclude) {
            Management.RoleItemCollection<Management.Role, Management.RoleItem> roleitems = null;
            IList<UserRoleDto> users = new List<UserRoleDto>();

            if (isInclude) {
                roleitems = role.Include;
            } else {
                roleitems = role.Exclude;
            }

            foreach (Management.RoleItem roleitem in roleitems) {
                UserRoleDto user = new UserRoleDto();
                var single = GetEmployee(roleitem.Name);

                if (single != null) {
                    user.fullName = single.fullName;
                    user.Devision = single.devision;
                    user.EmployeeNo = single.employeeNo;
                    user.GroupName = single.groupName;
                    user.Position = single.position;
                    user.SubDept = single.subDept;
                }

                user.LoginName = SecurityLabel.GetNameWithoutLabel(roleitem.Name).ToUpper();
                user.Include = isInclude;

                users.Add(user);
            }

            return users;
        }

        private EmployeeDto GetEmployee(string loginname) {
            var sql = " SELECT[ID] Id " +
                 " ,[LOGIN_NAME] loginName " +
                 " ,[EMP_NO] employeeNo " +
                 " ,[DISPLAY_NAME] fullName " +
                 " ,[POSITION] position " +
                 " ,[EMAIL] email " +
                 " ,[TELEPHONE] ext " +
                 " ,[MOBILE_PHONE] phone " +
                 " ,[MANAGER] reportTo " +
                 " ,[GROUP_NAME] groupName " +
                 " ,[TEAM_ID] subDeptId " +
                 " ,[TEAM_NAME] subDept " +
                 " ,[DEPT_TYPE] devision " +
                 " ,[HOD] hod " +
                 " ,[EMP_TYPE] empType" +
              " FROM [HR].[VIEW_EMPLOYEE_LIST] WHERE LOGIN_NAME = @loginname";

            var single = SqlQuery<EmployeeDto>(sql,
                new object[] {
                    new SqlParameter("@loginname", SecurityLabel.GetNameWithoutLabel(loginname))
                }
            ).FirstOrDefault();

            return single;
        }

        public bool AddUserRole(string roleName, string loginName, bool include) {
            return true;
        }

        public bool UpdateUserRole(string roleName, string loginName, bool include) {
            return true;
        }

        public IEnumerable<WorkflowRoleDto> GetRoles(string loginName, string identity) {

            string sql = @"EXEC BPMDATA.GET_WORKFLOW_ROLES @LoginName=@LoginName, @Identity=@Identity";

            IEnumerable<WorkflowRoleDto> result = SqlQuery<WorkflowRoleDto>(sql,
                                                        new object[] {
                                                            new SqlParameter("@LoginName", SecurityLabel.GetNameWithLabel(loginName)),
                                                            new SqlParameter("@Identity", SecurityLabel.GetNameWithLabel(identity))
                                                        }
                                                    );
            return result;
        }
        public bool UpdateDbUserRole(string roleName, int empId, bool include) {

            int effectCount = 0;
            var context = DbContext as WorkflowContext;

            var oEntity = context.UserRoles.FirstOrDefault(p => p.RoleCode == roleName && p.EmpId == empId);

            if (oEntity == null)
                return false;

            oEntity.Status = include == true ? ACTIVE : IN_ACTIVE;

            if (context.ChangeTracker.HasChanges())
                effectCount = context.SaveChanges();

            return effectCount > 0;
        }

        public bool AddDbUserRole(string roleName, int empId, bool include) {

            var context = DbContext as WorkflowContext;
            int effectCount = 0;

            var oEntity = context.UserRoles.FirstOrDefault(p => p.RoleCode == roleName && p.EmpId == empId);

            if (oEntity != null)
                throw new Exception(string.Format("User already exist in {0}.", roleName));

            var nEntity = new BpmUserRole() {
                RoleCode = roleName,
                EmpId = empId,
                Status = include == true ? ACTIVE : IN_ACTIVE
            };

            context.UserRoles.Add(nEntity);

            if (context.ChangeTracker.HasChanges())
                effectCount = context.SaveChanges();

            return effectCount > 0;
        }

        public bool RemoveUserRole(string roleName, string user, string desc = "") {
            return true;
        }

        public bool RemoveUserRoles(string roleName, IEnumerable<string> users, string desc="") {
            bool effectCount = false;
            foreach (string u in users) {
                effectCount = true;
            }
            return effectCount;
        }

        public bool RemoveDbUserRole(string roleName, int empId, string desc = "") {
            int effectCount = 0;
            var context = DbContext as WorkflowContext;

            var oEntity = context.UserRoles.FirstOrDefault(p => p.RoleCode == roleName && p.EmpId == empId);

            if (oEntity == null)
                return false;

            context.UserRoles.Remove(oEntity);

            if (context.ChangeTracker.HasChanges())
                effectCount = context.SaveChanges();

            return effectCount > 0;
        }

        public bool RemoveDbUserRoles(string roleName, int[] empIds, string desc = "") {
            bool effect = false;
            foreach (int id in empIds) {
                effect = RemoveDbUserRole(roleName, id, desc);
            }
            return effect;
        }

        public bool RemoveUserByRoles(string loginName, IEnumerable<WorkflowRoleDto> roles) {
            bool effectCount = false;
            foreach (WorkflowRoleDto r in roles) {
                effectCount = true;
            }
            return effectCount;
        }

        public bool RemoveDbUserByRoles(int empId, IEnumerable<WorkflowRoleDto> roles) {
            bool effect = false;
            foreach (WorkflowRoleDto r in roles) {
                effect = RemoveDbUserRole(r.Role, empId, r.FullName);
            }
            return effect;
        }
    }
}
