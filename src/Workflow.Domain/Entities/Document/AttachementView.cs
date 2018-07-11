using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core.Upload;

namespace Workflow.Domain.Entities
{
    [DataContract]
    public class AttachementView
    {
        public string AttachmentId { get; set; }
        public int RequestHeaderId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string FileContent { get; set; }
        public DateTime CteateDate { get; set; }
        public string Status{ get; set; }

        [IgnoreDataMember]
        public string FileName
        {
            get
            {
                if (_fileName != null)
                {
                    return _fileName;
                }
                else
                {
                    var fileContent = this.FileContent.XmlDeserializeFromString<ContentFile>();
                    this.FileName = fileContent.FileName;
                    return _fileName;
                }
            }
            set
            {
                _fileName = value;
            }
        }
        [IgnoreDataMember]
        public byte[] FileBinary
        {
            get
            {
                if (_fileBinary != null)
                {
                    return _fileBinary;
                }
                else
                {
                    var fileContent = this.FileContent.XmlDeserializeFromString<ContentFile>();
                    this.FileName = fileContent.FileName;
                    return Convert.FromBase64String(fileContent.FileContent);
                }
            }
            set
            {
                _fileBinary = value;
            }
        }

        public byte[] _fileBinary;
        public string _fileName;
    }
}
