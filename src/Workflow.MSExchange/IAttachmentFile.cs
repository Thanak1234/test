using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange
{
    public interface IAttachmentFile
    {
        string FileName { get; set; }
        byte[] Stream { get; set; }
    }
}
