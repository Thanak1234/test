using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.EOMRequestForm;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IEOMRequestFilesRepository : IRepository<EOMUploadFile> {
        IEnumerable<EOMUploadFile> GetLoadFiles(int requestHeaderId);
    }
}
