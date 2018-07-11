/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.EOMRequestForm {
    public class EOMFormDataProcessing : AbstractFormDataProcessing, IEOMFormDataProcessing {
        public bool IsSaveRequestData
        {
            get; set;
        }
    }
}
