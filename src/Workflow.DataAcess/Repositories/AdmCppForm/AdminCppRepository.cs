using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities.ADMCPPForm;

namespace Workflow.DataAcess.Repositories.AdmCppForm {
    public class AdminCppRepository : RepositoryBase<ADMCPP>, IAdminCppRepository {
        public AdminCppRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ADMCPP GetByRequestHeaderId(int id)
        {
            IDbSet<ADMCPP> dbSet = DbContext.Set<ADMCPP>();
            try
            {
                var result = dbSet.FirstOrDefault(p => p.RequestHeaderId == id);
                return result;
            }
            catch (SmartException e)
            {
                throw e;
            }
        }

        public bool ExistSerialNo(string serialNo, int requestHeaderId) {
            IDbSet<ADMCPP> dbSet = DbContext.Set<ADMCPP>();
            var entity = dbSet.FirstOrDefault(p => p.CPSN == serialNo && p.RequestHeaderId != requestHeaderId);
            return entity != null;
        }
    }
}
