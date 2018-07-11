using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public class Worksheet1Repository : RepositoryBase<Worksheet1>, IWorksheet1Repository
    {
        public Worksheet1Repository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Worksheet1> GetByRequestHeaderId(int id)
        {
            IDbSet<Worksheet1> dbSet = DbContext.Set<Worksheet1>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Worksheet1>();
            }
        }
    }
}
