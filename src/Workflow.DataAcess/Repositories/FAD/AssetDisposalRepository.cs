using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAD
{
    public class AssetDisposalRepository : RepositoryBase<AssetDisposal>, IAssetDisposalRepository
    {
        public AssetDisposalRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public AssetDisposal GetByRequestHeader(int id)
        {
            IDbSet<AssetDisposal> dbSet = DbContext.Set<AssetDisposal>();
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
