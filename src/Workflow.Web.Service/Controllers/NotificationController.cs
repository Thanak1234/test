using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Service.Interfaces.Notification;
using Workflow.Service.Notification;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/notification")]
    public class NotificationController : ApiController
    {

        private readonly INotificationService notificationService;
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeDto emp;
        public NotificationController()
        {
            notificationService = new NotificationService();
            _employeeService = new EmployeeService();

            emp = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
            
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                var list = notificationService.getNotifyList(emp);
                return Request.CreateResponse(HttpStatusCode.OK, list);
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
        [Route("read/{id}")] //Ticket Id
        public HttpResponseMessage read(int id)
        {
            try
            {
                notificationService.markAsRead(id);
                return Request.CreateResponse(HttpStatusCode.OK);
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
        [Route("count")] //Ticket Id
        public HttpResponseMessage count()
        {
            try
            {
                var unread = notificationService.getUnread(emp);
                return Request.CreateResponse(HttpStatusCode.OK, unread);
            }catch(Exception e)
            {
                var values = new
                {
                    status = "failed",
                    message = e.Message
                };
                return Request.CreateResponse(HttpStatusCode.OK, 0);
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, values);
            }
            
            
        }

    }
}