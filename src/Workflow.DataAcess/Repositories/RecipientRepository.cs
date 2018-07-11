using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Scheduler;

namespace Workflow.DataAcess.Repositories {
    public class RecipientRepository : RepositoryBase<Recipient>, IRecipientRepository {

        public RecipientRepository(IDbFactory dbFactory): base(dbFactory) {

        }

    }
}
