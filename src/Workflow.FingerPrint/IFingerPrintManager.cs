namespace Workflow.FingerPrint
{
    public interface IFingerPrintManager {
        void Startup();
        void Start();
        void Stop();
        void Disconnect(string ip);
        void PingFingerPrintConnection();
        void Connect(string ip);
    }
}
