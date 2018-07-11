/**
*@author : Phanny
*/

namespace Workflow.Business.AvirForm {
    public class AvirFormDataProcessing : AbstractFormDataProcessing, IAvirFormDataProcessing {
        public bool IsSaveRequestData { get; set; }
        public bool IsSaveBusinessData { get; set; }
    }
}
