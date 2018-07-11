/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Domain.Entities;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models;

namespace Workflow.Web.Service.Controllers
{
    public class RequestHeaderController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            IRequestHeaderService requestHeaderService = new RequestHeaderService();
            RequestHeader requestHeader = requestHeaderService.GetRequestHeader(id);
            return requestHeader.Title ;
        }

        // POST api/<controller>
        public AbstractFormDataViewModel Post([FromBody]ItRequestFormViewModel value)
        {

           return value;

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}