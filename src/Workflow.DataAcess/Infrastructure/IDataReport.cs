using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public interface IDataReport<T> : IRepository<T> where T : class
    {
        
    }
}
