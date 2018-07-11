using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public class PrescriptionRepository : RepositoryBase<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Prescription> GetByTreatmentId(int id)
        {
            IDbSet<Prescription> dbSet = DbContext.Set<Prescription>();
            try
            {
                return dbSet.Where(p => p.TreatmentId == id).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Prescription>();
            }
            
        }
    }
}
