/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;

namespace Workflow.Business.Interfaces
{
    public interface IFileAttachmentBC {
        IEnumerable<FileTemp> GetAttachementsBySerial(string serial);
    }
}
