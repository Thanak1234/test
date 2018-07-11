using Quartz;
using System;
using System.Configuration;
using System.Management;
using System.Threading.Tasks;

namespace Workflow.Scheduler.Jobs
{

    public class RestartiClockServiceJob : ScheduleJobBase, IScheduleJob {

        private const string SERVICE_NAME = "iClockQueueService";
        private string SERVER_IP;
        private const string USER_NAME = "nagaworld\\k2admin";
        private const string PASS = "**aa12345";

        protected override string KeyValue {
            get {
                return "RESTART_ICLOCKSERVICE_JOB";
            }
        }

        protected override object Model {
            get {
                return null;
            }
        }

        protected override Type Type {
            get {
                return GetType();
            }
        }

        protected override void ExecuteJob(IJobExecutionContext context) {
            SERVER_IP = ConfigurationManager.AppSettings["iClockHost"] ?? "10.62.0.23";
            Restart();
        }

        private async void Restart()
        {
            await Task.Factory.StartNew(() =>
            {
                if(IsServiceStarted())
                {
                    Stop();
                    Task.Delay(7000).Wait();
                    Start();
                } else
                {
                    Start();
                }
            });
        }

        public void Start()
        {
            using (ManagementObjectSearcher searcher = GetObjectSearcher())
            {
                ManagementObjectCollection services = searcher.Get();
                foreach (ManagementObject service in services)
                {
                    service.InvokeMethod("StartService", null);
                    return;
                }
            }
        }

        public void Stop()
        {
            using (ManagementObjectSearcher searcher = GetObjectSearcher())
            {
                ManagementObjectCollection services = searcher.Get();
                foreach (ManagementObject service in services)
                {
                    service.InvokeMethod("StopService", null);
                    return;
                }
            }
        }

        public bool IsServiceStarted()
        {
            using (ManagementObjectSearcher searcher = GetObjectSearcher())
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

        private ManagementObjectSearcher GetObjectSearcher()
        {
            ConnectionOptions connectoptions = new ConnectionOptions();
            connectoptions.Username = USER_NAME;
            connectoptions.Password = PASS;
            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", SERVER_IP));
            scope.Options = connectoptions;
            SelectQuery query = new SelectQuery(string.Format("SELECT * FROM Win32_Service WHERE name = '{0}'", SERVICE_NAME));
            return new ManagementObjectSearcher(scope, query);
        }
    }
}
