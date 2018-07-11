using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public class Worksheet3Repository : RepositoryBase<Worksheet3>, IWorksheet3Repository
    {
        public Worksheet3Repository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Worksheet3> GetByRequestHeaderId(int id)
        {
            IDbSet<Worksheet3> dbSet = DbContext.Set<Worksheet3>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Worksheet3>();
            }
        }
    }
}
