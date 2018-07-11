using Workflow.DataContract.K2;
using Workflow.Framework;

namespace Workflow.Business
{
    public class WorkflowEngine
    {
        private IProcInstProvider _provider;
        public WorkflowEngine() {

        }

        public WorkflowEngine(string currentUser) {
            _provider = new ProcInstProvider(currentUser);
        }

        public void Execute(string serialNo, string actionName)
        {
            _provider.Execute(new ExecInstParam() {
                SerialNo = serialNo,
                Action = actionName
            });
        }
    }
}
