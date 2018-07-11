using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Service;
using Workflow.Web.Service.Models;

namespace Workflow.Web.Service.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            ViewBag.UI_RESOURCE_PATH = (env == "DEVELOPMENT") ? "/Workflow" : "";
            return View();
        }
    }
}