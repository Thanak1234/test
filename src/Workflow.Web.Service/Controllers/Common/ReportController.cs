using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using Workflow.ReportingService;
using Workflow.ReportingService.Report;
using Workflow.DataObject;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers.Common
{
    public abstract class ReportController<TReport, TParam> : ApiController where TReport : class where TParam : class
    {
        protected IEmployeeService _employeeService = new EmployeeService();
        protected Dictionary<string, string> ReportPaths = new Dictionary<string, string>();
        protected IReport<TReport, TParam> processInst;
        protected string ReportPath;
        private readonly string _baseReportPath = "/REPORTS/PROCESS_INSTANCES/PROC_INST_";

        public ReportController()
        {
            processInst = new Report<TReport, TParam>();
            ReportPath = GetReportPath();
        }

        public EmployeeDto Originator(int id = 0)
        {
            if (id > 0)
            {
                return _employeeService.GetEmployee(id);
            }
            else {
                return _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
            }
        }

        public int Skip {
            get {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString["start"]); 
            }
        }
        public int Take
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString["limit"]);
            }
        }
        public int Page
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            }
        }

        public string Extension
        { //extension: pdf, xlsx
            get {
                return HttpContext.Current.Request.QueryString["exportType"];
            }
        }
        
        public HttpResponseMessage ExportFile(byte[] buffer, string fileName)
        {
            var stream = new MemoryStream(buffer);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Concat(fileName, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".", Extension)
            };

            return result;
        }

        #region Process Instance Report

        private string FormName {
            get {
                return typeof(TReport).Name.Replace("ProcInst", string.Empty);
            }
        }
        private string GetReportPath()
        {
            if (ProcessCode != null)
            {
                return _baseReportPath + ProcessCode.Replace("_REQ", string.Empty);
            } else {
                if (!string.IsNullOrEmpty(FormName))
                {
                    return _baseReportPath + FormName;
                }
                return _baseReportPath + "CLASSIC";
            }
        }
        
        public string ProcessCode
        {
            get
            {
                string processCode = HttpContext.Current.Request.QueryString["AppName"];
                return string.IsNullOrEmpty(processCode) || processCode == "null" ? null : processCode;
            }
        }

        #endregion

        #region Parameter Parser
        public T ParseQueryString<T>() where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var valueAsString = string.Empty;
                var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), true);

                bool hasDma = false;
                foreach (DataMemberAttribute dma in attributes)
                {
                    hasDma = true;
                    valueAsString = HttpContext.Current.Request.QueryString[dma.Name];
                }

                if (!hasDma)
                {
                    valueAsString = HttpContext.Current.Request.QueryString[property.Name];
                }

                var value = Parse(property.PropertyType, valueAsString);

                if (value == null)
                    continue;

                property.SetValue(obj, value, null);
            }
            return obj;
        }

        public T ParseProcInstParam<T>() where T : ProcInstParam
        {
            T obj = (T)Activator.CreateInstance(typeof(T));

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var valueAsString = string.Empty;
                var attributes = property.GetCustomAttributes(typeof(DataMemberAttribute), true);

                bool hasDma = false;
                foreach (DataMemberAttribute dma in attributes)
                {
                    hasDma = true;
                    valueAsString = HttpContext.Current.Request.QueryString[dma.Name];
                }

                if (!hasDma)
                {
                    valueAsString = HttpContext.Current.Request.QueryString[property.Name];
                }

                var value = Parse(property.PropertyType, valueAsString);

                if (value == null)
                    continue;

                property.SetValue(obj, value, null);
            }

            var currentUser = this.Originator();
            if (currentUser != null)
            {
                obj.CurrentUserId = currentUser.id;
            }
            if (!string.IsNullOrEmpty(FormName))
            {
                obj.ProcessName = string.Concat(FormName, "_REQ");
            }
            return obj;
        }

        private object Parse(Type dataType, string ValueToConvert)
        {
            if (dataType.Name == "Int32" && ValueToConvert.IsEmpty())
            {
                ValueToConvert = "0";
            }
            else if (dataType.Name == "String" && ValueToConvert.IsEmpty())
            {
                return null;
            }
            try
            {
                TypeConverter obj = TypeDescriptor.GetConverter(dataType);
                return obj.ConvertFromString(null, CultureInfo.InvariantCulture, ValueToConvert);
            }
            catch
            {
                return null;
            }
        } 
        #endregion
    }
}
