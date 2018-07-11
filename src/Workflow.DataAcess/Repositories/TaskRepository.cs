/**
*@author : Phanny
*/
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Dashboard;

namespace Workflow.DataAcess.Repositories
{
    public class TaskRepository: RepositoryBase<object>, ITaskRepository {

    
        public TaskRepository(IDbFactory dbFactory) : base(dbFactory) {
            
        }

        public ResourceWrapper GetTasksByLoginName(TaskQueryParameter queryParameter) {

            object owner = DBNull.Value;
            object submittedBy = DBNull.Value;

            string orderBy = GetOrderBy(queryParameter);
            var totalRecords = new SqlParameter("@TotalRecords", System.Data.SqlDbType.Int);
            totalRecords.Direction = System.Data.ParameterDirection.Output;

            if(!string.IsNullOrEmpty(queryParameter.ContributedBy)) {
                owner = queryParameter.ContributedBy;
            }

            if (!string.IsNullOrEmpty(queryParameter.SubmittedBy)) {
                submittedBy = queryParameter.SubmittedBy;
            }

            var query = DbContext.Database.SqlQuery<TaskDto>("[BPMDATA].[GET_SUBMITTOR_TASK] @owner=@owner, @SubmittedBy=@SubmittedBy, @IsAssigned=@IsAssigned, @Start=@Start, @Limit=@Limit, @Query=@Query, @ORDER=@ORDER, @TotalRecords=@TotalRecords OUT", new object[] {
                new SqlParameter("@owner", owner),
                new SqlParameter("@SubmittedBy", submittedBy),
                new SqlParameter("@IsAssigned", queryParameter.IsAssigned),
                new SqlParameter("@Start", queryParameter.start),
                new SqlParameter("@Limit", queryParameter.limit),
                new SqlParameter("@Query", queryParameter.query),
                new SqlParameter("@ORDER", orderBy),
                totalRecords
            });

            ResourceWrapper wrapper = new ResourceWrapper();            
            wrapper.Records = query.ToList();
            wrapper.TotalRecords = (int)totalRecords.Value;
            
            return wrapper;
        }

        public string GetOrderBy(TaskQueryParameter queryParameter) {
            string order = string.Empty;

            var sorts = queryParameter.GetSorts();

            if (sorts == null || sorts.Count() == 0)
                return string.Empty;

            sorts.Each((p) => {
                order += string.Format("{0} {1},", GetColumnName(p.Property), p.Direction);
            });

            if(order.Length > 0) {
                order = order.Substring(0, order.Length - 1);
            }

            return string.Format("ORDER BY {0}", order);
        }

        public string GetColumnName(string property) {
            string column = Regex.Replace(property, "([A-Z])", "_$1");
            return column.ToUpper();
        }
    }
}
