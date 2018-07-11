using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Workflow.K2Service.Cores {

    public class SecurityLabelUtils {

        public static string GetNameWithoutLabel(string name) {
            return Regex.Replace(name, "^K2:", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string GetNameWithLabel(string name) {
            if (name.StartsWith("K2:"))
                return name;
            return string.Format("K2:{0}", name);
        }

        public static bool IsCorrectUserName(string name) {
            string pattern = @"^(k2:)?nagaworld[\\]\w+$";
            return Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase);
        }
    }

}
