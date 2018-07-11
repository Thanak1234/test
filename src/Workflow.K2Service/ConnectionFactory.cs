using SourceCode.Security.UserRoleManager.Management;
using System.Configuration;

namespace Workflow.K2Service
{

    public class ConnectionFactory : IConnectionFactory {

        protected UserRoleManager manager;

        public UserRoleManager GetRoleManager() {
            if(manager == null)
                    manager = new UserRoleManager();
            try {
                manager.CreateConnection();
                manager.Connection.Open(ConfigurationManager.ConnectionStrings["HostServer"].ConnectionString);
            } catch (SmartException e) {
                if (manager.Connection != null && manager.Connection.IsConnected == true) {
                    manager.Connection.Close();
                }
                throw e;
            }
            return manager;
        }

    }

}
