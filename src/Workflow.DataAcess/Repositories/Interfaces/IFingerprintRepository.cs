using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;
using Workflow.Domain.Entities.Queue;

namespace Workflow.DataAcess.Repositories.Interfaces {
    public interface IFingerprintRepository: IRepository<FingerPrintMachine> {
    }
}
