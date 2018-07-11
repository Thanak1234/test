using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Scheduler;

namespace Workflow.DataAcess.Repositories {
    public class EmailContentRepository : RepositoryBase<EmailContent>, IEmailContentRepository {

        public EmailContentRepository(IDbFactory dbFactory): base(dbFactory) {

        }

    }
}
