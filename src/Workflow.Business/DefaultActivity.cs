using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business
{
    public class DefaultActivity<T> : AbstractActivity<T>, IActivity<T>
    where   T : IFormDataProcessing
    {

        public DefaultActivity(string activityName) : base(activityName) { }

        public T FormDataProcessing
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
