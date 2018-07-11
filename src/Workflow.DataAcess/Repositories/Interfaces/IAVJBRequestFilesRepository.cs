/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IAVJBRequestFilesRepository: IRepository<AvbUploadFile> {
        IEnumerable<AvbUploadFile> GetLoadFiles(int requestHeaderId);
    }
}
