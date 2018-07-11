using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.ICDRequestForm
{
    public class ICDFormDataProcessing : AbstractFormDataProcessing, IICDFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
