using System;
using System.IO;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using Workflow.ReportingService.Report;
using Workflow.Core;
using Workflow.DataAcess.Repositories;

namespace Workflow.Web.Service
{
    /// <summary>
    /// Summary description for DownloadService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Download : WebService
    {
        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod]
        public SmartObjectPdf GetSmartObjectPdf(int requestHeaderId)
        {
            return GetSmartObjectPdfCurrentUser(requestHeaderId, null);
        }

        [WebMethod]
        public SmartObjectPdf GetSmartObjectPdfCurrentUser(int requestHeaderId, string currentUser)
        {
            string folio = string.Empty;
            try
            {
                var repository = new Repository();
                var genericForm = new GenericFormRpt();

                var query = repository.DynamicQuery(@"
                        SELECT A.REPORT_PATH, A.REQUEST_CODE, H.TITLE FROM [BPMDATA].[REQUEST_APPLICATION] A
                        INNER JOIN BPMDATA.REQUEST_HEADER H ON H.REQUEST_CODE = A.REQUEST_CODE
                        WHERE H.ID = " + requestHeaderId);
                
                var application = query.GetDynamicObject();

                string fileContent = string.Empty;
                folio = application.TITLE;
                string fileName = string.Concat(folio, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
                
                byte[] buffer = genericForm.Export(
                    new GenericFormParam
                    {
                        RequestHeaderId = requestHeaderId,
                        Username = currentUser
                    },
                    application.REPORT_PATH, ExportType.Pdf);
                
                using (Stream fs = new MemoryStream(buffer))
                {
                    fileContent = Convert.ToBase64String(DataStream.ReadToEnd(fs));
                    
                }


                return new SmartObjectPdf()
                {
                    FileName = fileName,
                    PdfContent = string.Format("<file><name>{0}</name><content>{1}</content></file>", fileName, fileContent)
                };
            }
            catch (Exception ex)
            {
                logger.Info("Folio: " + folio + ", cannot export pdf because - " + ex.Message);
                return new SmartObjectPdf()
                {
                    FileName = "FORM",
                    PdfContent = "<file><name>{0}</name><content>{1}</content></file>"
                };
            }
        }
    }
    
    [XmlRoot("SmartObjectPdf")]
    public class SmartObjectPdf
    {
        [XmlElement("Filename")]
        public string FileName { get; set; }

        [XmlElement("PdfContent")]
        public string PdfContent { get; set; }
    }
    
}
