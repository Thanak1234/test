using System.ServiceProcess;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
namespace Workflow.EmailCapture
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TicketEmailService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
