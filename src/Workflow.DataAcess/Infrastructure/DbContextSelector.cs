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
    public class DbContextSelector
    {
        public enum DbName { Workflow, WorkflowDoc, K2 }

        private static DbContextSelector dbContextSelector;

        public static DbContextSelector getInstance()
        {
            return dbContextSelector ?? (dbContextSelector = new DbContextSelector());
        }

        public IDbFactory getDbFactory(DbName dbName)
        {

            IDbFactory dbFactory=null;

            switch (dbName)
            {
                case DbName.Workflow:
                    dbFactory = new WorkflowDbFactory();
                    break;
                case DbName.WorkflowDoc:
                    dbFactory = new WorkflowDbFactory(new WorkflowDocContext());
                    break;
                case DbName.K2:
                    dbFactory = new WorkflowDbFactory();
                    break;
            }

            return dbFactory;
        }
        
    }
}
