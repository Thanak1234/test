/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Workflow.Core;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Attachement;
using Workflow.Service.Interfaces;
using Workflow.Domain.Entities.Core.Upload;
using Workflow.Business.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Core.IO;

namespace Workflow.Service
{
    public class AttachmentService : IAttachmentService
    {
        private IDbFactory _dbFactory;
        private readonly IAttachementRepository<FileTemp> _tempFileRepository;
        private readonly IUnitOfWork _work;
        private readonly IWebHelper _webHelper;
        private string _mediaPath;

        public AttachmentService()
        {
            _dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            _tempFileRepository = new AttachementRepository<FileTemp>(_dbFactory);
            _work = new UnitOfWork(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));
            _webHelper = new WebHelper();
        }
        
        public IEnumerable<T> AttachFileTemp<T>(int requestId) where T : FileUpload
        {
            var attachementRepo = new AttachementRepository<T>(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));

            var attachments = attachementRepo.GetAll().Where(t => t.Id == requestId);

            return attachments;
        }

        public IEnumerable<FileTemp> GetAll()
        {
            return _tempFileRepository.GetAll();
        }

        public FileTemp GetDocument(int id)
        {
            return _tempFileRepository.GetById(id);
        }

        public IEnumerable<FileTemp> GetAttachementsBySerial(string serial)
        {
            return _tempFileRepository.GetAll().Where(
                p => string.Equals(p.Serial, serial, StringComparison.OrdinalIgnoreCase)
            );
        }

        public void UpdateAttachment(FileTemp document)
        {
            _tempFileRepository.Update(document);
            _work.commit();
        }

        public void DeleteAttachement(int requestHeaderId, string attachmentType)
        {
            _tempFileRepository.DeleteAttachement(requestHeaderId, attachmentType);
            _work.commit();
        }

        public FileTemp InsertAttachment(FileTemp attachment, bool dbStore)
        {
            attachment.MediaType = attachment.MediaType.EmptyNull();
            attachment.MediaType = attachment.MediaType.Truncate(20);

            attachment.FileName = attachment.FileName.Truncate(100);

            //Document document = _documentRepository.Create();
            
            var contentFile = new ContentFile()
            {
                FileName = attachment.FileName,
                FileContent = Convert.ToBase64String(attachment.FileBinary)
            };
            attachment.FileContent = contentFile.ToXml();
            _tempFileRepository.Add(attachment);
            _work.commit();

            if (!dbStore)
            {
                SaveDocumentInFile(attachment.Id, attachment.FileBinary, attachment.MediaType);
            }
            
            return attachment;
        }
        
        public byte[] ValidateDocument(byte[] fileBinary)
        {
            return fileBinary;
        }

        #region Utilities
        
        protected virtual string GetDocumentLocalPath(string fileName)
        {
            var path = _mediaPath ?? (_mediaPath = _webHelper.MapPath("~/Document/"));
            var filePath = Path.Combine(path, fileName);
            return filePath;
        }
        
        protected virtual void SaveDocumentInFile(int documentId, byte[] documentBinary, string mimeType)
        {
			string filePath;
			SaveDocumentInFile(documentId, documentBinary, mimeType, out filePath);
        }

		private void SaveDocumentInFile(int documentId, byte[] documentBinary, string mimeType, out string filePath)
		{
			filePath = null;
			string lastPart = MimeTypes.MapMimeTypeToExtension(mimeType);
			string fileName = string.Format("{0}-0.{1}", documentId.ToString("0000000"), lastPart);
			filePath = GetDocumentLocalPath(fileName);
			File.WriteAllBytes(filePath, documentBinary);
		}
        
        protected virtual void DeleteDocumentOnFileSystem(FileTemp document)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            string lastPart = MimeTypes.MapMimeTypeToExtension(document.MediaType);
            string fileName = string.Format("{0}-0.{1}", document.Id.ToString("0000000"), lastPart);
            string filePath = GetDocumentLocalPath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public T GetAttachment<T>(int id) where T : FileAttachement
        {
            IAttachementRepository<T> _attachmentRepository = new AttachementRepository<T>(_dbFactory);
            return _attachmentRepository.GetById(id);
        }

        public AttachementView GetAttachement(string attachmentId)
        {
            return _tempFileRepository.GetAttachement(attachmentId);
        }

        #endregion
    }
}
