using Workflow.Domain.Entities.Attachement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Core.Upload;
using Workflow.Domain.Entities;

namespace Workflow.Service.Interfaces
{
    /// <summary>
    /// Attachment service interface
    /// </summary>
    public partial interface IAttachmentService
    {  
        FileTemp InsertAttachment(FileTemp attachment ,bool dbStore);
        IEnumerable<FileTemp> GetAll();
        FileTemp GetDocument(int id);
        IEnumerable<FileTemp> GetAttachementsBySerial(string serial);
        void UpdateAttachment(FileTemp document);
        void DeleteAttachement(int requestHeaderId, string attachmentType);
        IEnumerable<T> AttachFileTemp<T>(int requestId) where T : FileUpload;
        T GetAttachment<T>(int id) where T : FileAttachement;
        AttachementView GetAttachement(string attachmentId);
    }
}
