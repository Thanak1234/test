using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;

namespace Workflow.Core.Attributes
{
    public class JsonFormatAttribute : ActionFilterAttribute
    {
        public bool CamelCase = true;
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null && CamelCase)
            {
                MediaTypeFormatterCollection Formatters = new MediaTypeFormatterCollection();
                Formatters
                .JsonFormatter
                .SerializerSettings
                .ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, 
                    (actionExecutedContext.Response.Content as ObjectContent).Value, 
                    Formatters.JsonFormatter);
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}