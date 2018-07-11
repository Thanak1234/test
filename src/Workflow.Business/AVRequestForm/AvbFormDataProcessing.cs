/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.AVRequestForm
{
    public class AvbFormDataProcessing : AbstractFormDataProcessing, IAvbFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
