

using Workflow.MSExchange;
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
    public interface IActivity<T> where T : IFormDataProcessing
    {
        IAction<T> CurrAction { get; set; }
        string ActivityName { get; }
        void SetCurrActionName(string actionName);
        bool Matched(string activityName);
        IEmailData Email { get; set; }

    }
}
