/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.IT;
using Workflow.Domain.Entities.IT;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IItRequestFormService : IRequestFormService<ItRequestWorkflowInstance>
    {
        IEnumerable<ItemDto> GetItems(int sessionId, bool deprecated = false);
        IEnumerable<ItemTypeDto> GetItemTypes(int itemId);
        IEnumerable<ItemRoleDto> GetItemRoles(int itemId, int itemTypeId);
    }
}
