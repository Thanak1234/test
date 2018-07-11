using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.RoleRights;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.Core;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.RoleRights;

namespace Workflow.Web.Service.Controllers {
    [RoutePrefix("api/rights")]
    public class RoleRightController : ApiController {
        private IRoleRightService _Service;

        public RoleRightController() {
            _Service = new RoleRightService();
        }

        [HttpGet]
        [Route("forms")]
        public IHttpActionResult GetForms() {
            return Ok(_Service.GetForms());
        }

        [HttpGet]
        [Route("activities")]
        public IHttpActionResult GetActivities() {
            return Ok(_Service.GetActivities());
        }

        [HttpGet]
        [Route("roles")]
        public IHttpActionResult GetRoles([FromUri]int actId) {
            return Ok(_Service.GetRoles(actId));
        }

        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetUsers([FromUri] UserQueryParameter queryParameter) {
            return Ok(_Service.GetUsers(queryParameter));
        }

        [HttpGet]
        [Route("rolerights")]
        public IHttpActionResult GetRoleRights(int empId) {
            return Ok(_Service.GetRoleRights(empId));
        }

        [HttpPost]
        [Route("user")]
        public IHttpActionResult AddUser(UserViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    Mapper.CreateMap<UserViewModel, ActivityRoleRight>();
                    var entity = Mapper.Map<ActivityRoleRight>(vm);
                    entity.CreatedBy = RequestContext.Principal.Identity.Name;
                    entity.CreatedDate = DateTime.Now;
                    _Service.Add(entity);
                    return Ok(true);
                } catch (SmartException ex) {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("user")]
        public IHttpActionResult UpdateUser(UserViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    Mapper.CreateMap<UserViewModel, ActivityRoleRight>();
                    var entity = Mapper.Map<ActivityRoleRight>(vm);
                    entity.ModifiedBy = RequestContext.Principal.Identity.Name;
                    entity.ModifiedDate = DateTime.Now;
                    _Service.Update(entity);
                    return Ok(true);
                } catch (SmartException ex) {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("deletes")]
        public IHttpActionResult DeleteUsers(IEnumerable<UserRightDto> users) {
            try {
                return Ok(_Service.DeleteUsers(users));
            } catch (SmartException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteRoleRights")]
        public IHttpActionResult DeleteRoleRights(IEnumerable<ActivityRightDto> rights) {
            try {
                return Ok(_Service.DeleteActivityRoleRights(rights));
            } catch (SmartException ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
