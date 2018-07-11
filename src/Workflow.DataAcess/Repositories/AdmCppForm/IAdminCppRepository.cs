using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.ADMCPPForm;

namespace Workflow.DataAcess.Repositories.AdmCppForm {
    public interface IAdminCppRepository: IRepository<ADMCPP> {
        ADMCPP GetByRequestHeaderId(int id);
        bool ExistSerialNo(string serialNo, int requestHeaderId);
    }
}
