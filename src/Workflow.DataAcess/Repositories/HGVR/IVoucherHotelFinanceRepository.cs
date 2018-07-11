using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.HGVR
{
    public interface IVoucherHotelFinanceRepository : IRepository<VoucherHotelFinance>
    {
        IList<VoucherHotelFinance> GetByRequestHeaderId(int id);
    }
}
