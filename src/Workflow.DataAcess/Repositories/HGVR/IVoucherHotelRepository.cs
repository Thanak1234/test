using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.HGVR
{
    public interface IVoucherHotelRepository : IRepository<VoucherHotel>
    {
        VoucherHotel GetByRequestHeader(int id);
    }
}
