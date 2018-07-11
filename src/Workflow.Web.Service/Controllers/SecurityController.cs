using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Workflow.Web.Service.Controllers
{
    public class SecurityController : Controller
    {
        [HttpGet]
        public void Logout() {
            //ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            //    return true;
            //};

            //string logoutUrl = ConfigurationManager.AppSettings["K2LogoutUrl"] ?? "https://forms.nagaworld.com:444/Identity/STS/Forms/Account/Logout/";

            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(logoutUrl);
            //webRequest.Method = "GET";
            //try {
            //    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            //    Stream content = webResponse.GetResponseStream();
            //    StreamReader respone = new StreamReader(content);

            //    SingOut();

            //    RedirectToHome();
            //} catch (SmartException ex) {
            //    throw ex;
            //}            
        }


        public ActionResult Index()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            ViewBag.UI_RESOURCE_PATH = (env == "DEVELOPMENT") ? "/Workflow" : "";
            return View();
        }

        public ActionResult Renew()
        {
            var env = ConfigurationManager.AppSettings["Environment"];
            ViewBag.UI_RESOURCE_PATH = (env == "DEVELOPMENT") ? "/Workflow" : "";
            return View();
        }

        private void SingOut() {
            //WSFederationAuthenticationModule fam = FederatedAuthentication.WSFederationAuthenticationModule;
            //FormsAuthentication.SignOut();
            //fam.SignOut(true);
        }

        private void RedirectToHome() {
            String queryString = HttpContext.Request.Url.PathAndQuery;
            String baseUrl = HttpContext.Request.Url.AbsoluteUri.Replace(queryString, "/");
            Response.Redirect(baseUrl);
        }

        public int Ping() {
            return 1;
        }
    }
}