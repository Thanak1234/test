using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K2API = SourceCode.Workflow.Client;

namespace Workflow.K2Service.Cores {
    public class ActivityInstanceDestination {
        public long ActID {
            get;
            set;
        }
        public long ActInstID {
            get;
            set;
        }
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
        public long ProcInstID {
            get;
            set;
        }
        public System.DateTime StartDate {
            get;
            set;
        }
        public DataFieldCollection DataFields {
            get;
            set;
        }
        public XmlFieldCollection XmlFields {
            get;
            set;
        }
        public ActivityInstanceDestination() {
            this.Priority = -1;
            this.ExpectedDuration = -1L;
        }
        public static ActivityInstanceDestination FromApi(K2API.ActivityInstanceDestination aid) {
            return new ActivityInstanceDestination {
                ActID = (long)aid.ActID,
                ActInstID = (long)aid.ActInstID,
                Description = aid.Description,
                ExpectedDuration = (long)aid.ExpectedDuration,
                ID = (long)aid.ID,
                Metadata = aid.MetaData,
                Name = aid.Name,
                Priority = aid.Priority,
                ProcInstID = (long)aid.ProcInstID,
                StartDate = aid.StartDate.ToUniversalTime()
            };
        }
        public void ToApi(K2API.ActivityInstanceDestination aid) {
            if (this.DataFields != null) {
                this.DataFields.ToApi(aid.DataFields);
            }
            if (this.XmlFields != null) {
                this.XmlFields.ToApi(aid.XmlFields);
            }
            if (this.ExpectedDuration != -1L) {
                aid.ExpectedDuration = (int)this.ExpectedDuration;
            }
            if (this.Priority != -1) {
                aid.Priority = this.Priority;
            }
        }
    }
}
