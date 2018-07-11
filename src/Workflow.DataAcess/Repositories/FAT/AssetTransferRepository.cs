using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;

namespace Workflow.DataAcess.Repositories.FAT
{
    public class AssetTransferRepository : RepositoryBase<AssetTransfer>, IAssetTransferRepository
    {
        public AssetTransferRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public AssetTransfer GetByRequestHeader(int id)
        {
            IDbSet<AssetTransfer> dbSet = DbContext.Set<AssetTransfer>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
