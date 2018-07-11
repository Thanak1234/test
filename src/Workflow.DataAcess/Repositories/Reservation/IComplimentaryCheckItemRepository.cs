using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public interface IComplimentaryCheckItemRepository : IRepository<ComplimentaryCheckItem>
    {
        IList<ComplimentaryCheckItemExt> GetComplimentaryCheckItem(int RequestHeaderId);
        ComplimentaryCheckItemLS GetPivotComplimentaryCheckItem(int RequestHeaderId);
        IList<ComplimentaryCheckItem> GetComplimentaryCheckItemByRequestHeader(int id);
        ComplimentaryCheckItem GetComplimentaryCheckItemByRequestHeader_TypeId(int RequestHeaderId, int TypeId);
    }
}
