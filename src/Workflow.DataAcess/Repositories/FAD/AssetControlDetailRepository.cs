using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAD
{
    public class AssetControlDetailRepository : RepositoryBase<AssetControlDetail>, IAssetControlDetailRepository
    {
        public AssetControlDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<AssetControlDetail> GetByRequestHeaderId(int id)
        {
            IDbSet<AssetControlDetail> dbSet = DbContext.Set<AssetControlDetail>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AssetControlDetail>();
            }
        }
    }
}
