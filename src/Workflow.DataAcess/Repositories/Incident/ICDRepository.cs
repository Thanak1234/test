using Workflow.DataAcess.Infrastructure;
namespace Workflow.DataAcess.Repositories.Incident
{
    public class ICDRepository : RepositoryBase<Workflow.Domain.Entities.INCIDENT.Incident>,IICDRepository
    {
        public ICDRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
