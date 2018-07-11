using System;
using Workflow.DataContract.Fingerprint;
using zkemkeeper;

namespace Workflow.FingerPrint
{
    public delegate void OnTouchHandler(FingerPrintClient client, iClockEventArg evt);    
    public interface IFingerPrintClient {
        string IP { get; set; }
        int Port { get; set; }
        int MachineNumber { get; set; }
        event OnTouchHandler OnTouch;
        void Connect(bool force, Func<CZKEMClass, bool, bool> onConnected);
        void DisConnect();
        bool IsNetConnected();
        void UpdateFingerPrintStatus(string status);
        void DisConnect(string status);
        void Reconnect(Func<CZKEMClass, bool, bool> onCompleted);
    }
}
