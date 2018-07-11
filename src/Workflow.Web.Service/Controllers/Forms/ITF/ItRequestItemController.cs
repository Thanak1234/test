/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.IT;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/itrequestitem")]
    public class ItRequestItemController : ApiController
    {

        [HttpGet]
        [Route("items")]
        public IEnumerable<ItemDto> GetItems(int sessionId)
        {
            IItRequestFormService requestFormService = new ItRequestFormService();
            return requestFormService.GetItems(sessionId);
        }

        [HttpGet]
        [Route("items-incl-deprecated")]
        public IEnumerable<ItemDto> GetItemsInclDeprecated(int sessionId)
        {
            IItRequestFormService requestFormService = new ItRequestFormService();
            return requestFormService.GetItems(sessionId, true);
        }

        [HttpGet]
        [Route("itemtypes")]
        public IEnumerable<ItemTypeDto> GetItemTypes(int itemId)
        {
            IItRequestFormService requestFormService = new ItRequestFormService();
            return requestFormService.GetItemTypes(itemId);
        }

        [HttpGet]
        [Route("itemroles")]
        public IEnumerable<ItemRoleDto> GetItemRoles(int itemId, int itemTypeId)
        {
            IItRequestFormService requestFormService = new ItRequestFormService();
            return requestFormService.GetItemRoles(itemId, itemTypeId);
        }
    }

}