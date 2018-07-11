using FluentValidation.WebApi;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Workflow.Scheduler;
using Workflow.Service.Ticketing;

namespace Workflow.Web.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public DateTime SecurityTokenValidFrom { get; set; }

        private readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));
            // configure FluentValidation model validator provider
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);

            TicketBackgroundProcess.startBackground();
            logger.Info("TicketBackgroundProcess start");

            SchedulerProvider.Instance.Init();
            SchedulerProvider.Instance.Scheduler.Start();
            logger.Info("Scheduler start");
        }
        

        protected void Application_End(object sender, EventArgs e)
        {
            TicketBackgroundProcess.endBackground();
            logger.Info("stop ");

            if(SchedulerProvider.Instance.Scheduler.IsStarted) {
                SchedulerProvider.Instance.Scheduler.Shutdown();
            }
        }

        protected void Application_Error(object sender, EventArgs e) {
            Exception lastError = Server.GetLastError();
        }
    }
}
