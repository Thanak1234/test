using log4net;
using System;
using System.Threading;
using System.Windows.Forms;
using Workflow.DataContract.Fingerprint;
using Workflow.FingerPrint;
using Workflow.RabbitMQ;

namespace Workflow.FingerPrint
{
    public class RabbitMQService
    {
        protected IFingerPrintManager fingerPrinterManager = null;
        protected ILog logger = LogManager.GetLogger(typeof(RabbitMQService));
        private Thread fingerPrintThread = null;
        protected RabbitMQClient client = null;

        public RabbitMQService()
        {

        }

        public void Start()
        {
            fingerPrintThread = new Thread(new ThreadStart(FingerPrintThreadStart));
            fingerPrintThread.SetApartmentState(ApartmentState.STA);
            fingerPrintThread.Start();
            InitRabbitClient();
        }

        protected void InitRabbitClient()
        {
            client = new RabbitMQClient("iClock Service Queue");
            client.OnRecieved += (model, message) =>
            {
                if (fingerPrinterManager != null && !string.IsNullOrEmpty(message))
                {
                    try
                    {
                        CommandObject command = client.JsonDeserialize(message);
                        switch (command.Command)
                        {
                            case MessageCommandEnum.DISCONNECT:
                                {
                                    string ip = command.Data as string;
                                    fingerPrinterManager.Disconnect(ip);
                                    break;
                                }
                            case MessageCommandEnum.CONNECT:
                                {
                                    string ip = command.Data as string;
                                    fingerPrinterManager.Connect(ip);
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message, ex);
                    }
                }
            };
        }

        protected void FingerPrintThreadStart()
        {
            fingerPrinterManager = new FingerPrintManager();
            fingerPrinterManager.Startup();
            fingerPrinterManager.Start();
            Application.Run();
        }

        public void Stop()
        {
            client.CloseConnection();
            client = null;
            fingerPrinterManager.Stop();
            fingerPrintThread.Join(1000);
            if (fingerPrintThread.IsAlive)
            {
                fingerPrintThread.Abort();
            }
        }
    }
}
