using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.DataObject.Errors {
    public class ModalStateErrorMessage {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Detail { get; set; }
    }
}
