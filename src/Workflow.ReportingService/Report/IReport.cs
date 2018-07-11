using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.ReportingService.Report;

namespace Workflow.ReportingService
{
    public interface IReport<TReport, TParam> where TReport : class where TParam : class
    {
        byte[] Export(object param, string reportPath, ExportType type);
        byte[] Export(object param, string reportPath, string type);
        IQueryable<TReport> GetList(object parameter, string reportPath = "");
    }
}
