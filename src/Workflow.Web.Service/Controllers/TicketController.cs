/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Service.Interfaces.ticketing;
using Workflow.Service.Ticketing;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using Workflow.DataAcess.Repositories;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        private readonly ITicketLookupService ticketLookupService = null;
        private readonly ITicketService ticketService = null;
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeDto emp = null;

        public TicketController()
        {
            ticketLookupService = new TicketLookupService();
            ticketService = new TicketService();
            _employeeService = new EmployeeService();
            emp = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);

        }


        #region Ticket enquiry

        [HttpGet]
        [Route("loadTicket/{id}")]
        public HttpResponseMessage load(int id) {
            try
            {
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                var ticket = ticketService.loadTicket(id, emp);
                return Request.CreateResponse(HttpStatusCode.OK, ticket);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }

        }

        [HttpGet]
        [Route("loadActions/{id}")] //Ticket Id
        public HttpResponseMessage getActions(int id)
        {
            try
            {
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                var actions = ticketService.getActions(id, emp);
                return Request.CreateResponse(HttpStatusCode.OK, actions);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }

        }

        #endregion
        [HttpGet]
        [Route("listing")]
        public HttpResponseMessage TicketListing([FromUri] string keyword=null, int status =0, string quickQuery = null, int execptTecktId = 0, int page= 0 , int start = 0 , int limit = 0, [FromUri] List<int?> ticketTypeId = null,[FromUri] string sort = null)
        {
            try
            {
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                if (status == 0)
                {
                    if (ticketService.GetTicketEnquiry().isAgent(emp))
                    {
                        status = 1003;
                    }
                    else
                    {
                        status = 1201;
                    }
                }
                
                var result = ticketService.GetTicketEnquiry().getTicketListing(keyword, status, quickQuery, emp, execptTecktId, page, start, limit, ticketTypeId, sort);

                return Request.CreateResponse(HttpStatusCode.OK, new {sql = result.sql, data = result.tickets, totalCount = result.total} );
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
        }


        public HttpResponseMessage Post([FromBody] TicketDto ticketDto)
        {
            try
            {
                ticketDto.CurrUser = emp; // _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                if (ticketDto.SourceId==0)
                {
                    ticketDto.SourceId = 3;//from web form
                }
                var message =  ticketService.takeAction(new TicketDataParser(ticketDto));
                var values = new
                {
                    status = "success",
                    message = message
                };
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
        }
        [HttpPost]
        [Route("action")]
        public HttpResponseMessage Action([FromBody] ActivityDto dto)
        {
            try
            {
                dto.CurrUser = emp; // _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                var message = ticketService.takeAction(new TicketActivityDataParser(dto));

                var values = new
                {
                    status = "success",
                    message = message
                };
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
        }

        #region Look up
        // GET api/<controller>
        public HttpResponseMessage Get(string by="User", int ticketId =0)
        {
            try
            {
                if ("User".Equals(by))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { });
                }

                TicketViewDto formData = null;
                
                if (ticketId > 0)
                {
                    formData = ticketService.loadTicket(ticketId);
                }

                IEnumerable<GeneralLookupDto> ticketAgents = null;
                IEnumerable<GeneralLookupDto> subCates = null;
                IEnumerable<GeneralLookupDto> items = null;
                EmployeeDto requestor = null;
                int? priority = null;
                if (formData !=null)
                {
                    ticketAgents = ticketLookupService.listTicketAgent((int)formData.itemId);
                    subCates = ticketLookupService.listTicketSubCate((int)formData.categoryId, false);
                    items = ticketLookupService.listTicketItem((int)formData.subCateId);
                    IEmployeeService empService = new EmployeeService();
                    if (formData.empId > 0)
                    {
                        requestor = empService.GetEmployee(formData.empId);
                        priority = formData.priorityId;
                    }
                    
                }

                var data = new
                {
                    types = ticketLookupService.listTicketType(false),
                    sources = ticketLookupService.listTicketSource(),
                    statuses = ticketLookupService.listTicketStatus(ticketId ==0? 2:1 , formData==null?0: (int)formData.statusId),
                    impacts = ticketLookupService.listTicketImpact(),
                    urgencies = ticketLookupService.listTicketUrgency(),
                    priorities = ticketLookupService.listTicketPriority(),
                    sites = ticketLookupService.listTicketSite(),
                    teams = ticketLookupService.listTicketTeam(),
                    agents = ticketAgents,
                    cates = ticketLookupService.listTicketCategory(),
                    subCates = subCates,
                    items = items,
                    requestor = requestor,
                    formData = formData
            };

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new
                {
                    status = "success",
                    message = e.Message
                });
            }
            
        }

        // GET api/<controller>/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {

            var ticketTypes     = ticketLookupService.listTicketType();
            var ticketStatus    = ticketLookupService.listTicketStatus(false);
            var ticketSources   = ticketLookupService.listTicketSource();
            var ticketImpacts = ticketLookupService.listTicketImpact();
            var ticketUgencies  = ticketLookupService.listTicketUrgency();
            var ticketPriorities= ticketLookupService.listTicketPriority();
            var ticketSites     = ticketLookupService.listTicketSite();
            var ticketTeams     = ticketLookupService.listTicketTeam();
            var ticketCates     = ticketLookupService.listTicketCategory();
            
            return Request.CreateResponse(HttpStatusCode.OK, new {
                types = ticketTypes,
                sources = ticketSources,
                statuses = ticketStatus,
                impacts = ticketImpacts,
                ugencies = ticketUgencies,
                priorities = ticketPriorities,
                sites = ticketSites,
                teams = ticketTeams,
                cates = ticketCates
            });
        }


        [Route("lookup/getPriority")]
        public HttpResponseMessage getPriority(int impactId, int urgencyId)
        {
            try
            {
                ITicketService service = new TicketService();
                var val = service.GetTicketEnquiry().getPriority(impactId, urgencyId);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    id = val.Id,
                    priorityName = val.PriorityName
                });
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }

        }

        [Route("lookup/getSlaMapping")]
        public HttpResponseMessage getSlaMapping(int typeId, int priorityId)
        {
            try
            {
                ITicketService service = new TicketService();
                var val = service.GetTicketEnquiry().getSla(typeId, priorityId);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    id = val.Id,
                    slaName = val.SlaName,
                    gracePeriod = val.GracePeriod,
                    firstResponsePeriod = val.FirstResponsePeriod
                });
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }

        }

        [Route("lookup/getSlaFilter")]
        public HttpResponseMessage getSlaFilter([FromUri] IEnumerable<int> typeId, [FromUri] IEnumerable<int> priorityId)
        {   
            ITicketService service = new TicketService();
            //var val = service.getTicketEnquiry().getSlas(typeId, priorityId);
            return Request.CreateResponse(HttpStatusCode.OK, service.GetTicketEnquiry().getSlas(typeId, priorityId));
        }

        [HttpGet]
        [Route("setting/sla/mapping/list")]        
        public HttpResponseMessage getSlaMappingList()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketService.getSlasMapping());
        }

        [HttpGet]
        [Route("current-status")]
        public HttpResponseMessage CurrentStatus(int ticketId)
        {
            var enquiry = ticketService.GetTicketEnquiry();
            var status = enquiry.GetCurStatusByTicketId(ticketId);
            var subTicket = enquiry.GetSubtickets(ticketId);
            
            var task = enquiry.GetWorkListItem(ticketId, emp.loginName);
            
            if (task != null && "IT Implementation".IsCaseInsensitiveEqual(task.ActivityName))
            {
                var user = _employeeService.GetEmpByLoginName(task.AllocatedUser.Replace("K2:",""));
                var k2integrationDto = new
                {
                    folio = task.Folio,
                    allocatedUser = user!=null? user.fullName + " (" + user.employeeNo +")" :task.AllocatedUser,
                    clossable = task.AllocatedUser.ToLower().IndexOf(emp.loginName.ToLower()) >=0,
                    status = task.Status,
                    formUrl = task.OpenFormUrl,
                    serial = task.Serial
                };
                return Request.CreateResponse(HttpStatusCode.OK, new { status = status, subTicket = subTicket, k2Integrated = k2integrationDto });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { status = status, subTicket = subTicket});
        }

        [HttpGet]
        [Route("current-assigned")]
        public HttpResponseMessage GetCurrAssgned(int ticketId)
        {
            try
            {
                ITicketService service = new TicketService();
                var val = service.getTicketAssignment(ticketId);
                return Request.CreateResponse(HttpStatusCode.OK, new {
                    teamId= val.TeamId,
                    agentId = val.AssigneeId
                });
            }
            catch(Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
            
        }

        [HttpGet]
        [Route("lookup/init-data")]
        public HttpResponseMessage initData()
        {
            //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
            var data = new
            {
                isAgent = ticketService.GetTicketEnquiry().isAgent(emp)
            };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpGet]
        [Route("lookup/team-list")]
        public HttpResponseMessage teamList()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketTeam());
        }

        /*Lookup*/
        [HttpGet]
        [Route("lookup/agent-list")]
        public HttpResponseMessage agentList(int teamId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketAgent(teamId));
        }


        [HttpGet]
        [Route("lookup/sub-cate")]
        public HttpResponseMessage subCateList(int cateId, bool breadcrumb = false)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketSubCate(cateId, breadcrumb));
        }

        [HttpGet]
        [Route("lookup/sub-cates")]
        public HttpResponseMessage subCateLists([FromUri]IEnumerable<int> cateId, bool breadcrumb = false)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketSubCate(cateId, breadcrumb));
        }

        [HttpGet]
        [Route("lookup/item")]
        public HttpResponseMessage itemList(int subCateId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketItem(subCateId));
        }

        [HttpGet]
        [Route("lookup/items")]
        public HttpResponseMessage itemLists([FromUri] IEnumerable<int> cateId, [FromUri] IEnumerable<int> subCateId, bool breadcrumb = false)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketItem(cateId, subCateId, breadcrumb));
        }


        [HttpGet]
        [Route("lookup/ticket_type")]
        public HttpResponseMessage ticketType()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketType());
        }


        [HttpGet]
        [Route("lookup/ticket_status")]
        public HttpResponseMessage ticketStatus(bool isFillter = false)
        {

            var statusList = ticketLookupService.listTicketStatus(isFillter);



            List<TicketStatus> rt = new List<TicketStatus>();
            if (isFillter)
            {
                rt.Add(new TicketStatus()
                {
                    Id = 1200,
                    Status = "All Tickets I Request",
                    Classify = "OwnQickAccess"
                });

                rt.Add(new TicketStatus()
                {
                    Id = 1201,
                    Status = "Active Tickets I Request",
                    Classify = "OwnQickAccess"
                });

                rt.Add(new TicketStatus()
                {
                    Id = 1202,
                    Status = "Inactive Tickets I Request",
                    Classify = "OwnQickAccess"
                });

                rt.Add(new TicketStatus()
                {
                    Id = 1203,
                    Status = "Deleted Tickets I Request",
                    Classify = "OwnQickAccess"
                });

                TicketService ticketService = new TicketService();
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                var isAgent = ticketService.GetTicketEnquiry().isAgent(emp);

                if (isAgent)
                {
                    rt.Add(new TicketStatus()
                    {
                        Id = 1000,
                        Status = "Tickets Assigned To me",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus() {
                        Id = 1102,
                        Status = "Tickets Assigned To My Team",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1001,
                        Status = "All Assigned Tickets",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1002,
                        Status = "All Unassigned Tickets",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1003,
                        Status = "All Active Tickets",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1004,
                        Status = "All Inactive Tickets",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1005,
                        Status = "All Deleted Tickets",
                        Classify = "QickAccess"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1100,
                        Status = "My Overdue Tickets",
                        Classify = "OverdueFilter"
                    });

                    rt.Add(new TicketStatus()
                    {
                        Id = 1101,
                        Status = "Overdue Tickets",
                        Classify = "OverdueFilter"
                    });
                }
            }
            rt.AddRange(statusList);

            return Request.CreateResponse(HttpStatusCode.OK, rt);
        }


        [HttpGet]
        [Route("lookup/dept-list")]
        public HttpResponseMessage ticketDepartment()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketDept());
        }

        [HttpGet]
        [Route("lookup/cate-list")]
        public HttpResponseMessage ticketCategory()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketCategory());
        }

        [HttpGet]
        [Route("lookup/grouppolicy-list")]
        public HttpResponseMessage ticketGroupPolicy()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketGroupPolicy());
        }

        [HttpGet]
        [Route("lookup/status-list")]
        public HttpResponseMessage ticketStatus()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketStatus());
        }

        [HttpGet]
        [Route("lookup/accounttype-list")]
        public HttpResponseMessage ticketAccountType()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.listTicketAccountType());
        }

        [HttpGet]
        [Route("lookup/ticket-list")]
        public HttpResponseMessage ticketLookupList(string lookupKey)
        {
            var list = ticketLookupService.listTicketByKey(lookupKey);
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        /**===================== Setting =====================**/
        [HttpGet]
        [Route("setting/getItems")]
        public HttpResponseMessage getItems([FromUri]TicketSettingCriteria criteria)
        {
            var list = ticketService.getTicketItems(criteria);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/item/create")]
        public HttpResponseMessage createSettingItem([FromBody]TicketItem item)
        {
            if (ticketService.isItemExisted(item))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Item was existed" });
            }
            else
            {
                TicketItem instance = ticketService.addNewTicketItem(item);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }

        [HttpPut]
        [Route("setting/item/update/{id:int}")]
        public HttpResponseMessage updateSettingItem(int id, [FromUri] TicketItem item)
        {
            if (ticketService.isItemExisted(item))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Item was existed" });
            }
            else
            {
                TicketItem instance = ticketService.updateTicketItem(item);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }

        [HttpGet]
        [Route("setting/subcate/list")]
        public HttpResponseMessage getSubCategories([FromUri]TicketSettingCriteria criteria)
        {
            var list = ticketService.getSubCategories(criteria);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/subcate/create")]
        public HttpResponseMessage createSettingSubCate([FromBody]TicketSubCategory subCate)
        {
            if (ticketService.isSubCategoryExisted(subCate))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Sub Category was existed" });
            }
            else
            {
                TicketSubCategory instance = ticketService.addNewSubCategory(subCate);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }

        [HttpPut]
        [Route("setting/subcate/update/{id:int}")]
        public HttpResponseMessage updateSettingSubCate(int id, [FromUri] TicketSubCategory subCate)
        {
            if (ticketService.isSubCategoryExisted(subCate))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Sub Category was existed" });
            }
            else
            {
                TicketSubCategory instance = ticketService.updateSubCategory(subCate);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }


        [HttpGet]
        [Route("setting/cate/list")]
        public HttpResponseMessage getCategories([FromUri]TicketSettingCriteria criteria)
        {
            var list = ticketService.getCategories(criteria);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/cate/create")]
        public HttpResponseMessage createSettingCate([FromBody]TicketCategory cate)
        {
            if (ticketService.isCategoryExisted(cate))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Category was existed" });
            }
            else
            {
                TicketCategory instance = ticketService.addNewCategory(cate);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }

        [HttpPut]
        [Route("setting/cate/update/{id:int}")]
        public HttpResponseMessage updateSettingCate(int id, [FromUri] TicketCategory cate)
        {
            if (ticketService.isCategoryExisted(cate))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Category was existed" });
            }
            else
            {
                TicketCategory instance = ticketService.updateCategory(cate);
                return Request.CreateResponse(HttpStatusCode.OK, instance);
            }
        }

        [HttpGet]
        [Route("setting/agent/list")]
        public HttpResponseMessage getAgents(string query = null)
        {
            var list = ticketService.getAgents(query);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/agent/create")]
        public HttpResponseMessage createSettingAgent([FromBody]TicketAgent instance)
        {
            if (ticketService.isAgentExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This agent was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewAgent(instance));
            }
        }

        [HttpPut]
        [Route("setting/agent/update/{id:int}")]
        public HttpResponseMessage updateSettingAgent(int id, [FromUri] TicketAgent instance)
        {
            if (ticketService.isAgentExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This agent was existed"});
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateAgent(instance));
            }
            
        }

        [HttpGet]
        [Route("setting/agent/team-list")]
        public HttpResponseMessage getTeamByAgents(int agentId = 0)
        {
            var list = ticketService.getAgentTeams(agentId);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }



        [HttpGet]
        [Route("setting/dept/list")]
        public HttpResponseMessage getDepartments([FromUri]TicketSettingCriteria criteria)
        {
            var list = ticketService.getDepartments(criteria);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/dept/create")]
        public HttpResponseMessage createSettingDepartment([FromBody]TicketDepartment instance)
        {
            if (ticketService.isDepartmentExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Department was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewDepartment(instance));
            }
        }

        [HttpPut]
        [Route("setting/dept/update/{id:int}")]
        public HttpResponseMessage updateSettingDepartment(int id, [FromUri] TicketDepartment instance)
        {
            if (ticketService.isDepartmentExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Department was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateDepartment(instance));
            }
        }

        [HttpGet]
        [Route("setting/grouppolicy/list")]
        public HttpResponseMessage getGroupPolicies(string query = null)
        {
            var list = ticketService.getGroupPolicies(query);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/grouppolicy/create")]
        public HttpResponseMessage createSettingGroupPolicy([FromBody]TicketGroupPolicyDto instance)
        {
            if (ticketService.isGroupPolicyExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Group Policy was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewGroupPolicy(instance));
            }
        }

        [HttpPost]
        [Route("setting/grouppolicy/update")]
        public HttpResponseMessage updateSettingGroupPolicy([FromBody] TicketGroupPolicyDto instance)
        {
            if (ticketService.isGroupPolicyExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Group Policy was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateGroupPolicy(instance));
            }
        }

        [HttpGet]
        [Route("setting/team/list")]
        public HttpResponseMessage getTeams([FromUri]TicketSettingCriteria criteria)
        {
            var list = ticketService.getTeams(criteria);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpPost]
        [Route("setting/team/create")]
        public HttpResponseMessage createSettingTeam([FromBody]TicketTeamDto instance)
        {
            if (ticketService.isTeamExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Team was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewTeamAgents(instance));
            }
        }

        [HttpPost]
        [Route("setting/team/update")]
        public HttpResponseMessage updateSettingTeam([FromBody] TicketTeamDto instance)
        {
            if (ticketService.isTeamExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Team was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateTeamAgents(instance));
            }
        }

        [HttpGet]
        [Route("setting/team/agent-list")]
        public HttpResponseMessage getAgentsByTeam(int teamId = 0)
        {
            var list = ticketService.getTeamAgents(teamId);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }
        

        [HttpPost]
        [Route("setting/sla/create")]
        public HttpResponseMessage createSettingSla([FromBody]TicketSLA instance)
        {
            if (ticketService.isSLAExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This SLA was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewSla(instance));
            }
        }

        [HttpPut]
        [Route("setting/sla/update/{id:int}")]
        public HttpResponseMessage updateSettingSla(int id, [FromUri] TicketSLA instance)
        {
            if (ticketService.isSLAExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This SLA was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateSla(instance));
            }
        }

        [HttpPost]
        [Route("setting/sla/mapping/create")]
        public HttpResponseMessage createSettingSlaMapping([FromBody]TicketSLAMapping instance)
        {
            if (ticketService.isSLAMappingExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This SLA Mapping was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewSlaMapping(instance));
            }
        }

        [HttpPut]
        [Route("setting/sla/mapping/update/{id:int}")]
        public HttpResponseMessage updateSettingSlaMapping(int id, [FromUri] TicketSLAMapping instance)
        {
            if (ticketService.isSLAMappingExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This SLA Mapping was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updateSlaMapping(instance));
            }
        }

        [HttpGet]
        [Route("setting/sla/list")]
        public HttpResponseMessage getSlas(string query = null)
        {
            var list = ticketService.getSlas(query);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }


        [HttpPost]
        [Route("setting/priority/create")]
        public HttpResponseMessage createSettingPriority([FromBody]TicketPriority instance)
        {
            if (ticketService.isPriorityExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Priority was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.addNewPriority(instance));
            }
        }

        [HttpPut]
        [Route("setting/priority/update/{id:int}")]
        public HttpResponseMessage updateSettingPriority(int id, [FromUri] TicketPriority instance)
        {
            if (ticketService.isPriorityExisted(instance))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, new { msg = "This Priority was existed" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ticketService.updatePriority(instance));
            }
        }

        [HttpGet]
        [Route("setting/priority/list")]
        public HttpResponseMessage getPriorities(string query = null)
        {
            var list = ticketService.getPriorities(query);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpGet]
        [Route("setting/grouppolicy/team-list")]
        public HttpResponseMessage getTeamsByGroupPolicy(int groupPolicyId = 0)
        {
            var list = ticketService.getGroupPolicyTeams(groupPolicyId);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        [HttpGet]
        [Route("setting/grouppolicy/report-team-list")]
        public HttpResponseMessage getReportAccessTeamsByGroupPolicy(int groupPolicyId = 0)
        {
            var list = ticketService.getGroupPolicyReportAccessTeams(groupPolicyId);
            var re = new
            {
                data = list,
                totalCount = list.Count(),
            };
            return Request.CreateResponse(HttpStatusCode.OK, re);
        }

        //[HttpGet]
        //[Route("report/search")]
        //public HttpResponseMessage searchTicket([FromUri]TicketingSearchParamsDto @params)
        //{
        //    var queryResult = ticketService.GetReportResult(@params);
        //    return Request.CreateResponse(HttpStatusCode.OK, new { totalCount = queryResult.Count(), data = queryResult.Skip(@params.start).Take(@params.limit).ToList()});
        //}

        //[HttpGet]
        //[Route("report/export")]
        //public HttpResponseMessage Export([FromUri]TicketingSearchParamsDto @params) {
        //    byte[] buffer = Export(@params, "/REPORTS/TICKET_REPORT", @params.exportType);
        //    return ExportFile(buffer, "TICKET_REPORT", @params.exportType);
        //}

        public HttpResponseMessage ExportFile(byte[] buffer, string fileName, string extension) {
            var stream = new MemoryStream(buffer);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = string.Concat(fileName, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".", extension)
            };

            return result;
        }

        public object GetPropValue(object source, string propertyName) {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            if (property != null) {
                return property.GetValue(source);
            }
            return null;
        }

        public byte[] Export(object param, string reportPath, string type) {

            string rsUri = ConfigurationManager.AppSettings["rsUri"];
            ReportViewer report = new ReportViewer();
            report.ProcessingMode = ProcessingMode.Remote;
            report.ServerReport.ReportPath = reportPath;
            report.ServerReport.ReportServerUrl = new Uri(rsUri);
            List<ReportParameter> @params = new List<ReportParameter>();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            pInfo = report.ServerReport.GetParameters();

            if (param != null) {
                Type parameterType = param.GetType();
                var properties = parameterType.GetProperties();
                foreach (var property in properties) {
                    string name = property.Name;
                    object value = GetPropValue(param, name);

                    ReportParameterInfo rpInfo = pInfo[name];

                    if (rpInfo == null) continue;

                    if (value != null && value.ToString().ToUpper() != "NULL") {
                        @params.Add(new ReportParameter(name, value.ToString(), true));
                    } else {
                        ReportParameter rparam = new ReportParameter(name);
                        rparam.Values.Add(null);
                        @params.Add(rparam);
                    }
                }
            }

            report.ServerReport.SetParameters(@params);
            report.ServerReport.Refresh();

            string format = "";
            string extension = "";
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string[] streams = null;
            Warning[] warnings = null;

            if (type.ToLower() == "xls") {
                format = "EXCEL";
                extension = "xls";
            } else if (type.ToLower() == "pdf") {
                format = "PDF";
                extension = "pdf";
            }

            return report.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        }

        #endregion


        #region Dashboad
        [HttpGet]
        [Route("dashboard/item-overview")]
        public HttpResponseMessage getItemOverview(int type)
        {
            try
            {
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                ITicketService service = new TicketService();
                var results = service.GetTicketEnquiry().getTicketItemDashboard((TicketItemDashboard.TICKET_ITEM_DHB)type, emp);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
        }


        [HttpGet]
        [Route("dashboard/item-performance")]
        public HttpResponseMessage getItemPerformance(int type=1, int timeFrame=1, int timeFilter =101) // 1 = items, 2. agents
        {
            try
            {
                //var emp = _employeeService.getEmplyByLoginName(RequestContext.Principal.Identity.Name);
                ITicketService service = new TicketService();
                var results = service.GetTicketEnquiry().getItemPerformance(type, (HierarchyDashB.TIME_FRAME)timeFrame, (HierarchyDashB.TIME_FILTER)timeFilter, emp);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
        }

        [HttpGet]
        [Route("lookup/root-causes")]
        public HttpResponseMessage GetRootCauses()
        {
            var result = new Repository().ExecDynamicSqlQuery(@"
                                        SELECT
                                            [ID] causeId,
	                                        [CAUSE] [cause]      
                                        FROM [Workflow].[TICKET].[ROOT_CAUSE]
                                        WHERE [STATUS] = 'ACTIVE'
                                        ORDER BY [DESCRIPTION]");

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        #endregion
    }
}