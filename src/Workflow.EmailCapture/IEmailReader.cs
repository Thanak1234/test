using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.EmailCapture {

    public interface IEmailReader {
        void StartNotification();
        void StopNotification();
        void PullEmail();
        void StartSchedule();
        void StopSchedule();
    }

}
