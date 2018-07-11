using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Workflow.FingerPrint
{
    public class QueueTCPServer
	{
		public static IPAddress DEFAULT_SERVER = IPAddress.Parse("127.0.0.1"); 
		public static int DEFAULT_PORT=31001;
		public static IPEndPoint DEFAULT_IP_END_POINT = 
			new IPEndPoint(DEFAULT_SERVER, DEFAULT_PORT);

		private TcpListener networkTCPServer = null;
		private bool stopServer=false;
		private bool stopPurging=false;

		private Thread serverThread = null;
		private Thread purgingThread = null;		
        private Thread fingerPrintThread = null;
        private System.Timers.Timer pingFingerPrintTimer = null;

        private List<Socket> networkSockets = null;
        private List<QueueTCPSocketListener> socketListeners = null;

        protected IFingerPrintManager fingerPrinterManager = null;
        protected ILog logger = LogManager.GetLogger(typeof(QueueTCPServer));

        public QueueTCPServer()
		{
			Init(DEFAULT_IP_END_POINT);
		}
		public QueueTCPServer(IPAddress serverIP)
		{
			Init(new IPEndPoint(serverIP, DEFAULT_PORT));
		}

		public QueueTCPServer(int port)
		{
			Init(new IPEndPoint(DEFAULT_SERVER, port));
		}

		public QueueTCPServer(IPAddress serverIP, int port)
		{
			Init(new IPEndPoint(serverIP, port));
		}

		public QueueTCPServer(IPEndPoint ipNport)
		{
			Init(ipNport);
		}

		~QueueTCPServer()
		{
			StopServer();
		}

		private void Init(IPEndPoint ipNport)
		{
			try
			{
				networkTCPServer = new TcpListener(ipNport);
			}
			catch(Exception)
			{
				networkTCPServer=null;
			}
		}

		public void StartServer()
		{
			if (networkTCPServer!=null)
			{
                try {
                    networkSockets = new List<Socket>();
                    socketListeners = new List<QueueTCPSocketListener>();

                    networkTCPServer.Start();
                    serverThread = new Thread(new ThreadStart(ServerThreadStart));
                    serverThread.Priority = ThreadPriority.AboveNormal;
                    serverThread.Start();

                    purgingThread = new Thread(new ThreadStart(PurgingThreadStart));
                    purgingThread.Priority = ThreadPriority.Normal;
                    purgingThread.Start();

                    fingerPrintThread = new Thread(new ThreadStart(FingerPrintThreadStart));
                    fingerPrintThread.SetApartmentState(ApartmentState.STA);
                    fingerPrintThread.Start();

                    pingFingerPrintTimer = new System.Timers.Timer();
                    pingFingerPrintTimer.Interval = 3000;
                    pingFingerPrintTimer.Elapsed += PingFingerPrintThread_Elapsed;
                    pingFingerPrintTimer.Start();
                } catch(Exception ex) {
                    logger.Error(ex.Message, ex);
                }                
            }
		}

        private void PingFingerPrintThread_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (fingerPrinterManager != null) {
                fingerPrinterManager.PingFingerPrintConnection();
            }
        }

        public void FingerPrintThreadStart() {
            fingerPrinterManager = new FingerPrintManager(networkSockets);
            fingerPrinterManager.Startup();
            fingerPrinterManager.Start();
            Application.Run();
        }

		public void StopServer()
		{
			if (networkTCPServer!=null)
			{
                stopServer = true;
                networkTCPServer.Stop();

                CloseAllSocket();
                fingerPrinterManager.Stop();

                serverThread.Join(1000);
                
                if (serverThread.IsAlive) {
                    serverThread.Abort();
                }
                serverThread = null;

                stopPurging = true;
                purgingThread.Join(1000);
                if (purgingThread.IsAlive) {
                    purgingThread.Abort();
                }
                purgingThread = null;

                fingerPrintThread.Join(1000);
                if (fingerPrintThread.IsAlive) {
                    fingerPrintThread.Abort();
                }
                fingerPrintThread = null;

                if (pingFingerPrintTimer != null) {
                    pingFingerPrintTimer.Stop();
                    pingFingerPrintTimer.Dispose();
                    pingFingerPrintTimer = null;
                }

                networkTCPServer = null;
            }
		}
		private void ServerThreadStart()
		{
			Socket clientSocket = null;
			while(!stopServer)
			{
				try
				{
                    clientSocket = networkTCPServer.AcceptSocket();
                    if(clientSocket != null) {
                        lock (networkSockets) {
                            networkSockets.Add(clientSocket);
                        }

                        var socketListener = new QueueTCPSocketListener(clientSocket, fingerPrinterManager);
                        lock (socketListeners) {                           
                            socketListeners.Add(socketListener);                            
                        }
                        socketListener.StartSocketListener();
                    }
				}
				catch (SocketException se)
				{
					stopServer = true;
                    logger.Error(se.Message, se);
                }
			}
		}


        private void CloseAllSocket() {
            try {
                networkSockets.ForEach(s => s.Close());
                networkSockets.Clear();
                networkSockets = null;
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }            
        }

		private void PurgingThreadStart()
		{
			while (!stopPurging)
			{
                if(networkSockets != null) {
                    List<Socket> removes = new List<Socket>();
                    lock (networkSockets) {
                        networkSockets.ForEach(s => {
                            if (!IsSocketConnected(s)) {
                                removes.Add(s);
                            }
                        });

                        for (int i = 0; i < removes.Count; i++) {
                            var s = removes[i];
                            networkSockets.Remove(s);
                            s.Close();
                        }
                    }
                    removes = null;
                }
				
                if(socketListeners != null) {
                    List<QueueTCPSocketListener> deletes = new List<QueueTCPSocketListener>();
                    lock(socketListeners) {
                        socketListeners.ForEach(s => {
                            if(s.IsMarkedForDeletion()) {
                                deletes.Add(s);
                            }
                        });
                    }
                    for(int i=0; i<deletes.Count; i++) {
                        var c = deletes[i];
                        socketListeners.Remove(c);
                        c.StopSocketListener();
                    }
                }
				
				Thread.Sleep(10000);
			}
		}

        private bool IsSocketConnected(Socket s) {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }
    }
}
