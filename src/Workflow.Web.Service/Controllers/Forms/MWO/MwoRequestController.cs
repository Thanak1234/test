using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Business.MwoRequestForm;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.MwoRequestForm;
using Workflow.Core.Attributes;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    public class MwoRequestController : GenericController<MwoRequestWorkflowInstance, MwoRequestFormService> {
        public MwoRequestController() { }
    }
}
