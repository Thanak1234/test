using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories
{
    public interface ISimpleRepository<T> : IRepository<T> where T : class
    {

    }
}
