using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Attachment;
using Workflow.DataObject;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.Service.FileUploading
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IUploadFileRepository uploadRepo = null;

        public FileUploadService()
        {
            uploadRepo = new UploadFileRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc));
        }

        public List<UploadFile> getUploadFiles(List<string> serials)
        {
            List<UploadFile> files = new List<UploadFile>();
            serials.ForEach(t => {
                files.Add(getUploadFile(t));
            });

            return files;
        }

        public UploadFile getUploadFile(string serial)
        {
            return uploadRepo.Get(t => t.Serial.Equals(serial));
        }

        public void uploadToDb(List<FileUploadInfo> fileInfos)
        {

            fileInfos.ForEach(t =>
            {
                uploadToDb(t);
            });
        }

        public void uploadToDb(FileUploadInfo fileInfo)
        {
            var uploadFile = new UploadFile()
            {
                Serial = fileInfo.serial,
                DataContent = fileInfo.Stream
            };

            uploadRepo.Add(uploadFile);
        }

        public void markAsSave(List<string> serials)
        {
            List<UploadFile> files = getUploadFiles(serials);
            files.Each(t => {
                t.Status = "SAVE";
                uploadRepo.Update(t);
            });
        }
    }
}
