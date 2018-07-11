using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Repositories {

    public class AVJBRequestFilesRepository: RepositoryBase<AvbUploadFile>, IAVJBRequestFilesRepository {

        public AVJBRequestFilesRepository(IDbFactory dbFactory) : base(dbFactory){}

        public IEnumerable<AvbUploadFile> GetLoadFiles(int requestHeaderId)
        {
            return GetMany(p => p.RequestHeaderId == requestHeaderId);
        }
    }
}
