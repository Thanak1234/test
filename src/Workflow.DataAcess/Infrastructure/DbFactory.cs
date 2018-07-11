/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataAcess.Infrastructure
{
    public class WorkflowDbFactory : Disposable, IDbFactory
    {
        private DbContext _dbContext;

        public WorkflowDbFactory() { }

        public WorkflowDbFactory(DbContext dbContext) {
            _dbContext = dbContext;
        }

        public DbContext init()
        {
            return _dbContext ?? (_dbContext = new WorkflowContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
