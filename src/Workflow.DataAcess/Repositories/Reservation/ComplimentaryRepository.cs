using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Reservation;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public class ComplimentaryRepository : RepositoryBase<Complimentary>, IComplimentaryRepository
    {
        public ComplimentaryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Complimentary GetByRequestHeader(int id)
        {
            IDbSet<Complimentary> dbSet = DbContext.Set<Complimentary>();
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
