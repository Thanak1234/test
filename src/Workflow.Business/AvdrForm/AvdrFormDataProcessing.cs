

using System;
/**
*@author : Phanny
*/
namespace Workflow.Business.AvdrForm {
    public class AvdrFormDataProcessing : AbstractFormDataProcessing, IAvdrFormDataProcessing {
        public bool IsSaveBusinessData { get; set; }
        public bool IsSaveRequestData { get; set; }

    }
}
