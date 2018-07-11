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
    public class GenericFormRpt : Report<GenericForm, GenericFormParam>, IGenericFormRpt
    {
        private IDataReport<GenericForm> _procInstRepo;

        public GenericFormRpt()
        {
            IDbFactory factory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            
            _procInstRepo = new DataReport<GenericForm>(factory);
        }
    }
}
