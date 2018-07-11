using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core.Upload;

namespace Workflow.DataAcess.Repositories
{
    public class AttachementRepository<T> : RepositoryBase<T>, IAttachementRepository<T> where T : FileUpload
    {
        public AttachementRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }

        public IEnumerable<T> GetAttachementsByRequest(int requestHeaderId)
        {
            var attachments = this.GetAll().Where(x => x.RequestHeaderId == requestHeaderId);

            return attachments.ToList();
        }

        public IEnumerable<FileTemp> GetFileAttachementsBySerials(IEnumerable<string> serials) {

            if (serials == null || serials.Count() == 0) {
                return null;
            }

            var context = DbContext as WorkflowDocContext;
            var fileUploads = context.Documents.Where(x => serials.Contains(x.Serial));

            return fileUploads.ToList();
        }

        public AttachementView GetAttachement(string attachmentId)
        {
            var attachments = SqlQuery<AttachementView>(
                "SELECT TOP(1) * FROM BPMDATA.ATTACHMENT WHERE AttachmentId = @attachmentId", 
                new { attachmentId = attachmentId });

            return attachments.Single();
        }

        public void DeleteAttachement(int requestHeaderId, string attachmentType)
        {
            var attachments = StoreProc(
                "EXEC [BPMDATA].[DELETE_ATTACHMENT] @attachmentType, @requestHeaderId",
                new { requestHeaderId = requestHeaderId, attachmentType = attachmentType });
        }

        public void DeleteAttachement(string serial)
        {
            throw new NotImplementedException();
        }

        public void InsertAttachement(string serial, string attachmentType, int requestHeaderId)
        {
            var attachments = StoreProc(
                "EXEC [BPMDATA].[INSERT_ATTACHMENT] @serial, @attachmentType, @requestHeaderId",
                new { serial = serial, attachmentType = attachmentType, requestHeaderId = requestHeaderId });
        }
    }
}
