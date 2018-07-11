using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.HGVR
{
    public class VoucherHotelDetailRepository : RepositoryBase<VoucherHotelDetail>, IVoucherHotelDetailRepository
    {
        public VoucherHotelDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<VoucherHotelDetail> GetByRequestHeaderId(int id)
        {
            IDbSet<VoucherHotelDetail> dbSet = DbContext.Set<VoucherHotelDetail>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<VoucherHotelDetail>();
            }
        }
    }
}
