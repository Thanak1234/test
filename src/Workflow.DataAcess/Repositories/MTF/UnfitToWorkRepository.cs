using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public class UnfitToWorkRepository : RepositoryBase<UnfitToWork>, IUnfitToWorkRepository
    {
        public UnfitToWorkRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<UnfitToWork> GetByRequestHeader(int id)
        {
            IDbSet<UnfitToWork> dbSet = DbContext.Set<UnfitToWork>();
            try
            {
                return dbSet.Where(p => p.RequestId == id).ToList();
            }
            catch (Exception)
            {
                return new List<UnfitToWork>();
            }

        }

    }
}
