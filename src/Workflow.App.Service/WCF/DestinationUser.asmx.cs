using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories;
using Workflow.App.Service.Models;

namespace Workflow.App.Service.WCF
{
    /// <summary>
    /// Summary description for Destination
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DestinationUser : System.Web.Services.WebService
    {
        Repository<Models.DestinationUser> repository;
        public DestinationUser()
        {
            repository = new Repository<Models.DestinationUser>();
        }

        [WebMethod]
        public List<Models.DestinationUser> GetDestinationUsersByActivity(
            int RequestHeaderId, 
            string ActivityName, 
            bool IsMergeActivity)
        {
            string sql = @"EXEC [BPMDATA].[DESTINATION_USERS] 
                    @RequestHeaderId = {0}, 
                    @ActivityName = '{1}',
                    @IsMergeActivity = {2}";

            var query = repository.SqlQuery(string.Format(sql, RequestHeaderId, ActivityName, IsMergeActivity));

            return query.ToList();
        }
        
    }
}
