using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {
    public class WorklistItemCollection : Collection<WorklistItem> {

        public static WorklistItemCollection FromApi(SourceCode.Workflow.Client.Worklist worklist) {
            WorklistItemCollection worklistItemCollection = new WorklistItemCollection();
            foreach (SourceCode.Workflow.Client.WorklistItem wli in worklist) {                
                worklistItemCollection.Add(WorklistItem.FromApi(wli));
            }
            return worklistItemCollection;
        }

    }
}
