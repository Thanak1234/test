using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using log4net;
using Workflow.FingerPrint;
using Newtonsoft.Json;
using Workflow.DataContract.Fingerprint;

namespace Workflow.FingerPrint {

	public class QueueTCPSocketListener
	{
        protected ILog logger = LogManager.GetLogger(typeof(QueueTCPSocketListener));

        public Socket clientSocket = null;
		private bool stopClient=false;
		private Thread clientListenerThread=null;
		private bool markedForDeletion=false;
        private IFingerPrintManager fingerPrintManager = null;

        public QueueTCPSocketListener(Socket clientSocket, IFingerPrintManager fingerPrintManager)
		{
			this.clientSocket = clientSocket;
            this.fingerPrintManager = fingerPrintManager;
        }

		~QueueTCPSocketListener()
		{
			StopSocketListener();
		}

        public void StartSocketListener()
		{
            if (clientSocket != null) {
                try {
                    clientListenerThread =
                    new Thread(new ThreadStart(SocketListenerThreadStart));
                    clientListenerThread.Priority = ThreadPriority.Lowest;
                    clientListenerThread.Start();
                } catch(Exception ex) {
                    logger.Error(ex.Message, ex);
                }
                
            }
        }
        
		private void SocketListenerThreadStart()
		{
			int size=0;			
            while (!stopClient) {
                try {
                    byte[] byteBuffer = new byte[1024];
                    size = clientSocket.Receive(byteBuffer);
                    Subcriber(byteBuffer, size);
                } catch (SocketException se) {
                    stopClient = true;
                    markedForDeletion = true;
                    logger.Error(se.Message, se);
                }
            }
        }
		public void StopSocketListener()
		{
            try {
                if (clientSocket != null) {
                    stopClient = true;
                    clientListenerThread.Join(1000);
                    if (clientListenerThread.IsAlive) {
                        clientListenerThread.Abort();
                    }
                    clientListenerThread = null;
                    clientSocket = null;
                    markedForDeletion = true;
                }
            } catch (Exception ex) {
                logger.Error(ex.Message, ex);
            }
		}

		public bool IsMarkedForDeletion()
		{
			return markedForDeletion;
		}

		private void Subcriber(byte [] byteBuffer, int size)
		{
            string content = Encoding.ASCII.GetString(byteBuffer, 0, size);
            if(fingerPrintManager != null && !string.IsNullOrEmpty(content)) {                
                try {
                    CommandObject command = JsonConvert.DeserializeObject<CommandObject>(content);
                    switch (command.Command) {
                        case MessageCommandEnum.DISCONNECT: {
                                string ip = command.Data as string;
                                fingerPrintManager.Disconnect(ip);
                                break;
                            }
                        case MessageCommandEnum.CONNECT: {
                                string ip = command.Data as string;
                                fingerPrintManager.Connect(ip);
                                break;
                            }
                    }
                } catch (Exception ex) {
                    logger.Error(ex.Message, ex);
                }                
            }
        }
	}
}
