using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAT
{
    public class AssetTransferDetailRepository : RepositoryBase<AssetTransferDetail>, IAssetTransferDetailRepository
    {
        public AssetTransferDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<AssetTransferDetail> GetByRequestHeaderId(int id)
        {
            IDbSet<AssetTransferDetail> dbSet = DbContext.Set<AssetTransferDetail>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AssetTransferDetail>();
            }
        }
    }
}
