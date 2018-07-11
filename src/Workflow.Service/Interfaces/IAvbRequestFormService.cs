/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject.AV;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service.Interfaces
{
    public interface IAvbRequestFormService : IRequestFormService<AvbRequestWorkflowInstance>
    {
        IEnumerable<AvbItemTypeDto> getItemType();
        IEnumerable<AvbItemDto> getItem(int itemTypeId);
        IEnumerable<AvbItemDto> getItem(string itemTypeName);
    }
}
