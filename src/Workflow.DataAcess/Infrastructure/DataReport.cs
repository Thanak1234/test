using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public class DataReport<T> : RepositoryBase<T>, IDataReport<T> where T : class
    {
        public DataReport(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
