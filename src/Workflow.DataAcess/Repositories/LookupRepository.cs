using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class LookupRepository : RepositoryBase<Lookup>, ILookupRepository
    {
        public LookupRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public IDbSet<Lookup> Lookups()
        {
            return DbContext.Set<Lookup>();
        }
        
        public Lookup GetLookup(string packageName)
        {
            return DbContext.Set<Lookup>().Single(t => t.Name == packageName);
        }

        public IDbSet<E> GetDataSet<E>() where E : class
        {
            return DbContext.Set<E>();
        }
    }
    
}
