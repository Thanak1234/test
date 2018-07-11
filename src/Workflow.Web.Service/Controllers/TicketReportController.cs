/**
*@author : Phanny
*/
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Workflow.DataObject.Ticket;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Service.Interfaces.ticketing;
using Workflow.Service.Ticketing;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/ticketrpt")]
    public class TicketReportController : ApiController
    {
        private readonly ITicketLookupService ticketLookupService = null;
        private readonly ITicketService ticketService = null;
        private readonly IEmployeeService _employeeService;

        public TicketReportController() {
            ticketLookupService = new TicketLookupService();
            ticketService = new TicketService();
            _employeeService = new EmployeeService();
        }

        [HttpGet]
        [Route("lookup")]
        public HttpResponseMessage LookupByKey(string key) {
            IEnumerable<object> records = null;
            switch (key) {
                case "TYPE":
                    records = ticketLookupService.listTicketType();
                    break;
                case "SOURCE":
                    records = ticketLookupService.listTicketSource();
                    break;
                case "GROUP":
                    records = ticketLookupService.listTicketTeam();
                    break;
                case "CATEGORY":
                    records = ticketLookupService.listTicketCategory();
                    break;
                case "STATUS":
                    records = ticketLookupService.listTicketStatus(true);
                    break;
                case "PRIORITY":
                    records = ticketLookupService.listTicketPriority();
                    break;
                case "SLA":
                    records = ticketLookupService.listTicketSLA();
                    break;
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { records = records });
        }

        [HttpGet]
        [Route("agents")]
        public HttpResponseMessage GetAgents(int teamId) {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.GetAgentEmpIdByTeam(teamId));
        }

        [HttpGet]
        [Route("agentsList")]
        public HttpResponseMessage GetAgentsList([FromUri] IEnumerable<int> teamId)        {
            return Request.CreateResponse(HttpStatusCode.OK, ticketLookupService.GetAgentEmpIdByTeam(teamId));
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search([FromUri]TicketingSearchParamsDto @params) {
            var emp = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
            @params.CurrLoginEmpId = emp.id;
            var queryResult = ticketService.GetReportResult(@params);
            return Request.CreateResponse(HttpStatusCode.OK, queryResult);
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export([FromUri]TicketingExportParamsDto @params) {
            var emp = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
            @params.CurrLoginEmpId = emp.id;
            byte[] buffer = Export(@params, "/REPORTS/TICKET_REPORT", @params.ExportType);
            return ExportFile(buffer, "TICKET_REPORT", @params.ExportType);
        }

        public HttpResponseMessage ExportFile(byte[] buffer, string fileName, string extension) {
            var stream = new MemoryStream(buffer);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") {
                FileName = string.Concat(fileName, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".", extension)
            };

            return result;
        }

        public object GetPropValue(object source, string propertyName) {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            if (property != null) {
                return property.GetValue(source);
            }
            return null;
        }

        public byte[] Export(object param, string reportPath, string type) {

            string rsUri = ConfigurationManager.AppSettings["rsUri"];
            ReportViewer report = new ReportViewer();
            report.ProcessingMode = ProcessingMode.Remote;
            report.ServerReport.ReportPath = reportPath;
            report.ServerReport.ReportServerUrl = new Uri(rsUri);
            List<ReportParameter> @params = new List<ReportParameter>();
            ReportParameterInfoCollection pInfo = default(ReportParameterInfoCollection);
            pInfo = report.ServerReport.GetParameters();

            if (param != null) {
                Type parameterType = param.GetType();
                var properties = parameterType.GetProperties();
                foreach (var property in properties) {
                    string name = property.Name;
                    object value = GetPropValue(param, name);
                    
                    ReportParameterInfo rpInfo = pInfo[name];

                    if (rpInfo == null) continue;

                    if (value != null && value.ToString().ToUpper() != "NULL" ){                        
                        @params.Add(new ReportParameter(name, value.ToString(), true));                        
                    } else {
                        ReportParameter rparam = new ReportParameter(name);
                        rparam.Values.Add(null);
                        @params.Add(rparam);
                    }
                }
            }

            report.ServerReport.SetParameters(@params);
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

        [HttpGet]
        [Route("lookupEx")]
        public HttpResponseMessage Lookup([FromUri] TicketingLookupParamsDto param)
        {
            IEnumerable<object> records = null;
            switch (param.key)
            {                
                case "STATUS":
                    records = ticketLookupService.listTicketStatus(param);
                    break;                
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { records = records });
        }

    }

}
