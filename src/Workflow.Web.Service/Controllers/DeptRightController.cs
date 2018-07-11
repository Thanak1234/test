using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Workflow.DataObject.DepartmentRight;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Core;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.DepartmentRight;

namespace Workflow.Web.Service.Controllers
{

    [RoutePrefix("api/deptrights")]
    public class DeptRightController : ApiController
    {

        private IDepartmentRightService _deptRightService = null;

        private IRequestApplicationService _reqservice = null;
        
        public DeptRightController()
        {
            _deptRightService = new DepartmentRightService();
            _reqservice = new RequestApplicationService();
        }

        [HttpGet]
        [Route("forms")]
        public IHttpActionResult GetForms()
        {
            return Ok(_deptRightService.GetForms());
        }

        [HttpGet]
        [Route("department")]
        public IHttpActionResult GetDepartment()
        {
            return Ok(_deptRightService.GetDepartments());
        }

        [HttpGet]
        [Route("department")]
        public IHttpActionResult GetDepartment(string query)
        {
            if(query == null || query.Trim().Length < 1)
                return Ok(_deptRightService.GetDepartments());
            else
                return Ok(_deptRightService.GetDepartments(query));
        }

        [HttpGet]
        [Route("departmentaccess")]
        public IHttpActionResult GetDepartmentAccess([FromUri] DeptAccessRightParam q)
        {
            var result = _deptRightService.GetDeptAccessRight(q.formid, q.deptid,q.query);            
            return Ok(
                        new { count = result.Count(),
                              records = result.Skip(q.start).Take(q.limit).ToList()
                     });
        }

        [HttpPost]
        [Route("user")]
        public IHttpActionResult AddUser(DeptRightViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emp = new EmployeeService().GetEmployeeByNo(vm.empno);
                    var req = _reqservice.GetRequestApplicationById(vm.formid);

                    DeptAccessRight dar = new DeptAccessRight()
                    {
                        DeptId = vm.deptid,
                        UserId = emp.id,
                        ReqApp = req.RequestCode,
                        CreatedBy = RequestContext.Principal.Identity.Name,
                        CreatedDate = DateTime.Now,
                        Sync = false,
                        Status = (vm.active ? "ACTIVE" : "INACTIVE")
                    };

                    _deptRightService.Add(dar);

                    return Ok(true);
                }
                catch (SmartException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("user")]
        public IHttpActionResult UpdateUser(DeptRightViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DeptAccessRight dar = _deptRightService.GetDeptAccessRightById(vm.id);

                    dar.ModifiedDate = DateTime.Now;
                    dar.ModifiedBy = RequestContext.Principal.Identity.Name;
                    dar.Status = (vm.active ? "ACTIVE" : "INACTIVE");

                    _deptRightService.Update(dar);

                    return Ok(true);
                }
                catch (SmartException ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("rolemngtsync")]
        public IHttpActionResult RoleManagementSync()
        {
            try
            {
                _deptRightService.RoleManagementSync();
                return Ok(new { message = "Role Sync Successfully!", status = true });
            }catch(Exception ex)
            {
                return Ok(new { message = "Fail! Role Sync is not Success!. " + ex.Message, status = false });
            }
        }

    }

}