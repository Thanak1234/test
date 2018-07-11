using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {

    public class ActionCollection : Collection<Action> {

        public static ActionCollection FromApi(Actions actions) {
            ActionCollection actionCollection = new ActionCollection();
            foreach (SourceCode.Workflow.Client.Action act in actions) {
                actionCollection.Add(Action.FromApi(act));
            }
            return actionCollection;
        }

    }
}
