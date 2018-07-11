/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Repositories
{
    public class AvbJobDetailRepository : RepositoryBase<AvbJobHistory>, IAvbJobDetailRepository
    {
        public AvbJobDetailRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public AvbJobHistory GetByRequestHeader(int id)
        {
            IDbSet<AvbJobHistory> dbSet = DbContext.Set<AvbJobHistory>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
