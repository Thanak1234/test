using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.ReportingService.Report;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Reports;
using Workflow.Domain.Entities;

namespace Workflow.ReportingService.Report
{
    public class ProcInstRpt : Report<ProcInst, ProcInstParam>, IProcInstRpt
    {
        private IDataReport<ProcInst> _procInstRepo;

        public ProcInstRpt()
        {
            IDbFactory factory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            
            _procInstRepo = new DataReport<ProcInst>(factory);
        }                
    }
}
