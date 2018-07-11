using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;

namespace Workflow.DataAcess.Repositories.ATCF
{
    public class AdditionalTimeWorkedRepository : RepositoryBase<AdditionalTimeWorked>, IAdditionalTimeWorkedRepository
    {
        public AdditionalTimeWorkedRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<AdditionalTimeWorked> GetByRequestHeaderId(int id)
        {
            IDbSet<AdditionalTimeWorked> dbSet = DbContext.Set<AdditionalTimeWorked>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AdditionalTimeWorked>();
            }
        }
    }
}
