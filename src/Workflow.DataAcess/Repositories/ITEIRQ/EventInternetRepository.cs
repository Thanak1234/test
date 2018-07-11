using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.ITEIRQ;

namespace Workflow.DataAcess.Repositories.ITEIRQ
{
    public class EventInternetRepository : RepositoryBase<EventInternet>, IEventInternetRepository
    {
        public EventInternetRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public EventInternet GetByRequestHeader(int id)
        {
            IDbSet<EventInternet> dbSet = DbContext.Set<EventInternet>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
