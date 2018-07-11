using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ClientApi = SourceCode.Workflow.Client;

namespace Workflow.K2Service.Cores {

    public class DestinationCollection : Collection<Destination> {

        public static DestinationCollection FromApi(Destinations destinations) {
            DestinationCollection destinationCollection = new DestinationCollection();
            foreach (ClientApi.Destination dest in destinations) {
                destinationCollection.Add(Destination.FromApi(dest));
            }
            return destinationCollection;
        }

    }

}
