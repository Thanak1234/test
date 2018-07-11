using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Workflow.Cores.Utils;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject;
using Workflow.DataObject.Roles;
using Workflow.Domain.Entities;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Models;
using Workflow.Web.Models.Roles;

namespace Workflow.Web.Service.Controllers {

    [RoutePrefix("api/roles")]
    public class RoleController : ApiController {

        private IRoleService _RoleService;

        public RoleController() {
            _RoleService = new RoleService();
        }

        [Route("treeitems")]
        public TreeItemDto GetTreeItems() {
            return _RoleService.GetTreeItems(RequestContext.Principal.Identity.Name);
        }

        [Route("users")]
        public IHttpActionResult GetUserRoles(string rolename, bool isDbRole = false) {
            if (string.IsNullOrEmpty(rolename))
                return BadRequest("Role is null or empty so operation failed.");

            var resource = _RoleService.GetUsers(rolename, isDbRole);

            return Ok(resource);
        }

        [HttpGet]
        [Route("byUser")]
        public IHttpActionResult GetRoles(string loginName) {
            if (string.IsNullOrEmpty(loginName))
                return BadRequest("Login name is null or empty so operation failed.");

            var resource = _RoleService.GetRoles(loginName, RequestContext.Principal.Identity.Name);

            return Ok(resource);
        }

        [HttpPost]
        [Route("save")]
        public IHttpActionResult Post(RoleViewModel vm) {
            if (ModelState.IsValid) {

                bool isCompleted = false;
                try {
                    if (vm.IsDbRole)
                        isCompleted = _RoleService.AddDbUserRole(vm.RoleName, vm.EmpId, vm.Include);
                    else
                        isCompleted = _RoleService.AddUserRole(vm.RoleName, vm.User, vm.Include);
                } catch (SmartException ex) {
                    return BadRequest(ex.Message);
                }


                if (!isCompleted)
                    return BadRequest("Save User Role was failed.");

                RecordTransactionHistory(vm);
                return Ok(isCompleted);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("save")]
        public IHttpActionResult Put(RoleViewModel vm) {
            try {
                _RoleService.UpdateDbUserRole(vm.RoleName, vm.EmpId, vm.Include);
                _RoleService.UpdateUserRole(vm.RoleName, vm.User, vm.Include);
                RecordTransactionHistory(vm);
            } catch (SmartException ex) {
                return BadRequest(ex.Message);
            }
            
            return Ok(true);
        }

        #region Record Change
        private void RecordTransactionHistory(RoleViewModel model)
        {
            var _transactionHistoryRepository = new TransactionHistoryRepository();
            string jsonData = JsonConvert.SerializeObject(model);


            var transactionHistory = new TransactionHistory()
            {
                ObjectType = "ACTIVITY_ROLE",
                ObjectName = "[BPMDATA].[DEPT_APPROVAL_ROLE]",
                ObjectId = model.EmpId,
                JsonData = jsonData,
                CreatedDate = DateTime.Now,
                CreatedBy = RequestContext.Principal.Identity.Name
            };
            _transactionHistoryRepository.Add(transactionHistory);
            _transactionHistoryRepository.Commit();
        }

        private void RecordTransactionHistory(DeleteViewModel model)
        {
            var _transactionHistoryRepository = new TransactionHistoryRepository();
            string jsonData = JsonConvert.SerializeObject(model);

            if (model.Users != null)
            {
                foreach (var user in model.Users)
                {
                    var transactionHistory = new TransactionHistory()
                    {
                        ObjectType = "ACTIVITY_ROLE",
                        ObjectName = "[BPMDATA].[DEPT_APPROVAL_ROLE]",
                        ObjectId = user.EmpId,
                        JsonData = jsonData,
                        CreatedDate = DateTime.Now,
                        CreatedBy = RequestContext.Principal.Identity.Name
                    };
                    _transactionHistoryRepository.Add(transactionHistory);
                    _transactionHistoryRepository.Commit();
                }
            }
        }

        private void RecordTransactionHistory(DeleteMultiRoleViewModel model)
        {
            var _transactionHistoryRepository = new TransactionHistoryRepository();
            string jsonData = JsonConvert.SerializeObject(model);
            
            var transactionHistory = new TransactionHistory()
            {
                ObjectType = "ACTIVITY_ROLE",
                ObjectName = "[BPMDATA].[DEPT_APPROVAL_ROLE]",
                ObjectId = model.EmpId,
                JsonData = jsonData,
                CreatedDate = DateTime.Now,
                CreatedBy = RequestContext.Principal.Identity.Name
            };
            _transactionHistoryRepository.Add(transactionHistory);
            _transactionHistoryRepository.Commit();
        } 
        #endregion

        [HttpPost]
        [Route("remove")]
        public IHttpActionResult Delete(DeleteViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    _RoleService.RemoveDbUserRoles(vm.RoleName, vm.Users.Select(p => Convert.ToInt32(p.EmpId)).ToArray(), vm.Description);
                    _RoleService.RemoveUserRoles(vm.RoleName, vm.Users.Select(p => SecurityLabel.GetNameWithLabel((string)p.LoginName)), vm.Description);
                    RecordTransactionHistory(vm);
                } catch (SmartException ex) {
                    return BadRequest(ex.Message);
                }                

                return Ok(true);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("multiremove")]
        public IHttpActionResult DeleteMultiRoles(DeleteMultiRoleViewModel vm) {

            if (ModelState.IsValid) {
                bool isCompleted = false;
                try {
                    string user = SecurityLabel.GetNameWithLabel(vm.LoginName);

                    IEnumerable<WorkflowRoleDto> k2Roles = vm.Roles.Where(p => p.Type == "K2").ToList();
                    IEnumerable<WorkflowRoleDto> dbRoles = vm.Roles.Where(p => p.Type == "DB").ToList();

                    if (k2Roles != null && k2Roles.Count() > 0) {
                        isCompleted = _RoleService.RemoveUserByRoles(user, k2Roles);
                    }

                    if (dbRoles != null && dbRoles.Count() > 0) {
                        isCompleted = _RoleService.RemoveDbUserByRoles(vm.EmpId, dbRoles);
                    }
                } catch (SmartException ex) {
                    return BadRequest(ex.Message);
                }

                if (!isCompleted)
                {
                    return BadRequest("Remove Role was failed.");
                }
                RecordTransactionHistory(vm);
                return Ok(isCompleted);
            }

            return BadRequest(ModelState);
        }
    }

}