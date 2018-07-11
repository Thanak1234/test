using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Repositories.ticket
{
    public class TicketLookupRepository : RepositoryBase<TicketType>, ITicketLookupRepository
    {
        public TicketLookupRepository(IDbFactory dbFactory) : base(dbFactory){ }

        public TicketStatus getStatus(int statusId)
        {
            return DbContext.Set<TicketStatus>().Where(t => t.Id == statusId).Single();
        }

        public IEnumerable<TicketAgent> listTicketAgent()
        {
            return DbContext.Set<TicketAgent>().ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketAgent(int teamId)
        {

            string sqlString = @"SELECT A.ID Id,
	                                EMP.DISPLAY_NAME display ,
	                                EMP.EMP_NO display1,
	                                EMP.EMAIL display2,
	                                EMP.POSITION description 
                                FROM TICKET.AGENT A
	                                INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON A.ID = AT.AGENT_ID 
	                                INNER JOIN HR.VIEW_EMPLOYEE_LIST EMP ON EMP.ID = A.EMP_ID
                                WHERE A.STATUS = 'ACTIVE'
	                                and AT.STATUS ='ACTIVE'
                                    and AT.TEAM_ID = " +  teamId ;

           return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(int teamId) {

            string sqlString = @"SELECT EMP.ID Id,
	                                EMP.DISPLAY_NAME display ,
	                                EMP.EMP_NO display1,
	                                EMP.EMAIL display2,
	                                EMP.POSITION description 
                                FROM TICKET.AGENT A
	                                INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON A.ID = AT.AGENT_ID 
	                                INNER JOIN HR.VIEW_EMPLOYEE_LIST EMP ON EMP.ID = A.EMP_ID
                                WHERE A.STATUS = 'ACTIVE'
	                                and AT.STATUS ='ACTIVE'
                                    and AT.TEAM_ID = " + teamId;

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<GeneralLookupDto> GetAgentEmpIdByTeam(IEnumerable<int> teamId)
        {
            string teamIdStr = teamId == null || teamId.Count() <= 0 ? "" : " AND AT.TEAM_ID IN (" + listToStr(teamId) + @") ";
            string sqlString = @"SELECT EMP.ID Id,
	                                EMP.DISPLAY_NAME display ,
	                                EMP.EMP_NO display1,
	                                EMP.EMAIL display2,
	                                T.TEAM_NAME description
									FROM TICKET.AGENT A (NOLOCK)
	                                INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT (NOLOCK) ON A.ID = AT.AGENT_ID 
	                                INNER JOIN HR.VIEW_EMPLOYEE_LIST EMP (NOLOCK) ON EMP.ID = A.EMP_ID
									INNER JOIN TICKET.TEAM T (NOLOCK) ON AT.TEAM_ID = T.ID
                                WHERE A.STATUS = 'ACTIVE'
	                                and AT.STATUS ='ACTIVE'
                                    " + teamIdStr;

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<TicketCategory> listTicketCategory()
        {
            return DbContext.Set<TicketCategory>().Where(t => (t.Status == "ACTIVE")).ToList();
        }

        public IEnumerable<TicketDepartment> listTicketDept()
        {
            return DbContext.Set<TicketDepartment>().Where(t => (t.Status == "ACTIVE")).ToList();
        }

        public IEnumerable<TicketImpact> listTicketImpact()
        {
            return DbContext.Set<TicketImpact>().ToList();
        }

        public IEnumerable<TicketItem> listTicketItem()
        {
            return DbContext.Set<TicketItem>().Where(t => (t.Status == "ACTIVE")).ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketItem(int subCateId=0)
        {
            string sqlString = @"SELECT ID id,
	                                ITEM_NAME display,
	                                DESCRIPTION description
                                FROM TICKET.ITEM
                                
                                WHERE STATUS = 'ACTIVE' AND("+ subCateId + " = 0 OR SUB_CATE_ID = " + subCateId+")";

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        private string listToStr(IEnumerable<int> list)
        {   
            if (list == null) return null;
            string value = String.Join(",", list);
            
            return value.IsEmpty() ? null : value;            
        }

        public IEnumerable<GeneralLookupDto> listTicketItem(IEnumerable<int> cateId, IEnumerable<int> subCateId, bool breadcrumb)
        {
            string bcStr = "I.ITEM_NAME";

            if (breadcrumb)
            {
                bcStr = "(C.CATE_NAME+'\\'+ SUB.SUB_CATE_NAME)";
            }

            string cateIdStr = cateId == null || cateId.Count() <= 0 ? "" : " AND C.ID IN (" + listToStr(cateId) + @") ";
            string subCateIdStr = subCateId == null || subCateId.Count() <= 0 ? "" : " AND I.SUB_CATE_ID IN (" + listToStr(subCateId) +@") ";
            string sqlString = @"SELECT I.ID id,
                                    I.ITEM_NAME display,
                                    "+bcStr+ @" display1,
	                                I.DESCRIPTION description
                                FROM TICKET.ITEM I (NOLOCK)
                                INNER JOIN TICKET.SUB_CATEGORY SUB (NOLOCK) ON I.SUB_CATE_ID = SUB.ID
								INNER JOIN TICKET.CATEGORY C (NOLOCK) ON SUB.CATE_ID = C.ID                                 
                                WHERE I.STATUS = 'ACTIVE' " 
                                + cateIdStr
                                + subCateIdStr
                                + @" ORDER BY I.ITEM_NAME"
                                ;

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<TicketPriority> listTicketPriority()
        {
            return DbContext.Set<TicketPriority>().ToList();
        }

        public IEnumerable<TicketSite> listTicketSite()
        {
            return DbContext.Set<TicketSite>().ToList();
        }

        public IEnumerable<TicketSLA> listTicketSLA()
        {
            return DbContext.Set<TicketSLA>().ToList();
        }

        public IEnumerable<TicketSource> listTicketSource()
        {
            return DbContext.Set<TicketSource>().ToList();
        }

        public IEnumerable<TicketStatus> listTicketStatus(bool all)
        {
            if (all)
            {
                return DbContext.Set<TicketStatus>().ToList();
            }
            else
            {
                return DbContext.Set<TicketStatus>().Where(t => t.StateId != 3).ToList();
            }

        }

        /*
        0 = all,
        1 = not deleted
        2 = Active
        */
        public IEnumerable<TicketStatus> listTicketStatus(int selectedState, int currStatusId)

        {

            if (selectedState==0)
            {
                return DbContext.Set<TicketStatus>().ToList();
            }
            else if(selectedState == 1)
            {
                return DbContext.Set<TicketStatus>().Where(t => t.StateId != 3 || t.Id == currStatusId).ToList();
            }
            else if (selectedState == 2)
            {
                return DbContext.Set<TicketStatus>().Where(t => (t.StateId != 3 && t.StateId != 2) ||  t.Id == currStatusId).ToList();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TicketStatus> listTicketStatus(TicketingLookupParamsDto param)
        {

            if (param.includeRemoved == false)
            {
                return DbContext.Set<TicketStatus>().Where(t=> t.StateId != 3).ToList();
            }            
            else
            {
                return DbContext.Set<TicketStatus>().ToList();
            }
        }

        public IEnumerable<TicketSubCategory> listTicketSubCate()
        {
            return DbContext.Set<TicketSubCategory>().ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketSubCate(int cateId, bool breadcrumb)
        {
            string bcStr = "SUB_CATE_NAME";

            if (breadcrumb)
            {
                bcStr = "(C.CATE_NAME+'\\'+ SUB_CATE_NAME)";
            }

            string sqlString = @"SELECT SUB.ID id, " + bcStr + @" display, SUB.DESCRIPTION description
                                FROM TICKET.SUB_CATEGORY SUB
								INNER JOIN TICKET.CATEGORY C ON SUB.CATE_ID = C.ID        
                                WHERE SUB.STATUS='ACTIVE' AND (@cateId = 0 OR SUB.CATE_ID = @cateId)
                                ORDER BY SUB.SUB_CATE_NAME ";

            return SqlQuery<GeneralLookupDto>(sqlString, new object[] { new SqlParameter("cateId", cateId) }).ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketSubCate(IEnumerable<int> cateId, bool breadcrumb)
        {
            string bcStr = "SUB.SUB_CATE_NAME";

            if (breadcrumb)
            {
                bcStr = "(C.CATE_NAME)";
            }

            string cateIdStr = cateId == null || cateId.Count() <= 0 ? "" : " AND SUB.CATE_ID IN (" + listToStr(cateId) + @")";

            string sqlString = @"SELECT SUB.ID id, SUB.SUB_CATE_NAME display, " + bcStr + @" display1, SUB.DESCRIPTION description
                                FROM TICKET.SUB_CATEGORY SUB (NOLOCK)
								INNER JOIN TICKET.CATEGORY C (NOLOCK) ON SUB.CATE_ID = C.ID        
                                WHERE SUB.STATUS='ACTIVE' "+cateIdStr+@"
                                ORDER BY SUB.SUB_CATE_NAME ";

            //return SqlQuery<GeneralLookupDto>(sqlString, new object[] {new SqlParameter("cateIdStr", cateIdStr), new SqlParameter("cateId", cateId) }).ToList();
            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<TicketTeam> listTicketTeam()
        {
            return DbContext.Set<TicketTeam>().Where(t => (t.Status == "ACTIVE")).ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketTeam(int ticketId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketUrgency> listTicketUrgency()
        {
            return DbContext.Set<TicketUrgency>().ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketGroupPolicy() {
            string sqlString = @"SELECT 
                                GP.ID id,
                                GP.GROUP_NAME display,
                                GP.DESCRIPTION description
                                FROM TICKET.GROUP_POLICY GP
                                ORDER BY GP.GROUP_NAME ";

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }
        public IEnumerable<GeneralLookupDto> listTicketStatus() {
            string sqlString = @"SELECT 
                                L.ID id,
                                L.LOOKUP_NAME display,
                                L.LOOKUP_CODE display1,
                                L.DESCRIPTION description
                                FROM TICKET.[LOOKUP] L 
                                WHERE 
                                L.ACTIVE = 1
                                AND L.LOOKUP_KEY = 'AGENT_STATUS'
                                ORDER BY L.LOOKUP_NAME ";

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }
        public IEnumerable<GeneralLookupDto> listTicketAccounttype()
        {
            string sqlString = @"SELECT 
                                L.ID id,
                                L.LOOKUP_NAME display,
                                L.LOOKUP_CODE display1,
                                L.DESCRIPTION description
                                FROM TICKET.[LOOKUP] L 
                                WHERE 
                                L.ACTIVE = 1
                                AND L.LOOKUP_KEY = 'AGENT_ACCOUNT_TYPE'
                                ORDER BY L.LOOKUP_NAME ";

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketDepartment()
        {
            string sqlString = @"SELECT 
                                D.ID id,
                                D.DEPT_NAME display,
                                D.DESCRIPTION description
                                FROM TICKET.DEPARTMENT D
                                ORDER BY D.DEPT_NAME";

            return SqlQuery<GeneralLookupDto>(sqlString).ToList();
        }

        public IEnumerable<GeneralLookupDto> listTicketByKey(string lookupKey)
        {
            string sqlString = @"SELECT 
                    L.ID id,
                    L.LOOKUP_NAME display,
                    L.LOOKUP_CODE display1,
                    L.[DESCRIPTION] description
                    FROM TICKET.[LOOKUP] L 
                    WHERE 
                    L.ACTIVE = 1
                    AND L.LOOKUP_KEY = @lookupKey
                    ORDER BY L.LOOKUP_NAME
                    ";
            return SqlQuery<GeneralLookupDto>(sqlString, new object[] { new SqlParameter("lookupKey", lookupKey) }).ToList();
            
        }

        public IEnumerable<TicketType> listTicketType(bool includeUnidentified)
        {
            return includeUnidentified ? DbContext.Set<TicketType>().ToList() : DbContext.Set<TicketType>().Where(t=>t.Id != 0).ToList();
        }
    }

    
}
