using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Workflow;
using Workflow.Core.Entities;

namespace Workflow.Core.Domain.Upload
{
    [DataContract]
    public class FileUpload : AbstractBaseEntity
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Comment { get; set; }

        [DataMember(Name = "serial")]
        public string Serial { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName {
            get
            {
                if (_fileName != null)
                {
                    return _fileName;
                }
                else
                {
                    var fileContent = this.FileContent.XmlDeserializeFromString<FileContent>();
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
        public string MediaType { get; set; }

        [IgnoreDataMember]
        public string FilePath { get; set; }

        [IgnoreDataMember]
        public string FileContent { get; set; }

        /// <summary>
        /// Raw custom parameters of the content-disposition multipart header
        /// </summary>
        [IgnoreDataMember]
        public ICollection<NameValueHeaderValue> ContentDisposition { get; set; }
        [IgnoreDataMember]
        public byte[] FileBinary {
            get
            {
                if (_fileBinary != null)
                {
                    return _fileBinary;
                }
                else
                {
                    var fileContent = this.FileContent.XmlDeserializeFromString<FileContent>();
                    this.FileName = fileContent.FileName;
                    return Convert.FromBase64String(fileContent.Content);
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