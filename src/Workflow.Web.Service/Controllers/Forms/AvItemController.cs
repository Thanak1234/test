/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.AV;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/avitem")]
    public class AvItemController : ApiController
    {
        [HttpGet]
        [Route("items")]
        public IEnumerable<AvbItemDto> GetItems(int itemTypeId)
        {
            IAvbRequestFormService avbRequestFormService = new AvbRequestFormService();
            return avbRequestFormService.getItem(itemTypeId);
        }

        [HttpGet]
        [Route("items-by-name")]
        public IEnumerable<AvbItemDto> GetItems(string itemTypeName)
        {
            IAvbRequestFormService avbRequestFormService = new AvbRequestFormService();
            return avbRequestFormService.getItem(itemTypeName);
        }

        [HttpGet]
        [Route("itemtypes")]
        public IEnumerable<AvbItemTypeDto> GetItemTypes()
        {
            IAvbRequestFormService avbRequestFormService = new AvbRequestFormService();
            return avbRequestFormService.getItemType();
        }
    }
}