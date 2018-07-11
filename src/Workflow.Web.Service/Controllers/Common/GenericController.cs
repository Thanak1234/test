using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Workflow.Core.Collection;
using Workflow.DataObject;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models;

namespace Workflow.Web.Service.Controllers.Common
{
    public class GenericController<T, S> : AbstractServiceController<T, FormViewModel> 
        where T : AbstractWorkflowInstance
    {
        protected override FormViewModel CreateNewFormDataViewModel() {
            return new FormViewModel();
        }

        protected override IRequestFormService<T> GetRequestformService() {
            return (IRequestFormService<T>)Activator.CreateInstance(typeof(S));
        }

        protected override void MoreMapDataBC(FormViewModel viewData, T workflowInstance) {
            var properites = workflowInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (properites != null && properites.Count() > 0) {
                JObject content = (JObject)viewData.dataItem;
                properites.Each(p => {
                    try
                    {
                        p.SetValue(workflowInstance, content.ToObject(p.Name, p.PropertyType), null);
                    } catch
                    {
                        try
                        {
                            p.SetValue(workflowInstance, content.ToObject(NameHelper.TransformName(p.Name), p.PropertyType), null);
                        } catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                });
            }
        }

        protected override void MoreMapDataView(T workflowInstance, FormViewModel viewData) {
            Type type = workflowInstance.GetType();
            var properites = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (properites != null && properites.Count() > 0) {
                viewData.dataItem = new ExpandoObject();
                IDictionary<string, object> content = (IDictionary<string, object>)viewData.dataItem;
                properites.Each(p => {
                    content.Add(p.Name, type.GetProperty(p.Name).GetValue(workflowInstance, null));
                });
            }
        }
        [HttpGet]
        [Route("config")]
        public object GetViewConfiguration(string req, string activity) {
            IActivityService service = new ActivityService();
            return JObject.Parse(service.GetViewConfiguration(req, activity)).SelectToken("viewConfiguration");
        }

        [HttpGet]
        [Route(Name ="forms")]
        public object GetFormsByREQ([FromUri]QueryParameter queryParam, string requestCode) {
            IRequestHeaderService service = new RequestHeaderService();
            return Request.CreateResponse(HttpStatusCode.OK, service.GetFormsByREQ(queryParam, requestCode));
        }
    }
}
