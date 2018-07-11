using System.ServiceProcess;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Workflow.FingerPrint
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            log4net.Config.XmlConfigurator.Configure();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new iClockService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
