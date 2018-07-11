using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.HR;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public interface ITravelDetailRepository: IRepository<TravelDetail>
    {
        TravelDetail GetByRequestHeader(int id);
    }
}
