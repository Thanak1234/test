using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.EOMRequestForm;

namespace Workflow.DataAcess.Repositories {

    public class EOMRequestFilesRepository : RepositoryBase<EOMUploadFile>, IEOMRequestFilesRepository {

        public EOMRequestFilesRepository(IDbFactory dbFactory) : base(dbFactory){}

        public IEnumerable<EOMUploadFile> GetLoadFiles(int requestHeaderId)
        {
            return GetMany(p => p.RequestHeaderId == requestHeaderId);
        }
    }
}
