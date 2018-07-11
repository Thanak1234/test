using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }

        //public IEnumerable<Document> GetDocumentList(int id) {

        //    string sqlString = @"SELECT * FROM [BPMDATA].[DOCUMENT] WHERE ObjectId IN (
        //        SELECT ID FROM[BPMDATA].[APPROVAL_COMMENT] WHERE REQUEST_HEADER_ID = @requestHeaderId
        //    )";

        //    var documentList = SqlQuery<Document>(sqlString, new SqlParameter("@requestHeaderId", id));
        //    return documentList.ToList();
        //    ;
        //}

        public IEnumerable<DocumentModel> GetDocumentList(int id)
        {
            string sqlString = @"SELECT 
	            D.*,
	            (CASE WHEN A.ACTIVITY IN ('Submit Request', 'Submission', 'Request Submission', 'Requestor Rework') THEN 
		            'ORIGINATOR' 
	            ELSE 
		            UPPER(REPLACE(A.ACTIVITY, ' ', '_'))
	            END) ActivityCode
            FROM [BPMDATA].[DOCUMENT] D
            INNER JOIN [BPMDATA].[APPROVAL_COMMENT] A ON A.ID = D.ObjectId AND ObjectName = '[BPMDATA].[APPROVAL_COMMENT]'
            WHERE D.DeletedDate IS NULL AND A.REQUEST_HEADER_ID = " + id ;

            return SqlQuery<DocumentModel>(sqlString).ToList();
        }

    }

    public class DocumentFileRepository : RepositoryBase<DocumentFile>, IDocumentFileRepository
    {
        public DocumentFileRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
