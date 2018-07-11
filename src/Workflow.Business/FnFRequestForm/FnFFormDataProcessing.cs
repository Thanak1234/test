/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.FnFRequestForm
{
    public class FnFFormDataProcessing : AbstractFormDataProcessing, IFnFFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
