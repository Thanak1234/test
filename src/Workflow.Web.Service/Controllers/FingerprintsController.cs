using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataContract.Fingerprint;
using Workflow.RabbitMQ;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/fingerprints")]
    public class FingerprintsController : ApiController {

        private IFingerprintService fingerprintService;

        private const string SERVICE_NAME = "iClockQueueService";
        private string SERVER_IP;
        private const string USER_NAME = "nagaworld\\k2admin";
        private const string PASS = "**aa12345";

        private static RabbitMQClient _client = null;

        public FingerprintsController() {
            fingerprintService = new FingerprintService();            
            SERVER_IP = ConfigurationManager.AppSettings["iClockHost"] ?? "10.62.0.23";
            if(_client == null)
                _client = new RabbitMQClient("Fingerprint Monitor Queue");
        }

        [HttpGet]
        public HttpResponseMessage Get() {
            var records = fingerprintService.GetFingerPrints();
            return Request.CreateResponse(HttpStatusCode.OK, new { totalCount = records.Count(), data = records });
        }
        
        [HttpPost]
        public HttpResponseMessage Post(CommandObject command) {
            _client.Publish(command.Command, command.Data.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, new { ip = command.Data });
        }

        [HttpGet]
        [Route("restart")]
        public bool Restart()
        {
            ConnectionOptions connectoptions = new ConnectionOptions();
            connectoptions.Username = USER_NAME;
            connectoptions.Password = PASS;
            ManagementScope scope = new ManagementScope(@"\\" + SERVER_IP + @"\root\cimv2");
            scope.Options = connectoptions;
            SelectQuery query = new SelectQuery("select * from Win32_Service where name = '" + SERVICE_NAME + "'");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject service in collection)
                {
                    if (service["Started"].Equals(false))
                    {
                        service.InvokeMethod("StartService", null);
                        return true;
                    }
                    else
                    {
                        service.InvokeMethod("StopService", null);
                        return true;
                    }
                }
            }
            return false;
        }

        [HttpGet]
        [Route("IsServiceStarted")]
        public bool IsServiceStarted()
        {
            ConnectionOptions connectoptions = new ConnectionOptions();
            connectoptions.Username = USER_NAME;
            connectoptions.Password = PASS;
            ManagementScope scope = new ManagementScope(@"\\" + SERVER_IP + @"\root\cimv2");
            scope.Options = connectoptions;
            SelectQuery query = new SelectQuery("select * from Win32_Service where name = '" + SERVICE_NAME + "'");

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject service in collection)
                {
                    if (service["Started"].Equals(false))
                    {
                        return false;
                    }

                }
            }
            return true;
        }
    }
}
