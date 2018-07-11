namespace Workflow.DataObject.BPMDATA
{
    public class ActionProperty
    {
        public string WorkflowPath { get; set; }

        public string ActivityName { get; set; }

        public string ActionName { get; set; }

        public bool IsSaveRequestData { get; set; }

        public bool IsSaveAttachments { get; set; }

        public bool IsAddNewRequestHeader { get; set; }

        public bool IsEditPriority { get; set; }

        public bool IsEditRequestor { get; set; }

        public bool IsSaveActivityHistory { get; set; }

        public bool IsUpdateLastActivity { get; set; }

        public bool? TriggerWorkflow { get; set; }

        public bool IsRequiredComment { get; set; }

        public bool IsRequiredAttachment { get; set; }

        //public bool? TriggerWorkflow {
        //    get {
        //        return _triggerWorkflow;
        //    }
        //    set {
        //        if (value != null) {
        //            _triggerWorkflow = value;
        //        }
        //    }
        //}

        //private bool? _triggerWorkflow = true;
    }
}