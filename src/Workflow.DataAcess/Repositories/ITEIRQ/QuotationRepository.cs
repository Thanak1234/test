using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.ITEIRQ;

namespace Workflow.DataAcess.Repositories.ITEIRQ
{
    public class QuotationRepository : RepositoryBase<Quotation>, IQuotationRepository
    {
        public QuotationRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public IList<Quotation> GetByRequestHeaderId(int id)
        {
            IDbSet<Quotation> dbSet = DbContext.Set<Quotation>();
            try
            {
                return dbSet.Where(p => p.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Quotation>();
            }
        }
    }
}
