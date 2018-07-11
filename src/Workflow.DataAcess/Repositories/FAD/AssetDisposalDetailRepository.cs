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
    public class AssetDisposalDetailRepository : RepositoryBase<AssetDisposalDetail>, IAssetDisposalDetailRepository
    {
        public AssetDisposalDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<AssetDisposalDetail> GetByRequestHeaderId(int id)
        {
            IDbSet<AssetDisposalDetail> dbSet = DbContext.Set<AssetDisposalDetail>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AssetDisposalDetail>();
            }
        }
    }
}
