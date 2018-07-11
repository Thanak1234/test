using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.VoucherRequest;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IRequestDataRepository: IRepository<RequestData> {
        int GetTotalQty();
    }
}
