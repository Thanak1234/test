using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {

    public class ProcessInstance {

        public string Description {
            get;
            set;
        }
        public long ExpectedDuration {
            get;
            set;
        }
        public string Folder {
            get;
            set;
        }
        public string Folio {
            get;
            set;
        }
        public string FullName {
            get;
            set;
        }
        public string Guid {
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
        public ProcessStatus Status {
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
        public string ViewFlow {
            get;
            set;
        }
        public ProcessInstance() {
            this.Priority = -1;
        }

        public static ProcessInstance FromApi(SourceCode.Workflow.Client.ProcessInstance processInstance) {
            return new ProcessInstance {
                Description = processInstance.Description,
                ExpectedDuration = (long)processInstance.ExpectedDuration,
                Folder = processInstance.Folder,
                Folio = processInstance.Folio,
                FullName = processInstance.FullName,
                Guid = processInstance.Guid.ToString(),
                ID = (long)processInstance.ID,
                Metadata = processInstance.MetaData,
                Name = processInstance.Name,
                Priority = processInstance.Priority,
                StartDate = processInstance.StartDate.ToUniversalTime(),
                Status = (ProcessStatus)processInstance.Status1,
                ViewFlow = processInstance.ViewFlow
            };
        }
        public void ToApi(SourceCode.Workflow.Client.ProcessInstance pi) {
            if (this.DataFields != null) {
                this.DataFields.ToApi(pi.DataFields);
            }
            if (this.XmlFields != null) {
                this.XmlFields.ToApi(pi.XmlFields);
            }
            if (!string.IsNullOrEmpty(this.Folio)) {
                pi.Folio = this.Folio;
            }
            if (this.Priority != -1) {
                pi.Priority = this.Priority;
            }
            if (this.ExpectedDuration != 0L) {
                pi.ExpectedDuration = (int)this.ExpectedDuration;
            }
        }
    }
}
