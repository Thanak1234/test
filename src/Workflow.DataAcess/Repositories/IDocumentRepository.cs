using System.Collections.Generic;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        IEnumerable<DocumentModel> GetDocumentList(int id);
    }

    public interface IDocumentFileRepository : IRepository<DocumentFile>
    {

    }
}
