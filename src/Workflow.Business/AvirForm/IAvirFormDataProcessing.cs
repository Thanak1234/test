/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AvirForm {
    public interface IAvirFormDataProcessing : IFormDataProcessing
    {
        bool IsSaveBusinessData { get; set; }
    }
}
