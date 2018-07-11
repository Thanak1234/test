using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.Ticket;
using Workflow.DataObject;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket {
	public class TicketingRepository : RepositoryBase<TicketItem>, ITicketingRepository {
		public TicketingRepository(IDbFactory dbFactory) : base(dbFactory) { }
       
        public object GetReportResult(TicketingSearchParamsDto param) {
            var totalRecords = new SqlParameter("TotalRecords", System.Data.SqlDbType.Int);
            totalRecords.Direction = System.Data.ParameterDirection.Output;

            string sql = @"EXEC TICKET.[REPORT] @ticketNo = @ticketNo,
                                            @subject = @subject,
                                            @sourceId = @sourceId,
                                            @teamId = @teamId,
                                            @itemId = @itemId,
                                            @statusId = @statusId,
                                            @subCateId = @subCateId,
                                            @typeId = @typeId,
                                            @cateId = @cateId,
                                            @assignedId = @assignedId,
                                            @start = @start,
                                            @limit = @limit, 
                                            @page = @page,
                                            @priority = @priority,
                                            @deptIds = @deptIds,
                                            @completedDateFrom = @completedDateFrom,
                                            @completedDateTo = @completedDateTo,
                                            @dueDateFrom = @dueDateFrom,
                                            @dueDateTo = @dueDateTo,
                                            @requestorId = @requestorId,
                                            @submittedDateFrom = @submittedDateFrom,
                                            @submittedDateTo = @submittedDateTo,
                                            @CurrLoginEmpId = @CurrLoginEmpId,
                                            @SlaId = @SlaId,
                                            @IncludeRemoved = @IncludeRemoved,                                            
                                            @Description = @Description, 
                                            @Comment = @Comment, 
                                            @RootCauseId = @RootCauseId,
                                            @totalRecords = @totalRecords OUT";
            var sqlParams = new List<SqlParameter>()
            {
                new SqlParameter("@ticketNo", GetSQLParamByString(param.ticketNo)),
                new SqlParameter("@subject", GetSQLParamByString(param.subject)),
                new SqlParameter("@sourceId", GetSQLParamByInt(param.SourceId)),
                new SqlParameter("@priority", GetSQLParamByList(param.Priority)),
                new SqlParameter("@itemId", GetSQLParamByList(param.ItemId)),
                new SqlParameter("@statusId", GetSQLParamByList(param.StatusId)),
                new SqlParameter("@subCateId", GetSQLParamByList(param.SubCateId)),
                new SqlParameter("@typeId", GetSQLParamByList(param.TypeId)),
                new SqlParameter("@cateId", GetSQLParamByList(param.CateId)),
                new SqlParameter("@teamId", GetSQLParamByList(param.TeamId)),
                new SqlParameter("@assignedId", GetSQLParamByList(param.assignedId)),
                new SqlParameter("@start", GetSQLParamByInt(param.Start)),
                new SqlParameter("@limit", GetSQLParamByInt(param.Limit)),
                new SqlParameter("@page", GetSQLParamByInt(param.Page)),
                new SqlParameter("@deptIds", GetSQLParamByString(param.Depts)),
                new SqlParameter("@requestorId", GetSQLParamByInt(param.RequestorId)),
                new SqlParameter("@completedDateFrom", GetSQLParamByDateTime(param.CompletedDateFrom)),
                new SqlParameter("@completedDateTo", GetSQLParamByDateTime(param.CompletedDateTo)),
                new SqlParameter("@dueDateFrom", GetSQLParamByDateTime(param.DueDateFrom)),
                new SqlParameter("@dueDateTo", GetSQLParamByDateTime(param.DueDateTo)),
                new SqlParameter("@CurrLoginEmpId", GetSQLParamByInt(param.CurrLoginEmpId)),
                new SqlParameter("@submittedDateFrom", GetSQLParamByDateTime(param.SubmittedDateFrom)),
                new SqlParameter("@submittedDateTo", GetSQLParamByDateTime(param.SubmittedDateTo)),
                new SqlParameter("@SlaId", GetSQLParamByList(param.SlaId)),
                new SqlParameter("@IncludeRemoved", GetSQLParamByBool(param.IncludeRemoved)),
                new SqlParameter("@Description", GetSQLParamByString(param.description)),
                new SqlParameter("@Comment", GetSQLParamByString(param.comment)),
                new SqlParameter("@RootCauseId", GetSQLParamByList(param.RootCauseId))
            };
            sqlParams.Add(totalRecords);        
            var queryResult = SqlQuery<TicketingDto>(sql, sqlParams.ToArray());
            return new { totalRecords = totalRecords.Value, records = queryResult };
		}

        public object GetSQLSortParam(string sort = null)
        {
            if (sort == null) return DBNull.Value;
            return getSorting(sort);
        }

        private string getSorting(string sort)
        {
            string sorting = "";

            string[] pairs = sort.Split(',');

            for (int i = 0; i < pairs.Length; i += 2)
            {
                string[] pair = pairs[i].Split(':');
                string[] ord = pairs[i + 1].Split(':');

                if (sorting.Length > 0)
                {
                    sorting += ",";
                }
                // get rid of all extra json characters.
                string pro = pair[1].Trim(' ', '{', '}', '[', ']', '\\', '\"', '"');
                pro = mappingColumnField()[pro.ToLower()];
                sorting += pro + " " + ord[1].Trim(' ', '{', '}', '[', ']', '\\', '\"', '"');
            }

            //return "'"+sorting+"'";
            return sorting;
        }

        private Dictionary<string, string> mappingColumnField()
        {
            Dictionary<string, string> mappedField = new Dictionary<string, string>();
            //mappedField.Add("subject", "TK.SUBJECT");
            mappedField.Add("ticketno", "Ticketing.TICKET_NO");
            //mappedField.Add("createddate", "TK.CREATED_DATE");
            //mappedField.Add("requestor", "CASE WHEN TK.REQUESTOR_ID =-1 THEN TNR.ORIGINATOR ELSE ISNULL(EMP.DISPLAY_NAME, 'N/A') END");
            //mappedField.Add("assignee", "EMP1.DISPLAY_NAME");
            //mappedField.Add("status", "ST.STATUS");
            //mappedField.Add("tickettype", "TT.[TYPE_NAME]");
            //mappedField.Add("priority", "PR.PRIORITY_NAME");
            //mappedField.Add("sla", "SLA.SLA_NAME");
            //mappedField.Add("waitactionedby", "TK.WAIT_ACTIONED_BY");
            return mappedField;
        }

        public object GetSQLParamByString(string val) {
            if (string.IsNullOrWhiteSpace(val) || string.IsNullOrEmpty(val))
            {
                return DBNull.Value;
            }
            else
            {
                return val;
            }
        }

        public object GetSQLParamByList(List<int?> collection) {
            if (collection == null || collection.Count == 0)
                return DBNull.Value;
            return string.Join(",", collection.ToArray());
        }

        public object GetSQLParamByStringList(List<string> collection)
        {
            if (collection == null || collection.Count == 0)
                return DBNull.Value;
            return string.Join(",", collection.ToArray());
        }

        public object GetSQLParamByDateTime(DateTime? dt) {
            if (dt == null)
                return DBNull.Value;
            return dt;
        }

        public object GetSQLParamByInt(int? val) {
            if (val == null)
                return DBNull.Value;
            return val;
        }

        public object GetSQLParamByBool(bool? val)
        {
            if (val == null)
                return false;
            return val;
        }
    }

    
}
