using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Reports;

namespace Workflow.ReportingService.Report
{
    public class Report<TReport, TParam> : IReport<TReport, TParam>
        where TReport : class
        where TParam : class
    {
        private string rsUri = ConfigurationManager.AppSettings["rsUri"];
        private IDataReport<TReport> _reportRepo;
        private IDataReport<ReportServer> _reportDefRepo;

        public Report() {
            IDbFactory factory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);

            _reportDefRepo = new DataReport<ReportServer>(factory);
            _reportRepo = new DataReport<TReport>(factory);
        }

        public IQueryable<TReport> GetList(object parameter, string reportPath)
        {
            IEnumerable<TReport> list = new List<TReport>();
            var report = GetReportDesign(reportPath);
            if (report != null) {
                if (report.CommandType == "StoredProcedure")
                {
                    list = _reportRepo.StoreProc(@"EXEC " + report.CommandText, parameter);
                }
                else
                {
                    list = _reportRepo.SqlQuery(report.CommandText, parameter);
                }   
            }
            return list.AsQueryable();
        }

        public byte[] Export(object param, string reportPath, ExportType type)
        {
            string extension = Enum.GetName(typeof(ExportType), type).ToLower();
            return Export(param, reportPath, extension);
        }

        public byte[] Export(object param, string reportPath, string type)
        {
            ReportViewer report = new ReportViewer();
            
            //Set Processing Mode
            report.ProcessingMode = ProcessingMode.Remote;
            report.ServerReport.ReportPath = reportPath;
            report.ServerReport.ReportServerUrl = new Uri(rsUri);
            List<ReportParameter> @params = new List<ReportParameter>();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            pInfo = report.ServerReport.GetParameters();

            if (param != null)
            {
                Type parameterType = param.GetType();
                var properties = parameterType.GetProperties();
                foreach (var property in properties)
                {
                    var attribute = property.GetAttribute<DataMemberAttribute>(true);

                    string name = attribute.Name;
                    object value = property.GetValue(param, null);

                    ReportParameterInfo rpInfo = pInfo[name];

                    if (rpInfo == null) continue;

                    if (value != null && value.ToString().ToUpper() != "NULL")
                    {
                        @params.Add(new ReportParameter(name, value.ToString(), true));
                    }
                    else {               
                        ReportParameter rparam = new ReportParameter(name);
                        rparam.Values.Add(null);
                        @params.Add(rparam);
                    }
                }
            }
            
            report.ServerReport.SetParameters(@params);

            // Process and render the report
            report.ServerReport.Refresh();

            string format = "";
            string extension = "";
            string deviceinfo = "";
            string mimeType = "";
            string encoding = "";
            string[] streams = null;
            Warning[] warnings = null;
            
            if (type.ToLower() == "xls") {
                format = "EXCEL";
                extension = "xls";
            } else if (type.ToLower() == "pdf") {
                format = "PDF";
                extension = "pdf";
            }

            return report.ServerReport.Render(format, deviceinfo, out mimeType, out encoding, out extension, out streams, out warnings);
        }

        protected ReportServer GetReportDesign(string reportPath)
        {
            return _reportDefRepo.Single(
                "SELECT TOP(1) * FROM [REPORT].[SSRS_DATASET] WHERE [Path] = @Path ",
                new { Path = reportPath });
        }
    }
}
