using System;
using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.HGVR
{
    public interface IVoucherHotelDetailRepository : IRepository<VoucherHotelDetail>
    {
        IList<VoucherHotelDetail> GetByRequestHeaderId(int id);
    }
}
