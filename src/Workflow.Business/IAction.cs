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

    public interface IAction<T> 
        where T : IFormDataProcessing 
    {
        Boolean Match(IAction<T> action);
        string ActionName { get; }

        T FormDataProcessing { get; }
    }
}
