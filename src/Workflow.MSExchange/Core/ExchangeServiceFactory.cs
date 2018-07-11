using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange.Core {
    public class ExchangeServiceFactory {

        /// <summary>
        /// ExchangeServiceFactory contructor
        /// </summary>
        static ExchangeServiceFactory() {
            CertificateCallback.Initialize();
        }

        /// <summary>
        /// Redirection Url Validation Callback
        /// </summary>
        /// <param name="redirectionUrl"></param>
        /// <returns></returns>
        private static bool RedirectionUrlValidationCallback(string redirectionUrl) {            
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            if (redirectionUri.Scheme == "https") {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// create new ExchangeService
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static ExchangeService CreateExchangeService(UserInfo userData) {
            return CreateExchangeService(userData, null);
        }

        /// <summary>
        /// create new ExchangeService include ITraceListener
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="listener"></param>
        /// <returns></returns>
        public static ExchangeService CreateExchangeService(UserInfo userData, ITraceListener listener) {
            ExchangeService service = new ExchangeService(userData.Version);

            if (listener != null) {
                service.TraceListener = listener;
                service.TraceFlags = TraceFlags.All;
                service.TraceEnabled = true;
            }

            service.Credentials = new NetworkCredential(userData.EmailAddress, userData.Password);

            if (userData.AutodiscoverUrl == null) {
                service.AutodiscoverUrl(userData.EmailAddress, RedirectionUrlValidationCallback);
                userData.AutodiscoverUrl = service.Url;
            } else {
                service.Url = userData.AutodiscoverUrl;
            }

            return service;
        }

        /// <summary>
        /// Connect To Service With Impersonation
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="impersonatedUserSMTPAddress"></param>
        /// <returns></returns>
        public static ExchangeService ConnectToServiceWithImpersonation(UserInfo userData, string impersonatedUserSMTPAddress) {
            return ConnectToServiceWithImpersonation(userData, impersonatedUserSMTPAddress, null);
        }

        /// <summary>
        /// Connect To Service With Impersonation include ITraceListener
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="impersonatedUserSMTPAddress"></param>
        /// <param name="listener"></param>
        /// <returns></returns>
        public static ExchangeService ConnectToServiceWithImpersonation(UserInfo userData, string impersonatedUserSMTPAddress, ITraceListener listener) {
            ExchangeService service = new ExchangeService(userData.Version);
            if (listener != null) {
                service.TraceListener = listener;
                service.TraceFlags = TraceFlags.All;
                service.TraceEnabled = true;
            }
            service.Credentials = new NetworkCredential(userData.EmailAddress, userData.Password);
            ImpersonatedUserId impersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, impersonatedUserSMTPAddress);
            service.ImpersonatedUserId = impersonatedUserId;
            if (userData.AutodiscoverUrl == null) {
                service.AutodiscoverUrl(userData.EmailAddress, RedirectionUrlValidationCallback);
                userData.AutodiscoverUrl = service.Url;
            } else {
                service.Url = userData.AutodiscoverUrl;
            }

            return service;
        }
    }
}
