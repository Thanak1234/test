/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business
{
    public class AbstractFormDataProcessing
    {

        private bool _trigerWorkFlow = true;
        private bool _sendEmail = false;

        public bool IsSaveAttachments { get; set; }

        public bool IsAddNewRequestHeader { get; set; }

        public bool IsEditPriority { get; set; }

        public bool IsEditRequestor { get; set; }

        public bool IsSaveActivityHistory { get; set; }

        public bool IsUpdateLastActivity { get; set; }

        public bool TriggerWorkflow { get { return _trigerWorkFlow; } set { _trigerWorkFlow = value; } }

        public bool SendEmail { get { return _sendEmail; }  set { _sendEmail = value; } }

        public bool IsRequiredComment { get; set; } = false;
        public bool IsRequiredAttachment { get; set; } = false;
    }
}
