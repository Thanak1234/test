namespace Workflow.DataContract.Fingerprint
{
    public class CommandObject {
        public MessageCommandEnum Command { get; set; }
        public object Data { get; set; }
    }
}
