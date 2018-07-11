using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.Core;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class ActivityService: IActivityService {

        private IActivityRepository _repository;

        public ActivityService() {
            var context = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _repository = new ActivityRepository(context);
        }

        public string GetViewConfiguration(string req, string activity) {
            return _repository.GetSubmissionConfig(req, activity);
        }
    }
}
