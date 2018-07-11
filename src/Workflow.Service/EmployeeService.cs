/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Employees;
using Workflow.Domain.Entities;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class EmployeeService : IEmployeeService {

        private IEmployeeRepository _employeeRepository;
        private IUnitOfWork unitOfWork;


        public EmployeeService() {
            _employeeRepository = new EmployeeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            unitOfWork = new UnitOfWork(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
        }

        public EmployeeDto GetRequestor(int requestHeaderId, int requestorId)
        {
           
            try
            {
                return GetRequestorByRequestHeaderId(requestHeaderId);
            }
            catch (Exception)
            {
                return GetEmployee(requestorId);
            }
        }

        public EmployeeDto GetRequestorByRequestHeaderId(int id)
        {
            var sql = @"SELECT 
                [RequestorId] [id] 
	            ,[Name] fullName
                ,[EmpCode] employeeNo
                ,[Position] position
                ,[Dept] groupName
                ,[Line] deptName
                ,[Team] subDept
                ,[Phone] phone
                ,[ExtPhone] ext
                ,[Email] email
            FROM [BPMDATA].[REQUESTOR_LOG] E 
            WHERE E.RequestHeaderId = @id ";

            return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@id", id) });
        }

        public EmployeeDto GetEmployee(int id) {
            var sql = GetAllEmployeeViewQuery();
            sql = sql + " WHERE E.ID = @id ";
            try {
                return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@id", id) });
            } catch (SmartException) {
                throw new Exception(string.Format("Employee: {0} could not be found in K2, please contact administrator.", id.ToString()));
            }
        }

        public EmployeeDto GetEmployeeByNo(string empNo) {

            var sql = GetActiveEmployeeViewQuery();
            sql = sql + " WHERE E.EMP_NO = @empNo ";
            try {
                return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@empNo", empNo) });
            } catch (SmartException e) {
                throw e;
            }
        }

        public IEnumerable<EmployeeDto> GetEmployeeList() {
            return _employeeRepository.SqlQuery<EmployeeDto>(GetAllEmployeeViewQuery());
        }

        public EmployeeDto GetEmpByLoginName(string loginName) {
            var sql = GetAllEmployeeViewQuery();
            sql = sql + " WHERE E.LOGIN_NAME = @loginName ";
            try {
                return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@loginName", loginName) });
            } catch (SmartException) {
                return new EmployeeDto() {
                    email = "Unknown",
                    employeeNo = "Unknown",
                    fullName = "Unknown",
                    loginName = loginName
                };
            }
        }
        public EmployeeDto getEmplyByEmpNo(string empNo) {
            var sql = GetAllEmployeeViewQuery();
            sql = sql + " WHERE E.[EMP_NO] = @empNo ";
            try
            {
                return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@empNo", empNo) });
            }
            catch (SmartException)
            {
                return new EmployeeDto()
                {
                    email = "Unknown",
                    employeeNo = "Unknown",
                    fullName = "Unknown"
                };
            }
        }

        // update employee profile
        public void UpdateEmployeeProfile(Employee obj) {
            _employeeRepository.Update(obj);
            unitOfWork.commit();
        }
        public IEnumerable<DepartmentDto> SearchDepartment(string value) {

            var sql = GetDepartmentViewQuery();
            if (String.IsNullOrWhiteSpace(value)) {
                return _employeeRepository.SqlQuery<DepartmentDto>(sql);
            } else {
                value = "%" + value + "%";
                sql += "WHERE TEAM_CODE LIKE @value or TEAM_NAME like @value or DEPT_CODE like @value or DEPT_NAME like @value or FULL_DEPT_NAME like @value";
                return _employeeRepository.SqlQuery<DepartmentDto>(sql, new object[] { new SqlParameter("@value", value) });
            }
            throw new NotImplementedException();
        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
            return _employeeRepository.SqlQuery<DepartmentDto>(@"SELECT DISTINCT
                    D.TEAM_ID id, 
                    D.DEPT_ID deptId, 
                    D.GROUP_ID groupId, 
                    D.TEAM_CODE teamCode, 
                    D.TEAM_NAME teamName,
                    D.DEPT_CODE deptCode, 
                    D.DEPT_NAME deptName, 
                    D.GROUP_CODE groupCode, 
                    D.GROUP_NAME groupName, 
                    D.DEPT_TYPE deptType, 
                    D.FULL_DEPT_NAME fullName 
                FROM HR.VIEW_DEPARTMENT D ORDER BY D.FULL_DEPT_NAME ");
        }

        public IEnumerable<DepartmentDto> GetDepartmentRightByUser(int userId, string appName)
        {
            return _employeeRepository.SqlQuery<DepartmentDto>(@"SELECT DISTINCT
                    D.TEAM_ID id, 
                    D.DEPT_ID deptId, 
                    D.GROUP_ID groupId, 
                    D.TEAM_CODE teamCode, 
                    D.TEAM_NAME teamName,
                    D.DEPT_CODE deptCode, 
                    D.DEPT_NAME deptName, 
                    D.GROUP_CODE groupCode, 
                    D.GROUP_NAME groupName, 
                    D.DEPT_TYPE deptType, 
                    D.FULL_DEPT_NAME fullName 
                FROM (
	                SELECT DISTINCT D.* FROM BPMDATA.DEPT_ACCESS_RIGHT R INNER JOIN HR.VIEW_DEPARTMENT D ON D.TEAM_ID = R.DEPT_ID 
		                WHERE USER_ID = @UserId AND R.[STATUS] = 'ACTIVE' AND (@AppName IS NULL OR R.REQ_APP = @AppName)
	                /* UNION ALL 
	                SELECT TOP(1) D.* FROM HR.EMPLOYEE E INNER JOIN HR.VIEW_DEPARTMENT D ON D.TEAM_ID = E.DEPT_ID WHERE E.ID = @UserId */
            ) D ORDER BY D.FULL_DEPT_NAME ", new object[] {
                new SqlParameter("@UserId", userId),
                (appName!=null?new SqlParameter("@AppName", appName):new SqlParameter("@AppName", DBNull.Value)) 
            });
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> SearchEmployee(string value) {

            try {
                var sql = GetActiveEmployeeViewQuery();

                if (String.IsNullOrWhiteSpace(value)) {
                    return _employeeRepository.SqlQuery<EmployeeDto>(sql);
                } else {
                    sql = sql + " WHERE E.EMP_NO like @value or E.LOGIN_NAME like @value or E.DISPLAY_NAME like @value or E.EMAIL like  @value";
                    value = "%" + value + "%";
                    return _employeeRepository.SqlQuery<EmployeeDto>(sql, new object[] { new SqlParameter("@value", value) });
                }
            } catch (SmartException e) {
                throw e;
            }
        }
        // method get employee profile
        public EmployeeDto GetEmployeeProfile(String empNo) {
            var sql = GetEmployeeView();
            sql = sql + " inner join EMPLOYEE em on em.EMP_NO = vem.EMP_NO "
                        + " WHERE vem.EMP_NO = @empNo ";
            try {
                return _employeeRepository.Single<EmployeeDto>(sql, new object[] { new SqlParameter("@empNo", empNo) });
            } catch (SmartException e) {
                throw e;
            }
        }
        // 
        private string GetEmployeeView() {
            var sql = " SELECT " +
                         " vem.[ID] Id " +
                         " ,vem.[LOGIN_NAME] loginName " +
                         " ,vem.[EMP_NO] employeeNo " +
                         " ,vem.[DISPLAY_NAME] fullName " +
                         " ,vem.[POSITION] position " +
                         " ,vem.[EMAIL] email " +
                         " ,vem.[TELEPHONE] ext " + // ext
                         " ,vem.[MOBILE_PHONE] phone " +
                         " ,vem.[MANAGER] reportTo " +
                         " ,vem.[GROUP_NAME] groupName " +
                         " ,vem.[TEAM_NAME] subDept " +
                         " ,vem.[DEPT_NAME] deptName " +
                         " ,vem.[DEPT_TYPE] devision " +

                         " ,vem.[HODName] hod " +
                         " ,convert(varchar(20),em.HIRED_DATE,120) hiredDate " +
                         " ,em.[REPORT_TO] reportTo " +
                         " ,em.[ADDRESS] address " +
                         " ,em.[ACTIVE] active " +
                         " ,em.[DEPT_ID] deptId " +
                         " ,em.[JOB_TITLE] jobTitle " +
                         " ,em.[EMP_TYPE] empType " +

              " FROM [HR].[VIEW_WF_EMPLOYEE] vem";

            return sql;
        }

        private string GetAllEmployeeViewQuery()
        {
            var sql = @" SELECT E.[ID] Id 
                  ,E.[LOGIN_NAME] loginName 
                  ,E.[EMP_NO] employeeNo 
                  ,E.[DISPLAY_NAME] fullName 
                  ,E.[POSITION] position 
                  ,E.[EMAIL] email 
                  ,E.[TELEPHONE] ext 
                  ,E.[MOBILE_PHONE] phone 
                  ,E.[MANAGER] reportTo 
                  ,E.[GROUP_NAME] groupName 
                  ,E.[TEAM_ID] subDeptId 
                  ,E.[TEAM_NAME] subDept 
                  ,E.[DEPT_NAME] deptName
                  ,E.[DEPT_TYPE] devision 
                  ,E.[HOD] hod 
                  ,E.[EMP_TYPE] empType
                  ,ISNULL(R.WORKFLOW_ADMIN, 0) isAdmin
                  ,STUFF((SELECT  ',' + R.[KEY]
					FROM [ADMIN].[ROLES] R INNER JOIN [ADMIN].[USER_ROLE] UR ON UR.ROLE_ID = R.ID
					WHERE REPLACE(UR.[USER_NAME], 'K2:', '') = E.LOGIN_NAME
					FOR XML PATH('')), 1, 1, ''
                  ) AS roles
               FROM [HR].[VIEW_EMPLOYEE_ALL]  E LEFT JOIN (SELECT DISTINCT [USER_NAME], [WORKFLOW_ADMIN] FROM [BPMDATA].[WORKFLOW_RIGHT])
               R ON REPLACE(R.[USER_NAME], 'K2:','') = E.LOGIN_NAME ";

            return sql;
        }

        private string GetActiveEmployeeViewQuery() {
            var sql = @" SELECT E.[ID] Id 
                  ,E.[LOGIN_NAME] loginName 
                  ,E.[EMP_NO] employeeNo 
                  ,E.[DISPLAY_NAME] fullName 
                  ,E.[POSITION] position 
                  ,E.[EMAIL] email 
                  ,E.[TELEPHONE] ext 
                  ,E.[MOBILE_PHONE] phone 
                  ,E.[MANAGER] reportTo 
                  ,E.[GROUP_NAME] groupName 
                  ,E.[TEAM_ID] subDeptId 
                  ,E.[TEAM_NAME] subDept 
                  ,E.[DEPT_NAME] deptName
                  ,E.[DEPT_TYPE] devision 
                  ,E.[HOD] hod 
                  ,E.[EMP_TYPE] empType
                  ,ISNULL(R.WORKFLOW_ADMIN, 0) isAdmin
               FROM [HR].[VIEW_EMPLOYEE_LIST]  E LEFT JOIN (SELECT DISTINCT [USER_NAME], [WORKFLOW_ADMIN] FROM [BPMDATA].[WORKFLOW_RIGHT])
               R ON REPLACE(R.[USER_NAME], 'K2:','') = E.LOGIN_NAME ";

            return sql;
        }

        private string GetDepartmentViewQuery() {
            var sql = "SELECT  " +
                        "TEAM_ID id, " +
                        "DEPT_ID deptId, " +
                        "GROUP_ID groupId, " +
                        "TEAM_CODE teamCode, " +
                        "TEAM_NAME teamName, " +
                        "DEPT_CODE deptCode, " +
                        "DEPT_NAME deptName, " +
                        "GROUP_CODE groupCode, " +
                        "GROUP_NAME groupName, " +
                        "DEPT_TYPE deptType, " +
                        "FULL_DEPT_NAME fullName " +
                    "FROM HR.VIEW_DEPARTMENT ";
            return sql;
        }

        public IEnumerable<EmployeeDto> SearchEmployee(EmployeeQueryParameter param) {

            try {
                string sql = "";

                Object value = "%" + param.query + "%";

                if (String.IsNullOrWhiteSpace(param.query)) {
                    value = DBNull.Value;
                }



                sql = @"SELECT	Id, 
		                        loginName,
		                        employeeNo,
		                        fullName,
		                        position,
		                        email,
		                        ext,
		                        phone,
		                        reportTo,
		                        groupName,
		                        subDeptId,
		                        subDept,
                                deptName,
		                        devision,
		                        hod,
		                        empType  
                        FROM 
                        (
	                        SELECT [ID] Id, 
		                         [LOGIN_NAME] loginName, 
		                         [EMP_NO] employeeNo, 
		                         [DISPLAY_NAME] fullName ,
		                         [POSITION] position,
		                         [EMAIL] email, 
		                         [TELEPHONE] ext, 
		                         [MOBILE_PHONE] phone, 
		                         [MANAGER] reportTo,
		                         [GROUP_NAME] groupName,
		                         [TEAM_ID] subDeptId,
		                         [TEAM_NAME] subDept,
                                 [DEPT_NAME] deptName,
		                         [DEPT_TYPE] devision,
		                         [HOD] hod,
		                         [EMP_TYPE] empType,
		                         ROW_NUMBER() OVER (ORDER BY [DISPLAY_NAME]) ROW_NO 
                          FROM @VIEW_NAME@ 
                          WHERE (@value IS NULL OR EMP_NO LIKE @value OR (LOGIN_NAME LIKE @value ) OR DISPLAY_NAME LIKE @value OR EMAIL LIKE  @value)
                            AND (@EmpType = '' OR EMP_TYPE = @EmpType) AND (@LoginName = '' OR LOWER(LOGIN_NAME) <> @LoginName)
                            AND (EMP_TYPE IN @EMPLOYEE_TYPE@) AND (@EmpId = 0 OR ID = @EmpId)
                        ) AS query 
                        WHERE query.ROW_NO > @start AND query.ROW_NO <= (@start + @limit)";

                return _employeeRepository.SqlQuery<EmployeeDto>(
                    sql.Replace("@EMPLOYEE_TYPE@", (param.IncludeGenericAcct ? "('INTEGRATED', 'NONE_AD', 'GENERIC')" : "('INTEGRATED', 'NONE_AD')"))
                       .Replace("@VIEW_NAME@", (param.IncludeInactive? "[HR].[VIEW_EMPLOYEE_ALL]" : "[HR].[VIEW_EMPLOYEE_LIST]")),
                    new object[] {
                        new SqlParameter("@value", value),
                        new SqlParameter("@start", param.start),
                        new SqlParameter("@limit", param.limit),
                        new SqlParameter("@EmpType", param.Integrated == true ? "INTEGRATED": ""),
                        new SqlParameter("@LoginName", !string.IsNullOrEmpty(param.LoginName) ? param.LoginName: ""),
                        new SqlParameter("@EmpId", param.EmpId)
                    });

            } catch (SmartException e) {
                throw e;
            }
        }

        public int CountEmployee(QueryParameter param) {
            return _employeeRepository.CountEmployee(param);
        }

        public Employee GetEmployeeRaw(int id) {
            return _employeeRepository.GetById(id);
        }

        public int IsEmployeeExisted(string empNo) {
            return _employeeRepository.IsEmployeeExisted(empNo);
        }

        public ServiceResponseMsg AddNewManualEmployee(Employee emp) {
            ServiceResponseMsg _seriviceResponseMsg = new ServiceResponseMsg();

            try {
                int numEmp = IsEmployeeExisted(emp.EmpNo);

                if (numEmp == 0) {
                    _employeeRepository.Add(emp);
                    _seriviceResponseMsg.status = 1;
                    _seriviceResponseMsg.message = "Success to add new employee";
                    _seriviceResponseMsg.obj = emp;
                } else if (numEmp > 0) {
                    _seriviceResponseMsg.status = 2;
                    _seriviceResponseMsg.message = "This employee was existed";
                }
            } catch (SmartException e) {
                Debug.WriteLine(e.StackTrace);
                _seriviceResponseMsg.status = 0;
                _seriviceResponseMsg.message = "Failed to add new employee";
            }

            return _seriviceResponseMsg;
        }

        public ServiceResponseMsg UpdateManualEmployee(Employee emp) {
            ServiceResponseMsg _seriviceResponseMsg = new ServiceResponseMsg();


            try {
                _employeeRepository.Update(emp);
                _seriviceResponseMsg.status = 1;
                _seriviceResponseMsg.message = "Success to update this employee";
                _seriviceResponseMsg.obj = emp;
            } catch (SmartException e) {
                Debug.WriteLine(e.StackTrace);
                _seriviceResponseMsg.status = 0;
                _seriviceResponseMsg.message = "Failed to update this employee";
            }

            return _seriviceResponseMsg;
        }

    }
}
