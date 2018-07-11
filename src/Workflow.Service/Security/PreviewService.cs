/**
*@author : Yim Samaune
*/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Security;

namespace Workflow.Service {
    public class PreviewService //: IPreviewService
    {
        private DbContext _context;
        
        public PreviewService() {
            var dbFactory = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            _context = dbFactory.init();
        }

        public IList<AccessControl> GetFormAcl(int requestHeaderId, string user)
        {
            try
            {
                var query = _context.Database.SqlQuery<AccessControl>(
                    "EXEC [BPMDATA].[SP_FORM_ACL] @RequestHeaderId, @User", 
                    new object[] {
                        new SqlParameter("@RequestHeaderId", requestHeaderId),
                        new SqlParameter("@User", user)
                    }
                );

                return query.ToList();
            }
            catch (Exception)
            {

            }
            return new List<AccessControl>();
        }
    }
}
