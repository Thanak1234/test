using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business
{
    public class DefaultAction<T> : AbstractAction<T>
        where T: IFormDataProcessing
    {
        private T _formDataProcessing;

        public DefaultAction(string actionName) : base(actionName) { }

        public DefaultAction(string actionName, T formDataProcessing):base(actionName)
        {
            _formDataProcessing = formDataProcessing;
        }

        public override T FormDataProcessing
        {
            get
            {
                return _formDataProcessing;
            }
        }
    }
}
