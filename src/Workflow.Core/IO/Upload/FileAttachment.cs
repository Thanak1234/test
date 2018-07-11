using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Web;

namespace Workflow.Utilities.IO.Upload
{
    public class FileAttachment
    {
        /// <summary>
		/// Unquoted name attribute of content-disposition multipart header
		/// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Unquoted filename attribute of content-disposition multipart header
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Media (mime) type of content-type multipart header
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Indicates whether the uploaded file already exist and therefore has been skipped
        /// </summary>
        public bool Exists { get; set; }

        /// <summary>
        /// Indicates whether the uploaded file has been inserted
        /// </summary>
        public bool Inserted { get; set; }

        /// <summary>
        /// Url of the file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Raw custom parameters of the content-disposition multipart header
        /// </summary>
        public ICollection<NameValueHeaderValue> ContentDisposition { get; set; }

        /// <summary>
        /// The file byte[]. Can be null.
        /// </summary>
        public byte[] FileBinary { get; set; }
    }
}