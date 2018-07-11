/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ERFRequestForm
{
    public class ERFFormDataProcessing : AbstractFormDataProcessing, IERFFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
