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
using Workflow.Domain.Entities.EOMRequestForm;

namespace Workflow.DataAcess.Repositories
{
    public class EOMRequestInformationRepository : RepositoryBase<EOMDetail>, IEOMRequestInformationRepository {
        public EOMRequestInformationRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public EOMDetail GetByRequestHeaderId(int id)
        {
            IDbSet<EOMDetail> dbSet = DbContext.Set<EOMDetail>();
            try
            {
                var result = dbSet.FirstOrDefault(p => p.RequestHeaderId == id);
                return result;
            }
            catch (SmartException e)
            {
                throw e;
            }
        }
    }
}
