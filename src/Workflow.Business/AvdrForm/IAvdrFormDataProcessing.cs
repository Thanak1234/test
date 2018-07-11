/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AvdrForm {
    public interface IAvdrFormDataProcessing : IFormDataProcessing
    {
        bool IsSaveBusinessData { get; set; }
    }
}
