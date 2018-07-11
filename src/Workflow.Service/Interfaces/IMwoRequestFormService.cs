using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;

namespace Workflow.Service.Interfaces {
    public interface IMwoRequestFormService {
        IEnumerable<Mode> GetModes();
        IEnumerable<WorkType> GetWorkTypes();
        IEnumerable<RequestType> GetRequestTypes();
        IEnumerable<DepartmentChargable> GetDepartmentChargables();
        string GetReferenceNumber(string ccd);
    }
}
