using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataAcess;
using Workflow.DataObject.Errors;

namespace Workflow.Web.Service.Controllers.Common
{
    public class BaseController : ApiController
    {
        protected HttpResponseMessage CreateErrorMessageResponse (Exception ex) {
            GeneralErrorMessage errorMessage = new GeneralErrorMessage();
            errorMessage.Code = 101;
            errorMessage.Message = ex.Message;
            errorMessage.Detail = ex.StackTrace;

            return Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
        }

        protected HttpResponseMessage CreateModalStateErrorResponse() {
            ModalStateErrorMessage errorMessage = new ModalStateErrorMessage();
            errorMessage.Code = 102;
            errorMessage.Message = "Validation is not valid.";
            errorMessage.Detail = ModelState;

            return Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
        }

        protected IList<dynamic> ExecDynamicSqlQuery(string sqlQuery, params object[] parameters)
        {
            using (var dbContext = new WorkflowContext())
            {
                var dataCollection = new List<dynamic>();
                dynamic result = dbContext.Database.DynamicSqlQuery(sqlQuery, parameters);
                foreach (dynamic data in result)
                {
                    dataCollection.Add(data);
                }

                return dataCollection;
            }
        }
    }
}
