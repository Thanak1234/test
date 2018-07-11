using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Workflow.DataObject
{
    public class QueryParameter
    {
        public string query { get; set; }
        public int start { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string sort { get; set; }

        public IEnumerable<SortQueryParameter> GetSorts() {
            if (string.IsNullOrEmpty(sort))
                return null;

            return new JavaScriptSerializer().Deserialize<IEnumerable<SortQueryParameter>>(sort);
        }
    }
}
