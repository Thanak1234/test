using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models
{
    public class FileUploadViewModel
    {
        public int id { get; set; }
        public int requestHeaderId { get; set; }
        public string serial { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
        public string activity { get; set; }
        public DateTime? uploadDate { get; set; }
        public bool readOnly { get; set; }
    }
}
