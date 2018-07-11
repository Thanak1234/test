using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core.Upload;

namespace Workflow.DataAcess.Repositories.Interfaces
{
    public interface IAttachementRepository<T> : IRepository<T> where T : FileUpload
    {
        IEnumerable<FileTemp> GetFileAttachementsBySerials(IEnumerable<string> serials);
        IEnumerable<T> GetAttachementsByRequest(int requestHeaderId);
        AttachementView GetAttachement(string attachmentId);
        void DeleteAttachement(int requestHeaderId, string attachmentType);
        void DeleteAttachement(string serial);
        void InsertAttachement(string serial, string attachmentType, int requestHeaderId);
    }
}
