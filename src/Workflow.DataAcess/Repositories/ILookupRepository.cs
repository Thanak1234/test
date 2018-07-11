using System;
using System.Collections.Generic;
using System.Data.Entity;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess
{
    public interface ILookupRepository : IRepository<Lookup>
    {
        IDbSet<E> GetDataSet<E>() where E : class;
        IDbSet<Lookup> Lookups();
        Lookup GetLookup(string packageName);
    }
}
