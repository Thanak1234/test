using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;

namespace Workflow.DataAcess.Repositories
{
    public class SimpleRepository<T> : RepositoryBase<T>, ISimpleRepository<T> where T : class
    {
        public SimpleRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
