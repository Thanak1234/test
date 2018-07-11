using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataObject;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.Service.Interfaces {
    public interface IN2MwoRequestFormService {
        IEnumerable<N2Mode> GetModes();
        IEnumerable<N2WorkType> GetWorkTypes();
        IEnumerable<N2RequestType> GetRequestTypes();
        IEnumerable<N2DepartmentChargable> GetDepartmentChargables();
        string GetReferenceNumber(string ccd);
    }
}
