using System.Collections.Generic;
using Workflow.Domain.Entities.IRF;
using Workflow.Domain.Entities.VAF;

namespace Workflow.Domain.Entities.BatchData
{
    public class IRFWorkflowInstance : AbstractWorkflowInstance {
        public IEnumerable<IRFRequestItem> ItemRecords { get; set; }
        public IEnumerable<IRFRequestItem> ItemNewRecords { get; set; }
        public IEnumerable<IRFRequestItem> ItemUpdatedRecords { get; set; }
        public IEnumerable<IRFRequestItem> ItemRemovedRecords { get; set; }

        public IEnumerable<IRFVendor> VendorRecords { get; set; }
        public IEnumerable<IRFVendor> VendorNewRecords { get; set; }
        public IEnumerable<IRFVendor> VendorUpdatedRecords { get; set; }
        public IEnumerable<IRFVendor> VendorRemovedRecords { get; set; }
    }
}