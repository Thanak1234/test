using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.MSExchange.Core {
    public class UserInfo {

        public ExchangeVersion Version { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Uri AutodiscoverUrl { get; set; }

        public static UserInfo CreateUserData(string email, string password, ExchangeVersion version) {
            UserInfo user = new UserInfo();
            user.EmailAddress = email;
            user.Password = password;
            user.Version = version;
            return user;
        }
    }
}
