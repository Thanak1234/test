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
using Workflow.Domain.Entities;
using Workflow.Service.Interfaces;
using Workflow.Domain.Entities.Core.Upload;
using Workflow.Core.IO;
using Workflow.Domain.Entities.Attachement;

namespace Workflow.Service
{
    public class DocumentService : IDocumentService
    {
        private IDbFactory _dbFactory;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork _work;
        private readonly IWebHelper _webHelper;
        private string _mediaPath;

        public DocumentService()
        {
            _dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            _documentRepository = new DocumentRepository(_dbFactory);
            _work = new UnitOfWork(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));
            _webHelper = new WebHelper();
        }
        
        public void DeleteDocument()
        {
            
        }

        public FileTemp InsertDocument(FileTemp Document, bool dbStore)
        {
            Document.MediaType = Document.MediaType.EmptyNull();
            Document.MediaType = Document.MediaType.Truncate(20);

            Document.FileName = Document.FileName.Truncate(100);

            //Document document = _documentRepository.Create();
            
            var docContent = new FileContent()
            {
                FileName = Document.FileName,
                Content = Convert.ToBase64String(Document.FileBinary)
            };
            Document.FileContent = docContent.ToXml();
            //_documentRepository.Add(Document);
            _work.commit();

            if (!dbStore)
            {
                SaveDocumentInFile(Document.Id, Document.FileBinary, Document.MediaType);
            }
            
            return Document;
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

        //public T GetDocument<T>(int id) where T : FileDocument
        //{
        //    IDocumentRepository<T> _DocumentRepository = new DocumentRepository<T>(_dbFactory);
        //    return _DocumentRepository.GetById(id);
        //}

        //public DocumentView GetDocument(string DocumentId)
        //{
        //    return _documentRepository.GetDocument(DocumentId);
        //}

        #endregion
    }
}
