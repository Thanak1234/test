/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.EOMRequestForm;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IEOMRequestInformationRepository : IRepository<EOMDetail>
    {
        EOMDetail GetByRequestHeaderId(int id);
    }
}
