using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Attachment
{
    public class UploadFile
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public  Byte[] DataContent { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "TMP";
    }
}
