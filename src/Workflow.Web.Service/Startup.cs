using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Workflow.Web.Service.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace Workflow.Web.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
