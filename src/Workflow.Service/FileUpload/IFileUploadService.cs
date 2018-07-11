using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities.Attachment;

namespace Workflow.Service.FileUploading
{
    public interface IFileUploadService
    {
        void uploadToDb(FileUploadInfo fileInfo);
        void uploadToDb(List<FileUploadInfo> fileInfo);

        UploadFile getUploadFile(String serial);
        List<UploadFile> getUploadFiles(List<String> serials);

        void markAsSave(List<String> serials);
    }
    
}
