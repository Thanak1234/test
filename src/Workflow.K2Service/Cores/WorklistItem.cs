using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using K2API=SourceCode.Workflow.Client;

namespace Workflow.K2Service.Cores {

    public class WorklistItem {
        public string OpenBy { get; set; }
        public string AllocatedUser {
            get;
            set;
        }
        public string Data {
            get;
            set;
        }
        public long ID {
            get;
            set;
        }
        public string SerialNumber {
            get;
            set;
        }
        public WorklistStatus Status {
            get;
            set;
        }
        public ActionCollection Actions {
            get;
            set;
        }
        public ProcessInstance ProcessInstance {
            get;
            set;
        }
        public ActivityInstanceDestination ActivityInstanceDestination {
            get;
            set;
        }
        public DestinationCollection DelegatedUsers {
            get;
            set;
        }

        public EventInstance EventInstance {
            get;
            set;
        }

        public static WorklistItem FromApi(K2API.WorklistItem wli) {
            return new WorklistItem {
                AllocatedUser = wli.AllocatedUser,
                Data = wli.Data,
                ID = (long)wli.ID,
                SerialNumber = wli.SerialNumber,
                Status = (WorklistStatus)wli.Status,
                Actions = ActionCollection.FromApi(wli.Actions),
                ActivityInstanceDestination = ActivityInstanceDestination.FromApi(wli.ActivityInstanceDestination),
                EventInstance = EventInstance.FromApi(wli.EventInstance),
                ProcessInstance = ProcessInstance.FromApi(wli.ProcessInstance)
            };
        }
        public void ToApi(SourceCode.Workflow.Client.WorklistItem wli) {
            if (this.ActivityInstanceDestination != null) {
                this.ActivityInstanceDestination.ToApi(wli.ActivityInstanceDestination);
            }
            if (this.EventInstance != null) {
                this.EventInstance.ToApi(wli.EventInstance);
            }
            if (this.ProcessInstance != null) {
                this.ProcessInstance.ToApi(wli.ProcessInstance);
            }
        }
    }
}
