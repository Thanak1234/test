using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.WM;
using Workflow.Domain.Entities.Queue;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class FingerprintService : IFingerprintService {

        protected IFingerprintRepository fingerprintRepo;

        public FingerprintService() {
            IDbFactory dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            fingerprintRepo = new FingerprintRepository(dbFactory);
        }

        public IEnumerable<FingerPrintMachine> GetFingerPrints() {
            return fingerprintRepo.GetAll();
        }
    }
}
