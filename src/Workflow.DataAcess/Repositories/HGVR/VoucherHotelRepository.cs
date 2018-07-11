using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.HGVR
{
    public class VoucherHotelRepository : RepositoryBase<VoucherHotel>, IVoucherHotelRepository
    {
        public VoucherHotelRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public VoucherHotel GetByRequestHeader(int id)
        {
            IDbSet<VoucherHotel> dbSet = DbContext.Set<VoucherHotel>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
