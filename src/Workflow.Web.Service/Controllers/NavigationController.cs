/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.DataAcess.Repositories;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Application;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/navigation")]
    public class NavigationController : ApiController
    {
        Repository _repository = null;

        public NavigationController() {
            _repository = new Repository();
        }
        
        [HttpGet]
        [Route("tree")]
        public HttpResponseMessage GetTree(string type)
        {
            //RequestContext.Principal.Identity.Name
            var parents = GetTreeList(type);
            var children = GetTreeList(type, -1);
            
            parents.Each((p) =>
            {
                p.Children = (children.Where(c => c.ParentId == p.Id));
            });
            
            return Request.CreateResponse(HttpStatusCode.OK, parents);
        }

        private IList<Navigation> GetTreeList(string type, int parentId = 0) {
            string sql = "SELECT * FROM [BPMDATA].[MENU] WHERE [Type] = @Type AND [ParentId] {0} @ParentId";
            return _repository.ExecSqlQuery<Navigation>(
                string.Format(sql, ((parentId == -1)?">":"=")),
                new SqlParameter("Type", type), new SqlParameter("ParentId", parentId));
        }
    }
}