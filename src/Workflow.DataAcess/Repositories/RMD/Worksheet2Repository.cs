using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public class Worksheet2Repository : RepositoryBase<Worksheet2>, IWorksheet2Repository
    {
        public Worksheet2Repository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Worksheet2> GetByRequestHeaderId(int id)
        {
            IDbSet<Worksheet2> dbSet = DbContext.Set<Worksheet2>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Worksheet2>();
            }
        }
    }
}
