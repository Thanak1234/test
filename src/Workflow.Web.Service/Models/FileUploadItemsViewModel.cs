using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Web.Models
{
    public class FileUploadItemsViewModel
    {
        public IEnumerable<FileUploadViewModel> allItems { get; set; }
        public ICollection<FileUploadViewModel> newItems { get; set; }
        public ICollection<FileUploadViewModel> removedItems { get; set; }
        public ICollection<FileUploadViewModel> updatedItems { get; set; }
    }
}
