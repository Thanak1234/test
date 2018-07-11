using log4net;
using System;
using System.ServiceProcess;

namespace Workflow.FingerPrint
{
    public partial class iClockService : ServiceBase {
        protected ILog logger = LogManager.GetLogger(typeof(iClockService));
        public iClockService() {
            InitializeComponent();
        }
        protected RabbitMQService service;

        protected override void OnStart(string[] args) {
            try {
                service = new RabbitMQService();
                service.Start();
            } catch (Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }

        protected override void OnStop() {
            try {
                service.Stop();
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }
    }
}
