using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.OSHA;

namespace Workflow.DataAcess.Repositories.OSHA
{
    public class OSHAInformationRepository : RepositoryBase<OSHAInformation>, IOSHAInformationRepository
    {
        public OSHAInformationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public OSHAInformation GetByRequestHeader(int id)
        {
            IDbSet<OSHAInformation> dbSet = DbContext.Set<OSHAInformation>();
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
