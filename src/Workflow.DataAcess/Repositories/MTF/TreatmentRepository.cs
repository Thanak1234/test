using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MTF;

namespace Workflow.DataAcess.Repositories.MTF
{
    public class TreatmentRepository : RepositoryBase<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public Treatment GetByRequestHeader(int id)
        {
            IDbSet<Treatment> dbSet = DbContext.Set<Treatment>();
            try
            {
                return dbSet.Single(p => p.RequestHeaderId == id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public string GetPendingTreamentMessage()
        {
            try
            {
                var pendingNumber = SqlQuery<string>(@"EXEC [HR].[GET_ESTIMATED_REQUEST]");
                return pendingNumber.Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateDayLeave(int requestHeaderId)
        {
            try
            {
                var sqlString = @"UPDATE HR.TREATMENT 
                    SET [DAYS] = (SELECT SUM(NO_DAY) TOTAL_DAYS FROM HR.UNFIT_TO_WORK WHERE REQUEST_ID = REQUEST_HEADER_ID) 
                    WHERE REQUEST_HEADER_ID = @RequestHeaderId ";

                DbContext.Database.ExecuteSqlCommand(sqlString, new SqlParameter("@RequestHeaderId", requestHeaderId));
            }
            catch (Exception)
            {

            }
        }
        public void DeleteUniftTW(int requestHeaderId)
        {
            try
            {
                DbContext.Database.ExecuteSqlCommand(
                "UPDATE HR.UNFIT_TO_WORK SET REQUEST_ID = -REQUEST_ID WHERE REQUEST_ID = @RequestHeaderId",
                new SqlParameter("@RequestHeaderId", requestHeaderId));
            }
            catch (Exception) { }
        }
        public string GetSubjectEmail(int requestHeaderId)
        {
            try
            {
                return SqlQuery<string>(@"EXEC [HR].[GET_TREATMENT_BY_HEADER] " + requestHeaderId).Single();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
