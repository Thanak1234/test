using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.BCJ;
using Workflow.Domain.Entities.BCJ;
using Workflow.Service.Interfaces;

namespace Workflow.Service {

    public class BcjRequestContentService: IBcjRequestContentService {

        private IProjectDetailRepository _projectDetailRepository = null;

        public BcjRequestContentService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _projectDetailRepository = new ProjectDetailRepository(workflow);
        }

        public IEnumerable<CapexCategory> GetCapexCategories() {
            return _projectDetailRepository.GetCapexCategories();
        }
    }

}
