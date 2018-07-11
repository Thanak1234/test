

using Newtonsoft.Json;
/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Employees;
using Workflow.Domain.Entities;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        IEmployeeService _employeeService = new EmployeeService();
        IDepartmentRepository _deptRepo;

        public EmployeeController() {
            _deptRepo = new DepartmentRepositoy();
        }

        // GET api/<controller>
        public IEnumerable<EmployeeDto> Get()
        {
            IEmployeeService employeeService = new EmployeeService();
            return employeeService.GetEmployeeList();
        }
        
        [HttpGet]
        [Route("search")]
        public Object Get([FromUri]EmployeeQueryParameter param)
        {
            if(param.ExcludeOwner) {
                param.LoginName = RequestContext.Principal.Identity.Name;
            }

            IEmployeeService employeeService = new EmployeeService();
            return new {totalCount=employeeService.CountEmployee(param), data= employeeService.SearchEmployee(param)};            
        }

        [HttpGet]
        [Route("dept-list")]
        public HttpResponseMessage GetDeptList()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _deptRepo.GetDepartments());
        }

        [HttpGet]
        [Route("searchdept")]
        public IEnumerable<DepartmentDto> SearchDepartment(string query)
        {
            IEmployeeService employeeService = new EmployeeService();
            return employeeService.SearchDepartment(query);
        }

        [HttpGet]
        [Route("department-right")]
        public IEnumerable<DepartmentDto> GetDepartmentByUser(int userId = 0, string appName = null)
        {
            IEmployeeService employeeService = new EmployeeService();
            if (userId == 0) {
                var currentUser = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
                userId = currentUser.id;
                if (currentUser.isAdmin) {
                    return employeeService.GetDepartments();
                }
            }

            return employeeService.GetDepartmentRightByUser(userId, appName);
        }

        [HttpGet]
        [Route("department-level")]
        [JsonFormat]
        public object GetDepartmentLevel(string level)
        {
            var repository = new Repository();
            if (level == "GROUP")
            {
                return repository.ExecDynamicSqlQuery(@"SELECT GROUP_NAME groupName FROM HR.VIEW_DEPARTMENT GROUP BY GROUP_NAME ORDER BY GROUP_NAME");
            }
            else if (level == "DEPT")
            {
                return repository.ExecDynamicSqlQuery(@"SELECT GROUP_NAME groupName, DEPT_NAME deptName FROM HR.VIEW_DEPARTMENT GROUP BY GROUP_NAME, DEPT_NAME ORDER BY GROUP_NAME");

            }
            else if (level == "TEAM") {
                return new EmployeeService().GetDepartments();
            }

            return null;
        }

        [HttpGet]
        [Route("departments")]
        public IEnumerable<DepartmentDto> GetDepartments() {
            return new EmployeeService().GetDepartments();
        }

        [HttpGet]
        [Route("searchdept")]
        public IEnumerable<DepartmentDto> SearchDepartment()
        {
            return SearchDepartment(null);
        }

        //// GET api/<controller>/5
        //public Entities.Employee Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public ServiceResponseMsg Post(Employee emp)
        {
            var responseMsg = _employeeService.AddNewManualEmployee(emp);
            RecordTransactionHistory(responseMsg);
            return responseMsg;
        }

        // PUT api/<controller>/5
        public ServiceResponseMsg Put(Employee emp)
        {
            var responseMsg = _employeeService.UpdateManualEmployee(emp);
            RecordTransactionHistory(responseMsg);
            return responseMsg;
        }
        
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        /**
            @description : CRUD employee info only, by
        */

        [HttpGet]
        [Route("manual/{id}")]
        public Object GetEmployee(int id)
        {
            return new { Records= _employeeService.GetEmployeeRaw(id)};
        }

        [HttpGet]
        [Route("currentuser")]
        public EmployeeDto GetCurrentUser() {
            return _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
        }

        [HttpGet]
        [Route("profile")]
        public EmployeeDto GetEmployeeProfile(string empNo)
        {
            return _employeeService.getEmplyByEmpNo(empNo);
        }

        [HttpGet]
        [Route("serverdate")]
        public object GetServerDate() {
            return new { now = DateTime.Now, date = new DateTime(), today = DateTime.Today };
        }

        /* Record employee change log */
        private void RecordTransactionHistory(ServiceResponseMsg message)
        {
            var _transactionHistoryRepository = new TransactionHistoryRepository();
            string jsonData = JsonConvert.SerializeObject(message);
            var employee = (Employee)message.obj;

            var transactionHistory = new TransactionHistory()
            {
                ObjectType = "EMPLOYEE",
                ObjectName = "[HR].[EMPLOYEE]",
                ObjectId = (employee != null) ? employee.Id : 0,
                JsonData = jsonData,
                CreatedDate = DateTime.Now,
                CreatedBy = RequestContext.Principal.Identity.Name
            };
            _transactionHistoryRepository.Add(transactionHistory);
            _transactionHistoryRepository.Commit();
        }
    }

}