using System;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.RMD;

namespace Workflow.DataAcess.Repositories.RMD
{
    public class RiskAssessmentRepository : RepositoryBase<RiskAssessment>, IRiskAssessmentRepository
    {
        public RiskAssessmentRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public RiskAssessment GetByRequestHeader(int id)
        {
            IDbSet<RiskAssessment> dbSet = DbContext.Set<RiskAssessment>();
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
