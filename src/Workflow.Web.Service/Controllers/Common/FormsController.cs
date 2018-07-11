using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.BatchData;
using Workflow.Web.Models;
using System.IO;
using System.Net.Http;
using Workflow.Extensions;
using Workflow.Service;
using Workflow.Service.Interfaces;
using System.Net.Http.Headers;
using System.Net;
using Workflow.Core;
using Workflow.ReportingService;
using Workflow.ReportingService.Report;
using Workflow.Web.Service.Properties;
using System.Web.Configuration;
using System.Configuration;
using NLog;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Infrastructure;
using Workflow.Service.FileUploading;
using Workflow.Service.Interfaces.ticketing;
using Workflow.Service.Ticketing;
using Workflow.Business;

namespace Workflow.Web.Service.Controllers.Common
{
    [RoutePrefix("api/forms")]
    public class FormsController: ApiController
    {
        private Lazy<IAttachmentService> _attachmentService;
        private ILookupService lookupService;
        protected readonly IWMRepository _worklistRepository;
        protected readonly IDocumentRepository _documentRepository;
        protected readonly IDocumentFileRepository _fileRepository;

        // list of form is using the deprecated attachment method
        private List<string> _deprecatedProcCode = new List<string>();


        public FormsController()
        {
            _attachmentService = new Lazy<IAttachmentService>(() => new AttachmentService(), true);
            _documentRepository = new DocumentRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            _fileRepository = new DocumentFileRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));
            _worklistRepository = new WMRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            
            _deprecatedProcCode.Add(PROCESSCODE.ITF);
            _deprecatedProcCode.Add(PROCESSCODE.BCJ);
            _deprecatedProcCode.Add(PROCESSCODE.AVJB);
            _deprecatedProcCode.Add(PROCESSCODE.RSVNFF);
            _deprecatedProcCode.Add(PROCESSCODE.RSVNCR);
            _deprecatedProcCode.Add(PROCESSCODE.ERF);
            _deprecatedProcCode.Add(PROCESSCODE.ATT);
            _deprecatedProcCode.Add(PROCESSCODE.PB);

            
        }

        [HttpGet]
        [Route("attachments")]
        public object GetAttachments() { return new { }; }

        /// <summary>
        /// /api/forms/lookups
        /// {parentId = -1} return all children by namespace
        /// {parentId = 0} return all parent by namespace
        /// {parentId > 0} return all children by namespace and parent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("lookups")]
        public IEnumerable<Lookup> GetLookups(string name = "[NAMESPACE]", int parentId = 0, int id = 0)
        {
            lookupService = new LookupService();

            if (id > 0)
            {
                return lookupService.LookupByName(name, parentId, id);
            }
            else
            {
                return lookupService.LookupByName(name, parentId);
            }
        }

        #region Attachments
        [HttpGet]
        [Route("download")]
        public HttpResponseMessage DownloadForms(int requestHeaderId, string documentType)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            var formConfigs = _worklistRepository.GetReqAppByCode(documentType);

            //string reportPath = Settings.Default["RPT_PATH_" + documentType].ToString();
            var segment = formConfigs.ReportPath.Split('/');

            IGenericFormRpt genericForm = new GenericFormRpt();
            
            byte[] buffer = genericForm.Export(
                new GenericFormParam {
                    RequestHeaderId = requestHeaderId,
                    Username = RequestContext.Principal.Identity.Name
                }, 
                formConfigs.ReportPath, ExportType.Pdf);
            var stream = new MemoryStream(buffer);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Concat(segment.Last(),"_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf")
            };

            return result;
        }
        
        /// <summary>
        /// attachments/download/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("attachments/download")]
        public HttpResponseMessage Download(string attachmentId, bool isTemp, string processCode)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            byte[] buffer = null;
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(processCode) && (_deprecatedProcCode.Where(p => p == processCode).Count() > 0))
            {
                // DEPRECATED
                if (isTemp)
                {
                    var deprecatedDocument = _attachmentService.Value.GetDocument(int.Parse(attachmentId));
                    buffer = deprecatedDocument.FileBinary;
                    fileName = deprecatedDocument.FileName;
                }
                else
                {
                    var deprecatedDocument = _attachmentService.Value.GetAttachement(attachmentId);
                    buffer = deprecatedDocument.FileBinary;
                    fileName = deprecatedDocument.FileName;
                }
            }
            else
            {
                int documentId = 0;
                if (isTemp)
                {
                    documentId = int.Parse(attachmentId);
                }
                else
                {
                    var ids = attachmentId.Split(new char[] { '_' });
                    documentId = int.Parse(ids[0]);
                }
                var document = _documentRepository.GetById(documentId);
                var attachment = _fileRepository.Get(p => p.Id == document.DocumentId);
                buffer = attachment.GetFileBinary();
                fileName = attachment.Name;
            }
            
            if (buffer != null)
            {
                var stream = new MemoryStream(buffer);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            }

            return result;
        }

        /**
       *@author: Phanny
       *@Date: 25-07-2016
       *@Desc : 
       *@Feature: User for ticket upload file. 
       *@TODO: to make it support most scenario
       */
        [HttpGet]
        [Route("downloadFile")]
        public HttpResponseMessage downLoadFile(string serial)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            ITicketService ticketService = new TicketService();

            var uploading = ticketService.getDownloadInfo(serial);

            var stream = new MemoryStream(uploading.Stream);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = uploading.fileName
            };

            return result;
        }
        /**
        *@author: Phanny
        *@Date: 25-07-2016
        *Desc : Make a simply upload service to serve common purpose,
        *Feature: Support single file upload a time
        */
        [HttpPost]
        [Route("attach")]
        public async Task<HttpResponseMessage> AttachFile(/*[FromBody]dynamic document*/)
        {
            string tempDir = FileSystemHelper.TempDir();
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(tempDir);

                string fileName = String.Empty;
                string ext = String.Empty ;
                long? size = 0;
                byte[] fileBinary = null;

                try
                {
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    foreach (MultipartFileData fileData in streamProvider.FileData)
                    {
                        if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                        }
                        fileName = fileData.Headers.ContentDisposition.FileName;
                        size = fileData.Headers.ContentDisposition.Size;

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        ext = Path.GetExtension(fileName);
                        fileBinary = File.ReadAllBytes(fileData.LocalFileName);
                        break;
                    }
                    IFileUploadService upService = new FileUploadService();

                    var upload = new DataObject.FileUploadInfo()
                    {
                        Identifier = "ticket",
                        fileName = fileName,
                        ext = ext,
                        Stream = fileBinary,
                    };

                    upService.uploadToDb(upload);
                    //streamProvider.DeleteLocalFiles();

                    var result = new
                    {
                        success = true,
                        fileName = fileName,
                        ext = ext,
                        serial = upload.serial,
                        size = size
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, new
                    {
                        success = false,
                        message = e.Message
                    });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }

        [HttpPost]
        [Route("attachments")]
        public async Task<HttpResponseMessage> Attach()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw this.ExceptionUnsupportedMediaType();
                }

                string temporaryDocument = FileSystemHelper.TempDir();
                var provider = new MultipartFormDataStreamProvider(temporaryDocument);

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                }
                catch (SmartException)
                {
                    provider.DeleteLocalFiles();
                    int maxRequestLength = 0;
                    HttpRuntimeSection section =
                    ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                    if (section != null)
                        maxRequestLength = section.MaxRequestLength;
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "File is too large. File must be less than " + (maxRequestLength / 1024).ToString() + " megabytes.");
                }

                var processCode = (provider.FormData.AllKeys.Contains("processCode")) ? provider.FormData.GetValues("processCode").FirstOrDefault() : string.Empty;
                
                if (!string.IsNullOrEmpty(processCode) && (_deprecatedProcCode.Where(p => p == processCode).Count() > 0))
                {
                    return DeprecatedActivityAttachment(provider);
                }
                else
                {
                    return ActivityAttachment(provider);
                }
            }
            catch (SmartException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.GetBaseException().Message);
            }
            
        }

        #region Activity Attachment Method
        private HttpResponseMessage ActivityAttachment(MultipartFormDataStreamProvider provider)
        {
            Document document = null;
            if (provider.FormData.AllKeys.Contains("serial"))
            {
                document = new Document()
                {
                    ObjectId = 0,
                    ObjectName = null,
                    CreatedDate = DateTime.Now,
                    CreatedBy = RequestContext.Principal.Identity.Name,
                    Status = "TEMPORARY",
                    Name = (provider.FormData.AllKeys.Contains("name")) ? provider.FormData.GetValues("name").FirstOrDefault() : string.Empty,
                    Description = (provider.FormData.AllKeys.Contains("description")) ? provider.FormData.GetValues("description").FirstOrDefault() : string.Empty
                };
            }
            else
            {
                provider.DeleteLocalFiles();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not found document was matched.");
            }

            foreach (var fileData in provider.FileData)
            {
                var contentFile = File.ReadAllBytes(fileData.LocalFileName);

                var docContent = new ContentFile()
                {
                    FileName = fileData.Headers.ContentDisposition.FileName.ToUnquoted(),
                    FileContent = Convert.ToBase64String(contentFile)
                };

                if (docContent.FileContent != null && docContent.FileContent.Length > 0)
                {
                    var documentFile = new DocumentFile()
                    {
                        Name = docContent.FileName,
                        Content = docContent.ToXml()
                    };

                    _fileRepository.Add(documentFile);
                    _fileRepository.Commit();

                    document.DocumentId = documentFile.Id;
                    document.FileName = documentFile.Name;
                    _documentRepository.Add(document);
                    _documentRepository.Commit();

                }
            }

            var result = new
            {
                success = true,
                data = new
                {
                    id = document.Id,
                    serial = document.DocumentId,
                    name = document.Name,
                    description = document.Description,
                    fileName = document.FileName,
                    isTemp = true,
                    activity = string.Empty,
                    uploadDate = document.CreatedDate,
                    readOnly = false
                }
            };
            provider.DeleteLocalFiles();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        private HttpResponseMessage DeprecatedActivityAttachment(MultipartFormDataStreamProvider provider)
        {
            FileTemp document = null;
            if (provider.FormData.AllKeys.Contains("serial"))
            {
                document = new FileTemp()
                {
                    Serial = Guid.NewGuid().ToString(),
                    Name = (provider.FormData.AllKeys.Contains("name")) ? provider.FormData.GetValues("name").FirstOrDefault() : string.Empty,
                    Comment = (provider.FormData.AllKeys.Contains("description")) ? provider.FormData.GetValues("description").FirstOrDefault() : string.Empty
                };
            }
            else
            {
                provider.DeleteLocalFiles();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not found document was matched.");
            }

            var fileTemporary = new FileTemp();
            foreach (var fileData in provider.FileData)
            {
                var documentAttachment = new FileTemp
                {
                    Serial = document.Serial,
                    Comment = document.Comment,
                    Name = document.Name,
                    FileName = fileData.Headers.ContentDisposition.FileName.ToUnquoted(),
                    MediaType = fileData.Headers.ContentType.MediaType.ToUnquoted(),
                    ContentDisposition = fileData.Headers.ContentDisposition.Parameters,
                    FileBinary = File.ReadAllBytes(fileData.LocalFileName)
                };

                if (documentAttachment.FileBinary != null && documentAttachment.FileBinary.Length > 0)
                {
                    fileTemporary = _attachmentService.Value.InsertAttachment(documentAttachment, true); 
                }
            }

            var result = new { success = true, data = fileTemporary };
            provider.DeleteLocalFiles();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        } 
        #endregion

        #endregion
    }
}