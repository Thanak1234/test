using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Business.N2MwoRequestForm;
using Workflow.Domain.Entities.N2MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.MwoRequestForm;
using Workflow.Core.Attributes;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    public class N2MwoRequestController : GenericController<N2MwoRequestWorkflowInstance, N2MwoRequestFormService> {
        public N2MwoRequestController() { }
    }
}
