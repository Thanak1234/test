using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Queue;

namespace Workflow.Service.Interfaces {
    public interface IFingerprintService {
        IEnumerable<FingerPrintMachine> GetFingerPrints();
    }
}
