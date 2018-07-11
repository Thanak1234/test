using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataContract.K2
{
    public enum ProcStatus
    {
        Available = 0,
        Open = 1,
        Allocated = 2,

        Sleep = 3,
        Completed = 4,
        Active = 5
    }
}
