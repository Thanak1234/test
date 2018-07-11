using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.ReportingService.Report
{
    public class RSServerCredentials : IReportServerCredentials
    {
        public string userName = ConfigurationManager.AppSettings["rsUser"];
        public string password = ConfigurationManager.AppSettings["rsPassword"];
        public string domain = ConfigurationManager.AppSettings["rsDomain"];

        public WindowsIdentity ImpersonationUser
        {

            //Use the default windows user.  Credentials will be
            //provided by the NetworkCredentials property.

            get { return null; }
        }

        public ICredentials NetworkCredentials
        {

            get
            {
                //Read the user information from the web.config file. 
                //By reading the information on demand instead of storing
                //it, the credentials will not be stored in session,
                //reducing the vulnerable surface area to the web.config
                //file, which can be secured with an ACL.

                if ((string.IsNullOrEmpty(userName)))
                {
                    throw new Exception("Missing user name from web.config file");
                }

                if ((string.IsNullOrEmpty(password)))
                {
                    throw new Exception("Missing password from web.config file");
                }

                if ((string.IsNullOrEmpty(domain)))
                {
                    throw new Exception("Missing domain from web.config file");
                }

                return new NetworkCredential(userName, password, domain);

            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                   out string userName, out string password,
                   out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }

    }

}
