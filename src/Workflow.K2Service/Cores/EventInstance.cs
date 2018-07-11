using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {

    public class EventInstance {

        public string Description {
            get;
            set;
        }
        public long ExpectedDuration {
            get;
            set;
        }
        public long ID {
            get;
            set;
        }
        public string Metadata {
            get;
            set;
        }
        public string Name {
            get;
            set;
        }
        public int Priority {
            get;
            set;
        }
        public System.DateTime StartDate {
            get;
            set;
        }

        public EventInstance() {
            this.ExpectedDuration = -1;
        }

        public static EventInstance FromApi(SourceCode.Workflow.Client.EventInstance eventInstance) {
            return new EventInstance {
                Description = eventInstance.Description,
                ExpectedDuration = (long)eventInstance.ExpectedDuration,
                ID = (long)eventInstance.ID,
                Metadata = eventInstance.MetaData,
                Name = eventInstance.Name,
                Priority = eventInstance.Priority,
                StartDate = eventInstance.StartDate.ToUniversalTime()
            };
        }

        public void ToApi(SourceCode.Workflow.Client.EventInstance eventInstance) {
            if (this.ExpectedDuration != -1L) {
                eventInstance.ExpectedDuration = (int)this.ExpectedDuration;
            }
        }
    }
}
