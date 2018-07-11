using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Service.Interfaces {

    public interface IActivityService {
        string GetViewConfiguration(string req, string activity);
    }

}
