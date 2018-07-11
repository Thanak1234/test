using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.BcjRequestForm
{
    public class BcjFormDataProcessing : AbstractFormDataProcessing, IBcjFormDataProcessing
    {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
