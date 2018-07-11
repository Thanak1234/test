using log4net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Workflow.FingerPrint
{
    public class QueueTCPClient {

        public delegate void OnRecievedHandler(Socket socket, string data);
        private OnRecievedHandler _OnRecieved;
        public event OnRecievedHandler OnRecieved {
            add
            {
                _OnRecieved -= value;
                _OnRecieved = null;
                _OnRecieved += value;
            }
            remove
            {
                _OnRecieved -= value;
            }
        }
        Thread mainThread = null;

        public string IP { get; set; }
        public int Port { get; set; }

        protected Socket clientSocket;
        protected ILog logger = LogManager.GetLogger(typeof(QueueTCPClient));
        protected bool stopped = false;

        protected System.Timers.Timer retryTimer;
        protected int retryMax = 1000;
        protected int retryNum = 0;

        public void ConnectServer(string ip, int port) {
            if (string.IsNullOrEmpty(ip) || port == 0) {
                throw new ArgumentException("IP can not empty or null and port can't be 0");
            }
            IP = ip;
            Port = port;
            Disconnect();
            mainThread = new Thread(new ThreadStart(ClientThreadStart));
            mainThread.Start();
            retryTimer = new System.Timers.Timer();
            retryTimer.Interval = 5000;
            retryTimer.Elapsed += RetryTimer_Elapsed;
            retryTimer.Stop();
        }

        private void RetryTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            retryTimer.Stop();
            if (retryNum <= retryMax) {
                if(clientSocket != null) {
                    try {
                        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
                        stopped = false;
                        retryNum = 0;
                        Main();
                    } catch(Exception se) {
                        logger.Error(se.Message, se);
                        clientSocket.Close();                        
                        retryNum++;
                        retryTimer.AutoReset = true;
                        retryTimer.Start();
                    }
                }
                
            } else {
                retryNum = 0;
                stopped = true;
            }
        }

        private void ClientThreadStart() {
            stopped = false;
            try {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port));
            } catch(Exception ex) {
                stopped = true;
                logger.Error(ex.Message, ex);
                retryTimer.Start();
            }
            Main();
        }

        private void Main() {
            while (!stopped && clientSocket != null) {
                try {
                    byte[] byteBuffer = new byte[1024];
                    int size = clientSocket.Receive(byteBuffer);
                    string data = Encoding.ASCII.GetString(byteBuffer, 0, size);
                    _OnRecieved(clientSocket, data);
                } catch (Exception ex) {
                    logger.Error(ex.Message, ex);
                    if (ex is SocketException) {
                        stopped = true;
                        clientSocket.Close();
                        retryTimer.Start();
                    }
                }
            }
        }

        public void Send(string data) {
            try {
                clientSocket.Send(Encoding.ASCII.GetBytes(data));
            } catch(Exception ex) {
                logger.Error(ex.Message, ex);
            }            
        }

        public void Disconnect() {
            if (clientSocket != null) {
                clientSocket.Close();
                clientSocket = null;
            }
            if (mainThread != null) {
                stopped = true;
                mainThread.Join(1000);
                if(mainThread.IsAlive) {
                    mainThread.Abort();
                }
                mainThread = null;
            }
        }
    }
}
