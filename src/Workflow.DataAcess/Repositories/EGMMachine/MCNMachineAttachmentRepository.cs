using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EGM;

namespace Workflow.DataAcess.Repositories.EGMMachine
{
    public class MCNMachineAttachmentRepository : RepositoryBase<MachineAttachment>,IMCNMachineAttachmentRepository
    {
        public MCNMachineAttachmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }
    }
}
