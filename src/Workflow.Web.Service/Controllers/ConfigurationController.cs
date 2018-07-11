/**
*@author : Yim Samaune
*/

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Service;
using Workflow.DataObject.Worklists;
using Workflow.DataAcess.Repositories;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/configuration")]
    public class ConfigurationController : ApiController
    {
        private static IEnumerable<ActivityDto> activities = null;
        private readonly WorklistService _service;

        public ConfigurationController() {
            _service = new WorklistService();
        }

        [HttpGet]
        [Route("activities-properties")]
        public HttpResponseMessage GetActivitiesProperties(bool cache = true)
        {
            if (!cache) {
                activities = null;
            }

            if (activities == null)
            {
                activities = _service.GetActivities().Where(p => p.Property != null);
            }
            return Request.CreateResponse(HttpStatusCode.OK, activities);
        }

        [HttpGet]
        [Route("setting/version")]
        public string GetVersion()
        {
            var settings = new Repository().ExecDynamicSqlQuery(@"SELECT CONTENT [content] FROM [SYSTEM].[SETTINGS] WHERE MODULE = 'APP' AND [KEY] = 'APP_VERSION'");
            string version = settings.FirstOrDefault().content;

            return version;
        }

    }
}

