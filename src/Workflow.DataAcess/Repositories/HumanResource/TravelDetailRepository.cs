using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.HR;

namespace Workflow.DataAcess.Repositories.HumanResource
{
    public class TravelDetailRepository: RepositoryBase<TravelDetail>, ITravelDetailRepository
    {
        public TravelDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public TravelDetail GetByRequestHeader(int id)
        {
            IDbSet<TravelDetail> dbSet = DbContext.Set<TravelDetail>();
            //try
            //{
                //return dbSet.Single(p => p.RequestHeaderId == id);
                return dbSet.FirstOrDefault(p => p.RequestHeaderId == id);
            //}
            //catch (SmartException e)
            //{
            //    Console.WriteLine(e.Message);
            //    return null;
            //}

        }
    }
}
