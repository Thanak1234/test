/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Workflow.Business.Ticketing;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.ticket;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.Ticket;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Email;
using Workflow.Domain.Entities.Ticket;
using Workflow.Framework;
using Workflow.Service.FileUploading;

namespace Workflow.Service.Ticketing
{
    public class TicketDataProcessingProvider : IDataProcessingProvider , ITicketEnquiry
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITicketRepository ticketRepository;
        private readonly ITicketActivityRepository ticketActivityRepo;
        private readonly ITicketDeptRepository ticketDeptRepo;
        private readonly ITicketAssignmentRepository ticketAssignRepo;
        private readonly ITicketRoutingRepository ticketRoutingRepo;
        private readonly ITicketLookupRepository ticketLookupRepo;
        private readonly IAgentRepository agentRepo;
        private readonly ITicketFileUploadRepository fileUploadRepo;
        private readonly IFileUploadService fileUploadService;
        private readonly ITicketChangeStatusActRepository changeStatusActRepo;
        private readonly IEmployeeRepository empRepo;
        private readonly ISimpleRepository<TicketMerged> ticketMergedRepo;
        private readonly ISimpleRepository<TicketSubTkLink> ticketSubTkLinkRepo;
        private readonly ISimpleRepository<TicketNotification> ticketNotificationRepo;
        private readonly ITicketItemRepository itemRepo;
        private readonly TicketSlaRepository slaRepo;
        private readonly ISimpleRepository<TicketNoneReqEmp> ticketNoneReqEmpsRepo;
        private readonly ISimpleRepository<MailList> mailListRepo = null;
        private readonly ISimpleRepository<EmailItem> mailItemRepo = null;
        private readonly ITicketTeamRepository teamRepo = null;

        private readonly ISimpleRepository<TicketFormIntegrated> formIntegratedRepo;
        private readonly IDbFactory dbFactory = null;

        private IRequestUserRepository requestUserRepository = null;
        private IRequestItemRepository requestItemRepository = null;

        public TicketDataProcessingProvider(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
            this.unitOfWork = new UnitOfWork(dbFactory);
            this.ticketRepository = new TicketRepository(dbFactory);
            this.ticketActivityRepo = new TicketActivityRepository(dbFactory);
            this.ticketDeptRepo = new TicketDeptRepository(dbFactory);
            this.ticketAssignRepo = new TicketAssignmentRepository(dbFactory);
            this.ticketRoutingRepo = new TicketRoutingRepository(dbFactory);
            this.ticketLookupRepo = new TicketLookupRepository(dbFactory);
            this.agentRepo = new AgentRepository(dbFactory);
            this.fileUploadRepo = new TicketFileUploadRepository(dbFactory);
            this.fileUploadService = new FileUploadService();
            this.changeStatusActRepo = new TicketChangeStatusActRepository(dbFactory);
            this.empRepo = new EmployeeRepository(dbFactory);
            this.ticketMergedRepo = new SimpleRepository<TicketMerged>(dbFactory);
            this.itemRepo = new TicketItemRepository(dbFactory);
            this.slaRepo = new TicketSlaRepository(dbFactory);
            this.ticketSubTkLinkRepo = new SimpleRepository<TicketSubTkLink>(dbFactory);
            this.ticketNotificationRepo = new SimpleRepository<TicketNotification>(dbFactory);
            this.ticketNoneReqEmpsRepo = new SimpleRepository<TicketNoneReqEmp>(dbFactory);
            this.mailListRepo = new SimpleRepository<MailList>(dbFactory);
            this.formIntegratedRepo = new SimpleRepository<TicketFormIntegrated>(dbFactory);
            this.mailItemRepo = new SimpleRepository<EmailItem>(dbFactory);
            this.requestUserRepository = new RequestUserRepository(dbFactory);
            this.requestItemRepository = new RequestItemRepository(dbFactory);
            this.teamRepo = new TicketTeamRepository(dbFactory);
        }

        public int createTicketActivity(TicketActivity ticketActivity)
        {
            ticketActivityRepo.Add(ticketActivity);
            return ticketActivity.Id;
        }

        public TicketItem getTicketItem(int ticketItemId)
        {
            var item = itemRepo.GetById(ticketItemId);
            return item;
        }

        public Ticket getTicket(int ticketId)
        {
            return ticketRepository.GetById(ticketId);
        }

        public void saveAttachmentFiles(List<string> fileSerials, TicketActivity ticketActivity)
        {
           foreach(string serial in fileSerials)
            {
                Console.WriteLine(serial);
            }
        }

        public int saveTicket(Ticket ticket)
        {

            try
            {
                if (ticket.Id == 0)
                {
                    this.ticketRepository.Add(ticket);
                }
                else
                {
                    this.ticketRepository.Update(ticket);
                }

                return ticket.Id;
            }catch(Exception e)
            {
                throw e;
            }  
        }

        public int getDefaultTicketItem(int deptId)
        {
            var item = ticketDeptRepo.GetById(deptId);
            return item.DefaultItemId;

        }
        
        public int getDefaultDepartmentId()
        {
            try
            {
                var sql = "select ID id from TICKET.DEPARTMENT WHERE IS_DEFAULT=1 AND STATUS ='ACTIVE' ";

                var ret = ticketDeptRepo.SqlQuery<ResultId>(sql).Single();
                return ret.id;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public string getLastTicketNo()
        {
            return ticketRepository.getLastTicket();
        }

        public void saveTicketAssignment(TicketAssignment ticketAssignment)
        {
            if (ticketAssignment.Id > 0)
            {
                ticketAssignRepo.Update(ticketAssignment);
            }
            else
            {
                ticketAssignRepo.Add(ticketAssignment);
            }
            
        }

        public void saveTicketRouting(TicketRouting ticketRouting)
        {
            ticketRoutingRepo.Add(ticketRouting);
        }

        public int getDefaultAgentByTeamId(int teamId)
        {
            try
            {
                var sql = @"SELECT top 1 TA.AGENT_ID id 
                            FROM TICKET.TEAM_AGENT_ASSIGN TA 
                                INNER JOIN TICKET.AGENT A ON TA.AGENT_ID = A.ID 
                            WHERE A.STATUS = 'ACTIVE' 
                                AND TA.STATUS = 'ACTIVE' 
                                AND TA.TEAM_ID = {0} 
                                AND TA.IMMEDIATE_ASSIGN = 1 ";

                var value = ticketLookupRepo.SqlQuery<ResultId>( String.Format(sql,teamId)).Single();
                return value == null ? 0 : value.id;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
           
        }

        public List<int> getDestUsersByTeam(int teamId)
        {
            var sql = @"SELECT  A.EMP_ID id 
                        FROM TICKET.TEAM_AGENT_ASSIGN TA
                            INNER JOIN TICKET.AGENT A ON TA.AGENT_ID = A.ID 
                        WHERE A.STATUS = 'ACTIVE' 
                            AND TA.STATUS = 'ACTIVE' 
                            AND TA.IMMEDIATE_ASSIGN != 1
                            AND TA.TEAM_ID={0} ";

            var values = ticketLookupRepo.SqlQuery<ResultId>(string.Format(sql,teamId)).Select(t => t.id);
            return values.ToList();
        }

        public TicketRouting getCurrUsrRouting(int ticketId, int userId)
        {
            var sql = @"SELECT R.ID id 
                     FROM TICKET.ROUTING R 
                         INNER JOIN TICKET.ASSIGNMENT ASSG ON R.TICKET_ASSIGN_ID = ASSG.ID 
                         INNER JOIN TICKET.ACITVITY ACT ON ASSG.ACTIVITY_ID = ACT.ID 
                     WHERE ACT.TICKET_ID = {0} 
                           AND R.STATUS != 'EXPIRED' 
                           AND R.EMP_ID= {1} ";
            try
            {
                var value = ticketLookupRepo.SqlQuery<ResultId>(String.Format(sql, ticketId, userId)).Single();
                return ticketRoutingRepo.GetById(value.id);

            }catch(Exception)
            {
                return null;
            }

            throw new NotImplementedException();
        }

        public void releaseTicket(int ticketId)
        {
           
        }

        public bool isAgent(int empId)
        {
            try
            {
                var val = agentRepo.Get(t => t.EmpId == empId && t.Status == "ACTIVE");
                return val!=null;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public TicketAgent getAgentByEmpId(int empId)
        {
            try
            {
                return agentRepo.Get(t => t.EmpId == empId && t.Status == "ACTIVE");
            } catch (Exception) {
                return null;
            }
            
        }


        public int getAssignee(int ticketId)
        {
            var sql = @"SELECT  ASS.ASSIGNEE_ID id
                     FROM TICKET.ACITVITY ACT 
                         INNER JOIN TICKET.ASSIGNMENT ASS ON ACT.ID = ASS.ACTIVITY_ID  
                     WHERE ACT.TICKET_ID = {0} AND ASS.EXPIRED = 0 ";
            try
            {
                var value = ticketLookupRepo.SqlQuery<ResultId>(String.Format(sql, ticketId)).Single();
                return value.id;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            

        }

        public TicketAgent getAssigedAgent(int ticketId)
        {
            try
            {
                var id = getAssignee(ticketId);
                var ticketAgent = agentRepo.GetById(id);
                return ticketAgent;
            }
            catch (Exception)
            {
                return null;
            }
          
        }


        public TicketAssignment getCurrentAssigned(int ticketId)
        {
            var sql = @"SELECT  ASS.ID id 
                     FROM TICKET.ACITVITY ACT 
                         INNER JOIN TICKET.ASSIGNMENT ASS ON ACT.ID = ASS.ACTIVITY_ID  
                     WHERE ACT.TICKET_ID = {0} AND ASS.EXPIRED = 0 ";
            try
            {
               var value = ticketLookupRepo.SqlQuery<ResultId>(String.Format(sql, ticketId)).Single();
                //return ticketAssignRepo.Get(t => t.TicketActivityId == value.id);
                return ticketAssignRepo.GetById(value.id);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TicketAgent getAgentById(int agentId)
        {
            return agentRepo.GetById(agentId);
        }

        #region Query
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

            return sorting;
        }

        private Dictionary<string, string> mappingColumnField()
        {
            Dictionary<string, string> mappedField = new Dictionary<string, string>();
            mappedField.Add("subject", "TK.SUBJECT");
            mappedField.Add("ticketno", "TK.TICKET_NO");
            mappedField.Add("createddate", "TK.CREATED_DATE");
            mappedField.Add("requestor", "CASE WHEN TK.REQUESTOR_ID =-1 THEN TNR.ORIGINATOR ELSE ISNULL(EMP.DISPLAY_NAME, 'N/A') END");
            mappedField.Add("assignee", "EMP1.DISPLAY_NAME");
            mappedField.Add("status", "ST.STATUS");
            mappedField.Add("tickettype", "TT.[TYPE_NAME]");
            mappedField.Add("priority", "PR.PRIORITY_NAME");
            mappedField.Add("sla", "SLA.SLA_NAME");
            mappedField.Add("waitactionedby", "TK.WAIT_ACTIONED_BY");
            return mappedField;
        }

        public QueryResult getTicketListing(string keyword, int status, string quickQuery,EmployeeDto emp, int execptTecktId, int page = 0, int start = 0, int limit = 0, List<int?> ticketTypeId = null, string sort =null)
        {

            /*
             CASE 
                            WHEN ST.STATE_ID = 3 THEN 'Removed' 
                            WHEN ST.STATE_ID = 2 THEN 'Closed'
                            WHEN TK.LAST_ASSIGNED_EMP_ID =0 THEN 'UnAssigned'
                            WHEN TK.STATUS_ID IN(1,2,4) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'Opened'
                            WHEN TK.STATUS_ID IN(3) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'OnHold'
                        END StatusFlag,
            */

            string sorting = "TK.CREATED_DATE DESC";
                        
            if (sort != null)
            {
                sorting = getSorting(sort);  // parse the sorting json parameter
            }

            var field = @"
                        TK.ID Id, 
                        TK.TICKET_NO TicketNo, 
                        TK.SUBJECT Subject, 
                        TK.CREATED_DATE CreatedDate,
                        CASE WHEN TK.REQUESTOR_ID =-1 
		                    THEN TNR.ORIGINATOR
		                    ELSE ISNULL(EMP.DISPLAY_NAME,'N/A') 
	                    END Requestor, 
                        EMP1.DISPLAY_NAME Assignee, 
                        TK.DUE_DATE DueDate, 
                        DATEADD(SECOND, SLA.FIRST_RESPONSE_PERIOD, TK.CREATED_DATE) FirstResponseDate,
                        ST.STATUS Status, 
                        ST.ID StatusId,
                        s.STATE State, 
                        PR.PRIORITY_NAME Priority, 
                        TM.TEAM_NAME TeamName, 
                        TK.LAST_ACTION LastAction, 
                        TK.LAST_ACTION_DATE LastActionDate, 
                        emp.DISPLAY_NAME LastActionBy, 
                        TK.WAIT_ACTIONED_BY WaitActionedBy ,
                        TK.ACTUAL_MINUTES ActualMinutes,

                        CASE 
                            WHEN ST.STATE_ID = 3 THEN 'REMOVED' 
                            WHEN ST.STATE_ID = 2 THEN 'INACTIVE'
                            ELSE 'ACTIVE' 
                        END StatusFlag,
                        CAST(CASE WHEN NOT(TK.TICKET_TYPE_ID = 4 AND TK.SLA_ID = 6) AND DATEADD(SECOND, SLA.FIRST_RESPONSE_PERIOD, TK.CREATED_DATE) < GETDATE() AND TK.FIRT_RESPONSE=0 AND ST.STATE_ID <>3 THEN 1 ELSE 0 END AS bit) FSViolence,
                        CAST(CASE WHEN NOT(TK.TICKET_TYPE_ID = 4 AND TK.SLA_ID = 6) AND TK.DUE_DATE< COALESCE(TK.COMPLETED_DATE, GETDATE()) AND ST.STATE_ID <>3  THEN 1 ELSE 0 END AS bit) ODViolence,
                        CAST(ISNULL(( 
		                    SELECT TOP 1 1 
		                    FROM TICKET.ACITVITY ACT
			                    INNER JOIN TICKET.SUB_TICKET_LINK STL ON ACT.ID = STL.ACTIVITY_ID
		                    WHERE ACT.ACTIVITY_TYPE = 'SUB_TICKET_POSTING'
			                    AND ACT.TICKET_ID = TK.ID
	                    ),0) AS BIT)  IsMain,
                        CAST(ISNULL((
		                    SELECT TOP 1 1
		                    FROM TICKET.ACITVITY ACT
			                    INNER JOIN TICKET.SUB_TICKET_LINK STL ON ACT.ID = STL.SUB_TICKET_ACT_ID
		                    WHERE ACT.ACTIVITY_TYPE = 'TICKET_POSTING'
			                    AND ACT.TICKET_ID =  TK.ID
	                    ),0) AS BIT) IsSub,

                        ROW_NUMBER() OVER (ORDER BY "+sorting+@") AS RowNum,
						
						SLA.SLA_NAME Sla,
						SR.[SOURCE] Source,
						TT.[TYPE_NAME] TicketType,
                        TT.ICON TicketTypeIcon
                ";

            var sql = @"SELECT 
                        {1}
                    FROM TICKET.TICKETING TK (NOLOCK)
                        LEFT JOIN HR.EMPLOYEE EMP (NOLOCK) ON TK.REQUESTOR_ID = EMP.ID 
                        INNER JOIN TICKET.[STATUS] ST (NOLOCK) ON ST.ID = TK.STATUS_ID 
                        INNER JOIN TICKET.[STATE] S (NOLOCK) ON S.ID = ST.STATE_ID 
                        LEFT JOIN TICKET.[PRIORITY] PR (NOLOCK) ON PR.ID = TK.PRIORITY_ID 
                        LEFT JOIN HR.EMPLOYEE EMP1 (NOLOCK) ON TK.LAST_ASSIGNED_EMP_ID = EMP1.ID 
                        LEFT JOIN TICKET.TEAM TM (NOLOCK) ON TM.ID = tk.LAST_ASSIGNED_TEAM_ID 						
                        LEFT JOIN TICKET.SLA SLA (NOLOCK) ON SLA.ID = TK.SLA_ID
                        LEFT JOIN TICKET.TICKET_NONE_REG_EMP TNR (NOLOCK) ON TK.ID = TNR.TICKET_ID
                        INNER JOIN HR.EMPLOYEE EMP2 (NOLOCK) ON TK.LAST_ACTION_BY = EMP2.ID 
						LEFT JOIN TICKET.[SOURCE] SR (NOLOCK) ON TK.SOURCE_ID = SR.ID
						LEFT JOIN TICKET.[TYPE] TT (NOLOCK) ON TK.TICKET_TYPE_ID = TT.ID
                    WHERE   ";

            var userCon = @"
                (
                    TK.REQUESTOR_ID = {0}
                    OR TK.SUBMITTER_ID = {0}   
                )
            ";
            var agentCon = @" (
                            TK.LAST_ASSIGNED_EMP_ID = {0}
                            OR EXISTS(
                                SELECT 1 FROM TICKET.TEAM TM
                                    INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON TM.ID = AT.TEAM_ID
                                    INNER JOIN TICKET.AGENT A ON A.ID = AT.AGENT_ID
                                    INNER JOIN TICKET.GROUP_POLICY G ON G.ID = A.GROUP_POLICY_ID
                                WHERE TM.STATUS = 'Active'
                                    AND A.STATUS = 'ACTIVE'
                                    AND AT.STATUS = 'ACTIVE'
                                    AND A.EMP_ID = {0}
                                    AND (
                                            G.LIMIT_ACCESS = 'DEPT_TICKET'
                                            OR(G.LIMIT_ACCESS != 'OWN_TICKET'
                                                AND(G.LIMIT_ACCESS != 'TEAM_TICKET' OR AT.TEAM_ID = TK.LAST_ASSIGNED_TEAM_ID)
                                                AND(G.LIMIT_ACCESS != 'CUSTOM' OR EXISTS(SELECT 1 FROM TICKET.TEAM_GROUP_ACCESS TG WHERE TG.GROUP_ACCESS_ID = G.ID AND TG.TEAM_ID = TK.LAST_ASSIGNED_TEAM_ID AND TG.STATUS = 'ACTIVE'))
                                                )
                                        )
                            )
                           )";


            var con = "";

            var asAgentCon = true;

            if(! string.IsNullOrWhiteSpace(keyword))
            {
                con = " AND  (TK.TICKET_NO like '%{0}%'  OR  TK.SUBJECT like '%{0}%') ";
                con = string.Format(con, keyword);
                status = 0;

            }

            if (status > 0 && status < 1000)
            {

                con += " AND ST.ID = {0} ";
                con = string.Format(con, status);

            }
            else if(status>=1000)
            {
                if(status == 1000) //Tickets Assigned To me (Active)
                {
                    con = string.Format(" and TK.LAST_ASSIGNED_EMP_ID = {0}  AND S.ID =1 ", emp.id);
                } else if (status == 1102) //Tickets Assigned To My Team (Active)                  
                {
                    var teams = teamRepo.GetTeamsByEmployeeId(emp.id);
                    con = string.Format("  and TK.LAST_ASSIGNED_TEAM_ID IN ({0}) AND S.ID = 1 ", string.Join(",", teams));
                } else if (status == 1001) //All Assigned Tickets(Active)
                {
                    con = "  and TK.LAST_ASSIGNED_EMP_ID > 0 AND S.ID =1 ";
                }
                else if (status == 1002) //All Unassigned Tickets (Active)
                {
                    con = "  and TK.LAST_ASSIGNED_EMP_ID = 0 AND S.ID =1 ";
                }
                else if (status ==1003 || status == 1201) //All Active Tickets
                {
                    con = "  AND S.ID =1  ";
                }
                else if(status ==1004 || status == 1202)//All Inactive Tickets
                {
                    con = "  AND S.ID =2  ";
                }
                else if(status==1005 || status == 1203)//All Deleted Tickets
                {
                    con = "  AND S.ID =3  ";
                }else if(status == 1100) //My Overdue Tickets
                {
                    con = string.Format(" and TK.LAST_ASSIGNED_EMP_ID = {0}  AND S.ID =1 and  TK.DUE_DATE<GETDATE() ", emp.id);
                }
                else if (status == 1101) //Overdue Tickets
                {
                    con = " AND TK.DUE_DATE<GETDATE() AND S.ID =1  ";
                }

                if(status>= 1200 && status <= 1202)
                {
                    asAgentCon = false;
                }

            }

            if (execptTecktId > 0)
            {
                con += " AND TK.ID !=" + execptTecktId ;
                con += " AND ST.STATE_ID !=3 ";
            }
            
            if (ticketTypeId != null && ticketTypeId.Count > 0)
            {
                con += " AND TT.ID IN (" + string.Join(",", ticketTypeId.ToArray())+")";
            }

            if (con.IsEmpty())
            {
                con = " AND ST.STATE_ID !=3 ";
                
            }
            
            if (asAgentCon)
            {
                sql += agentCon;
            }
            else
            {
                sql += userCon;
            }

            sql += con;
            
            var query = "";

            int count = 0;
            //paging
            if (limit > 0 && page>0)
            {
                query = string.Format(sql, emp.id, field);
                var paging = @"
                       ;with results as (
                        {0}
                        )
                        select * from results 
                        where RowNum between {1} and {2} order by RowNum                   
                    ";
                query = string.Format(paging, query, (page -1) *  limit +1 , page * limit, sorting);


                var squeryCount = string.Format(sql, emp.id, " count(*)  id  ");

                var  resultCount = ticketLookupRepo.SqlQuery<ResultId>(squeryCount).Single();
                count = resultCount.id;
            }
            else
            {
                query = string.Format(sql, emp.id, "top 100 " + field);
            }

            try
            {
                var tickets = ticketLookupRepo.SqlQuery<TicketListing>(query).ToList();

                return new QueryResult()
                {
                    sql = query,
                    total = count,
                    tickets = tickets
                };
            }catch(Exception e)
            {
                throw e;
            }

           
        }


        public void saveFileUploads(List<FileUploadInfo> fileInfoList, TicketActivity ticketActivity)
        {
            List<String> serials = new List<string>();

            foreach(var fileInfo in fileInfoList)
            {

                var fileUpload = new TicketFileUpload()
                {
                    ActivityId = ticketActivity.Id,
                    FileName = fileInfo.fileName,
                    Ext = fileInfo.ext,
                    UploadSerial = fileInfo.serial
                };

                fileUploadRepo.Add(fileUpload);


                serials.Add(fileInfo.serial);
            }

            fileUploadService.markAsSave(serials);
        }

        public TicketFileUpload getFileInfo(string serial)
        {
            var file = fileUploadRepo.Get(t => serial==t.UploadSerial);
            return file;
        }

        public TicketViewDto getTicketView(int ticketId)
        {
            var sql = @"SELECT TK.ID id,  
                        REQ.DISPLAY_NAME empName, 
                        ISNULL(REQ.ID, 0) empId,
                        REQ.EMP_NO empNo, 
                        REQ.POSITION position, 
                        REQ.TEAM_NAME subDept, 
                        REQ.GROUP_NAME groupName, 
                        REQ.DEPT_TYPE division, 
                        REQ.MOBILE_PHONE phone, 
                        REQ.TELEPHONE ext, 
                        REQ.EMAIL email, 
                        REQ.HOD hod, 
                        tk.TICKET_NO ticketNo, 
                        tk.SUBJECT subject, 
                        TK.DESCRIPTION description, 
    
                        TP.ID typeId,
                        TP.TYPE_NAME type,
    
                        ST.ID statusId,
                        ST.STATUS status, 
                        ST.STATE_ID stateId,
    
                        SRC.ID sourceId, 
                        SRC.SOURCE source,
    
                        IM.ID impactId, 
                        IM.IMPACT_NAME impact, 
    
                        UR.ID urgencyId,
                        UR.URGENCY_NAME urgency, 
    
                        PIO.ID priorityId,
                        PIO.PRIORITY_NAME priority, 

                        SLA.ID slaId,
                        SLA.SLA_NAME sla, 
    
                        S.ID siteId,
                        S.SITE_NAME site, 
    
                        CATE.ID categoryId,
                        CATE.CATE_NAME category, 
    
                        SCATE.ID subCateId,
                        SCATE.SUB_CATE_NAME subCate, 
    
                        IT.ID itemId,
                        IT.ITEM_NAME item,
    
                        TM.ID teamId,
                        TM.TEAM_NAME team,
                        TK.ESTIMATED_HOURS estimatedHours,
                        CASE WHEN TK.ACTUAL_MINUTES>0  THEN TK.ACTUAL_MINUTES
							 WHEN TK.COMPLETED_DATE IS NOT NULL THEN DATEDIFF(MINUTE,TK.CREATED_DATE,TK.COMPLETED_DATE)
							 ELSE 0
                        END actualMinutes,
                        TK.DUE_DATE dueDate,
                        DATEADD(SECOND, SLA.FIRST_RESPONSE_PERIOD, TK.CREATED_DATE) firstResponseDate,
                        TK.LAST_ASSIGNED_EMP_ID assignedEmpId,
                        AG.ID assigneeId,
                        ASG.DISPLAY_NAME assignee,
	                    ASG.EMP_NO assigneeNo,
                        TK.CREATED_DATE createdDate,
                        TK.COMPLETED_DATE finishedDate, 
                        TK.VERSION version,
                        REF_TYPE refType,
                        REFERENCE reference
                     FROM TICKET.TICKETING TK (NOLOCK)
                        LEFT JOIN HR.VIEW_EMPLOYEE_ALL REQ (NOLOCK) ON TK.REQUESTOR_ID = REQ.ID  
                        INNER JOIN HR.EMPLOYEE SUB (NOLOCK) ON TK.SUBMITTER_ID = SUB.ID 
                        INNER JOIN TICKET.STATUS ST (NOLOCK) ON TK.STATUS_ID = ST.ID 
                        LEFT JOIN TICKET.SOURCE SRC (NOLOCK) ON TK.SOURCE_ID = SRC.ID 
                        LEFT JOIN TICKET.TYPE TP (NOLOCK) ON TK.TICKET_TYPE_ID = TP.ID 
                        LEFT JOIN TICKET.IMPACT IM (NOLOCK) ON TK.IMPACT_ID = IM.ID 
                        LEFT JOIN TICKET.URGENCY UR (NOLOCK) ON TK.URGENCY_ID = UR.ID 
                        LEFT JOIN TICKET.PRIORITY PIO (NOLOCK) ON TK.PRIORITY_ID = PIO.ID 
                        LEFT JOIN TICKET.SITE S (NOLOCK) ON TK.SITE_ID = S.ID 
                        INNER JOIN TICKET.ITEM IT (NOLOCK) ON TK.ITEM_ID = IT.ID 
                        INNER JOIN TICKET.SUB_CATEGORY SCATE (NOLOCK) ON IT.SUB_CATE_ID = SCATE.ID 
                        INNER JOIN TICKET.CATEGORY CATE (NOLOCK) ON SCATE.CATE_ID = CATE.ID 
                        LEFT JOIN HR.EMPLOYEE LOK (NOLOCK) ON TK.LOCKED_BY = LOK.ID
                        LEFT JOIN TICKET.TEAM TM (NOLOCK) ON TM.ID = TK.LAST_ASSIGNED_TEAM_ID
                        LEFT JOIN HR.EMPLOYEE ASG (NOLOCK) ON ASG.ID = TK.LAST_ASSIGNED_EMP_ID                        
                        LEFT JOIN TICKET.SLA SLA (NOLOCK) ON TK.SLA_ID = SLA.ID
                        LEFT JOIN TICKET.AGENT AG (NOLOCK) ON AG.EMP_ID = ASG.ID AND AG.STATUS ='ACTIVE'
                     WHERE TK.ID = {0}";

            var ticket = ticketLookupRepo.SqlQuery<TicketViewDto>(string.Format(sql, ticketId)).Single();
            return ticket;
            //LEFT JOIN TICKET.SLA_MAPPING SM (NOLOCK) ON TK.TICKET_TYPE_ID = SM.TYPE_ID AND TK.PRIORITY_ID = SM.PRIORITY_ID
        }

        public List<SimpleActivityViewDto> getActivity(int ticketId,EmployeeDto emp)
        {
            var sql = @"SELECT ACT.ID,
                            ACT.ACTIVITY_TYPE activityType, 
                            AT.ACT_TYPE_NAME activityName,
                            ACT.ACTION action,  
                            EMP.DISPLAY_NAME actionBy,  
                            ACT.DESCRIPTION description, 
                            ACT.CREATED_DATE createdDate, 
                            ACT.MODIFIED_DATE modifiedDate,  
                            ACT.VERSION version  
                        FROM TICKET.ACITVITY ACT  
                            INNER JOIN TICKET.ACTIVITY_TYPE AT ON AT.CODE = ACT.ACTIVITY_TYPE 
                            LEFT JOIN HR.EMPLOYEE EMP ON ACT.ACTION_BY = EMP.ID  
                        WHERE ACT.TICKET_ID= {0} 
                            AND (ACT.ACTIVITY_TYPE != 'POST_INTERNAL_NOTE' OR ACT.ACTION_BY={1})";
            var activities = ticketLookupRepo.SqlQuery<SimpleActivityViewDto>(string.Format(sql, ticketId,emp.id)).ToList();
            return activities;
        }


        public NotifyActivityDataDto getActivity(int activityId)
        {

            var sql = @"SELECT 
                            ACT.ID,
                            TK.ID ticketId,
	                        TK.TICKET_NO ticketNo,
                            TK.SUBJECT subject,
                            ACT.ACTIVITY_TYPE activityType, 
                            AT.ACT_TYPE_NAME activityName,
                            ACT.ACTION action,  
                            EMP.DISPLAY_NAME actionBy,  
                            ACT.DESCRIPTION description, 
                            ACT.CREATED_DATE createdDate, 
                            ACT.MODIFIED_DATE modifiedDate,  
                            ACT.VERSION version  
                        FROM TICKET.ACITVITY ACT  
                            INNER JOIN TICKET.ACTIVITY_TYPE AT ON AT.CODE = ACT.ACTIVITY_TYPE 
                            INNER JOIN TICKET.TICKETING TK ON TK.ID = ACT.TICKET_ID
                            LEFT JOIN HR.EMPLOYEE EMP ON ACT.ACTION_BY = EMP.ID  
                        WHERE ACT.ID = {0}";
            var activity = ticketLookupRepo.SqlQuery<NotifyActivityDataDto>(string.Format(sql, activityId)).Single();
            return activity;
        }

        public List<TicketFileUpload> getFileUpload(int activityId)
        {
            var files = fileUploadRepo.GetMany(t => t.ActivityId == activityId).ToList();
            return files;
        }

        public TicketAssignInfo getAssingeeInfo(int activityId)
        {
            var sql = @"SELECT AST.ID id, T.TEAM_NAME team, EMP.EMP_NO empNoAssignee, EMP.DISPLAY_NAME assignee, AST.EXPIRED expired 
                        FROM TICKET.ASSIGNMENT AST 
	                        INNER JOIN TICKET.TEAM T ON AST.TEAM_ID = T.ID  
	                        LEFT JOIN TICKET.AGENT AG ON AG.ID = AST.ASSIGNEE_ID
	                        LEFT JOIN HR.EMPLOYEE EMP ON AG.EMP_ID = EMP.ID  
                        WHERE AST.ACTIVITY_ID = {0}";
            var info = ticketLookupRepo.SqlQuery<TicketAssignInfo>(string.Format(sql, activityId)).Single();
            return info;
        }

        public List<SimpleActivityViewDto> getActivity(List<int> actIds)
        {
            
            if (actIds.IsNullOrEmpty())
            {
                return null;
            }
            var ids = actIds.ToSeparatedString();

            var sql = @"SELECT ACT.ID, 
                            ACT.ACTIVITY_TYPE activityType,  
                            ACT.ACTION action,  
                            EMP.DISPLAY_NAME actionBy,  
                            ACT.DESCRIPTION description,  
                            ACT.CREATED_DATE createdDate, 
                            ACT.MODIFIED_DATE modifiedDate,  
                            ACT.VERSION version  
                        FROM TICKET.ACITVITY ACT  
                            LEFT JOIN HR.EMPLOYEE EMP ON ACT.ACTION_BY = EMP.ID  
                        WHERE ACT.id in ({0}) ";

            var activities = ticketLookupRepo.SqlQuery<SimpleActivityViewDto>(string.Format(sql, ids)).ToList();

            return activities;
        }



       

        public List<ActionDto> getAvailableActions(int ticketStateId, int agentId)
        {
            var access = agentId == 0 ? "USER" : "AGENT";


            var sql = @"SELECT AT.GROUP_NAME groupName, CODE activityCode, AT.ACT_TYPE_NAME name 
                     FROM TICKET.ACTIVITY_TYPE AT  
                        INNER JOIN TICKET.STATE_ACTIVITY SAT ON AT.CODE = SAT.ACTVITY_TYPE_CODE
                        LEFT JOIN TICKET.AGENT AG ON AG.ID ={2} AND SAT.STATE_ID = {0}  
	                    LEFT JOIN TICKET.GROUP_POLICY GP ON GP.ID = AG.GROUP_POLICY_ID  
                     WHERE 
                        AT.ACTION_BT = 1  
                        AND ( AT.ACCESS_TYPE='{1}' OR  AT.ACCESS_TYPE ='BOTH')
	                    AND ( '{1}' !='AGENT' OR   
		                        ( 
			                        (AT.CODE !='TICKET_ASSIGNED' or GP.ASSIGN_TICKET=1)
			                        AND (AT.CODE !='DELETE_TICKET' OR GP.DELETE_TICKET=1)
			                        AND (AT.CODE !='DEPARTMENT_TRANSFER' OR GP.DEPT_TRANSFER=1)
			                        AND (AT.CODE !='MERGE_TICKET' OR GP.MERGE_TICKET=1)
			                        AND (AT.CODE !='EDIT_TICKET_INFO' OR GP.EDIT_TICKET=1)
			                        AND (AT.CODE !='EDIT_REQUESTOR' OR GP.EDIT_REQUESTOR=1)
			                        AND (AT.CODE !='CHANGE_STATUS' OR GP.CHANGE_STATUS=1)
                                    AND (AT.CODE !='SUB_TICKET_POSTING' OR GP.CREATE_SUB_TICKET=1)
		                        )
	                        )
                    ORDER BY AT.GROUP_NAME, AT.ITEM_ORDER
                    ";
                    

            var actions = ticketLookupRepo.SqlQuery<ActionDto>(string.Format(sql, ticketStateId, access,agentId)).ToList();
            return actions;
        }

        public void expiredCurrAssigned(Ticket ticket)
        {
            var sql = @"UPDATE TICKET.ASSIGNMENT 
                    SET EXPIRED =1 
                    WHERE EXISTS(
	                    SELECT 1 
	                    FROM TICKET.ACITVITY ACT 
	                    WHERE 	
		                    TICKET.ASSIGNMENT.ACTIVITY_ID =ACT.ID	
		                    AND ACT.TICKET_ID = {0}
	                    )
	                    AND TICKET.ASSIGNMENT.EXPIRED=0";
            ticketLookupRepo.executeSqlCommand(string.Format(sql, ticket.Id));
        }

        public bool isAgent(EmployeeDto emp)
        {
            return isAgent(emp.id);
        }

        public void saveChangeStatus(TicketChangeActivity changeStatusAct)
        {
            changeStatusActRepo.Add(changeStatusAct);
        }

        public TicketStatus GetCurStatusByTicketId(int ticketId)
        {
            try {
                var sql = @"SELECT ST.ID Id, ST.STATUS Status,ST.STATE_ID StateId, ST.CREATED_DATE CreatedDate, ST.DESCRIPTION Description, ST.MODIFIED_DATE  ModifiedDate
                        FROM TICKET.STATUS ST
	                        INNER JOIN TICKET.TICKETING TK ON ST.ID= TK.STATUS_ID
                        WHERE TK.ID = {0} AND ST.STATE_ID  !=3";
                var status = ticketRepository.SqlQuery<TicketStatus>(string.Format(sql, ticketId)).Single();
                return status;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public string getChangeStatusDesc(int ticketActId)
        {
            try
            {
                var sql = @"SELECT top 1 F.STATUS + ' to ' + T.STATUS description
                    FROM TICKET.STATUS_ACTIVITY SA
	                    INNER JOIN TICKET.STATUS F ON SA.STATUS_FROM_ID = F.ID
	                    INNER JOIN TICKET.STATUS T ON SA.STATUS_TO_ID = T.ID
                    WHERE SA.ACTIVITY_ID = {0}";

                var val = ticketRepository.SqlQuery<ResultString>(string.Format(sql, ticketActId)).Single();
                return val.description;
            }
            catch
            {
                return "";
            }
            

        }

        public TicketStatus getStatus(int statusId)
        {
            return ticketLookupRepo.getStatus(statusId);
        }

        public Employee getEmployee(int empId)
        {
            return empRepo.GetById(empId);
        }

        public List<Employee> getTeamMember(int teamId)
        {
            var sql = @"SELECT * 
                        FROM HR.EMPLOYEE EMP
	                        INNER JOIN TICKET.AGENT AT ON AT.EMP_ID = EMP.ID
	                        INNER JOIN TICKET.TEAM_AGENT_ASSIGN T ON T.AGENT_ID =AT.ID
                        WHERE T.TEAM_ID={0}
	                        AND AT.STATUS='ACTIVE'
	                        AND T.STATUS = 'ACTIVE' ";
            var rts = empRepo.SqlQuery<Employee>(string.Format(sql, teamId)).ToList();
            return rts;
        }

        public List<AgentInfo> getAgentInfoByTeam(int teamId)
        {

            var sql = @"SELECT AT.ID AgentId, 
	                    EMP.ID EmpId, 
	                    EMP.EMP_NO EmpNo, 
	                    EMP.DISPLAY_NAME EmpName, 
	                    EMP.EMAIL Email,
	                    GP.ASSIGNED_NOTIFY AssignedNotify,
	                    GP.CHANGE_STATUS_NOTIFY ChangeStatusNofify,
	                    GP.NEW_TICKET_NOTIFY NewTicketNotify,
	                    GP.REPLY_NOTIFY  ReplyNotify
                    FROM HR.EMPLOYEE EMP
                        INNER JOIN TICKET.AGENT AT ON AT.EMP_ID = EMP.ID
                        INNER JOIN TICKET.TEAM_AGENT_ASSIGN T ON T.AGENT_ID =AT.ID
                        INNER JOIN TICKET.GROUP_POLICY GP ON GP.ID = AT.GROUP_POLICY_ID
                    WHERE T.TEAM_ID={0}
                        AND AT.STATUS='ACTIVE'
                        AND T.STATUS = 'ACTIVE'";
            var rts = empRepo.SqlQuery<AgentInfo>(string.Format(sql, teamId)).ToList();

            return rts;
        }

        public List<TicketDepartment> getDepts()
        {
            throw new NotImplementedException();
        }

        public void saveMergedTicket(TicketMerged ticketMerged)
        {
            this.ticketMergedRepo.Add(ticketMerged);
        }

        public TicketMergeStatusDto getMergeInfo(int activityId)
        {
            var sql = @"
                SELECT 
                    T.ID TicketId,
	                CASE WHEN MT.ACTIVITY_ID = {0}
		                THEN 'Merged To' 
		                ELSE 'Merged From'
	                END Action,
	                T.TICKET_NO TicketNo,
	                T.SUBJECT Subject,
	                ST.STATUS Status
                FROM TICKET.MERGED_TICKET  MT
	                INNER JOIN TICKET.ACITVITY ACT ON ACT.ID = (CASE WHEN MT.ACTIVITY_ID = {0} THEN MT.TO_ACTIVITY_ID ELSE MT.ACTIVITY_ID  END)
	                INNER JOIN TICKET.TICKETING T ON T.ID = ACT.TICKET_ID
	                INNER JOIN TICKET.STATUS ST ON ST.ID = T.STATUS_ID
                WHERE 
	                MT.ACTIVITY_ID ={0} OR MT.TO_ACTIVITY_ID ={0}
            ";

            var reVal = ticketMergedRepo.SqlQuery<TicketMergeStatusDto>(string.Format(sql, activityId)).Single();
            return reVal;
            
        }

        public TicketPriority getPriority(int impactId, int urgencyId)
        {
            var sql = @"
                        SELECT P.ID Id, P.PRIORITY_NAME PriorityName, P.MODIFIED_DATE ModifiedDate, P.CREATED_DATE CreatedDate, P.DESCRIPTION description, P.SLA_ID SLAId
                        FROM TICKET.PRIORITY P
	                        INNER JOIN TICKET.PRIORITY_MAPPING PM ON P.ID = PM.PRIORITY_ID
                        WHERE PM.IMPACT_ID = {0} AND PM.URGENCY_ID = {1}
                    ";
            sql = string.Format(sql, impactId, urgencyId);
            var priority = ticketLookupRepo.SqlQuery<TicketPriority>(sql, impactId, urgencyId).Single();
            return priority;
        }

        public TicketSLA getSLAByPriority(int priorityId)
        {
            var sql = "SELECT SLA_ID   id FROM TICKET.PRIORITY WHERE ID=" + priorityId;
            var ret = ticketLookupRepo.SqlQuery<ResultId>(sql).Single();
            return slaRepo.GetById(ret.id);
            
        }

        public TicketSLA getSLAByPriorityAndTicketType(int priorityId, int ticketTypeId)
        {
            TicketSLA sla = getSla(ticketTypeId, priorityId);
            return slaRepo.GetById(sla.Id);
        }

        public TicketSLA getSla(int typeId, int priorityId)
        {
            var sql = @"SELECT P.ID Id
				, P.SLA_NAME SlaName
				, P.MODIFIED_DATE ModifiedDate
				, P.CREATED_DATE CreatedDate
				, P.[DESCRIPTION] Description
                , p.GRACE_PERIOD GracePeriod        
				, p.FIRST_RESPONSE_PERIOD FirstResponsePeriod        
				, p.STATUS Status
				FROM TICKET.SLA P
	            INNER JOIN TICKET.SLA_MAPPING SM ON P.ID = SM.SLA_ID
                WHERE ({0} = 0 OR SM.[TYPE_ID] = {0}) AND ({1} = 0 OR SM.PRIORITY_ID = {1})
                    ";
            sql = string.Format(sql, typeId, priorityId);
            return ticketLookupRepo.SqlQuery<TicketSLA>(sql, typeId, priorityId).Single();            
        }

        public IEnumerable<TicketSLA> getSlas(IEnumerable<int> typeId, IEnumerable<int> priorityId)
        {
            var typeIdStr = "0=0";
            if (typeId != null && typeId.Count() > 0)
            {
                typeIdStr = "SM.[TYPE_ID] IN ("+String.Join(",", typeId.ToArray())+")";
            }

            var priorityIdStr = "0=0";
            if (priorityId != null && priorityId.Count() > 0)
            {
                priorityIdStr = "SM.PRIORITY_ID IN (" + String.Join(",", priorityId.ToArray()) + ")";
            }

            var sql = @"SELECT P.ID Id
				, P.SLA_NAME SlaName
				, P.MODIFIED_DATE ModifiedDate
				, P.CREATED_DATE CreatedDate
				, P.[DESCRIPTION] Description
                , p.GRACE_PERIOD GracePeriod        
				, p.FIRST_RESPONSE_PERIOD FirstResponsePeriod        
				, p.STATUS Status
				FROM TICKET.SLA P
	            LEFT JOIN TICKET.SLA_MAPPING SM ON P.ID = SM.SLA_ID
                WHERE {0} AND {1}
                    ";
            sql = string.Format(sql, typeIdStr, priorityIdStr);
            return ticketLookupRepo.SqlQuery<TicketSLA>(sql).ToList();
        }


        /*
            When ticket listing change, it needs to be changed
        */
        public List<TicketItemDashboard> getTicketItemDashboard(TicketItemDashboard.TICKET_ITEM_DHB type, EmployeeDto emp)
        {

            var sql = @" 
                SELECT COALESCE({0}, 'UNKNOWN') ItemName,
	                SUM(CASE WHEN TK.STATUS_ID IN(1,2,4) and TK.LAST_ASSIGNED_EMP_ID !=0  and  ( TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL ) THEN 1 ELSE 0 END) Opened,
	                SUM(CASE WHEN TK.STATUS_ID IN(3) and TK.LAST_ASSIGNED_EMP_ID !=0  and  ( TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL )  THEN 1 ELSE 0 END) OnHold,
	                SUM(CASE WHEN tk.DUE_DATE<GETDATE() and TK.LAST_ASSIGNED_EMP_ID !=0 THEN 1 ELSE 0 END) OverDue,
	                SUM(CASE WHEN tk.LAST_ASSIGNED_EMP_ID =0   THEN 1 ELSE 0 END) UnAssigned
                FROM TICKET.TICKETING TK
	                INNER JOIN TICKET.ITEM IT ON TK.ITEM_ID = IT.ID
	                INNER JOIN TICKET.TEAM TM ON TK.LAST_ASSIGNED_TEAM_ID = TM.ID
	                INNER JOIN TICKET.SUB_CATEGORY SCATE ON SCATE.ID = IT.SUB_CATE_ID
	                INNER JOIN TICKET.CATEGORY CATE ON CATE.ID = SCATE.CATE_ID
	                LEFT JOIN TICKET.SOURCE SRC ON SRC.ID = TK.SOURCE_ID
	                LEFT JOIN HR.EMPLOYEE EMP ON EMP.ID = TK.LAST_ASSIGNED_EMP_ID
                WHERE TK.STATUS_ID NOT IN (5,6,7,8)
	                AND ( TK.LAST_ASSIGNED_EMP_ID = {1} OR  
		  	                EXISTS(
			                SELECT TOP 1 1 FROM TICKET.TEAM TM 
				                INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON TM.ID=AT.TEAM_ID
				                INNER JOIN TICKET.AGENT A ON A.ID = AT.AGENT_ID
				                INNER JOIN TICKET.GROUP_POLICY G ON G.ID = A.GROUP_POLICY_ID
			                WHERE TM.STATUS	='Active'
				                AND A.STATUS ='ACTIVE'
				                AND AT.STATUS ='ACTIVE'
				                AND A.EMP_ID = {1}  
				                AND (
                                    G.LIMIT_ACCESS = 'DEPT_TICKET'
                                    OR (G.LIMIT_ACCESS != 'OWN_TICKET'
                                        AND (G.LIMIT_ACCESS != 'TEAM_TICKET' OR AT.TEAM_ID = TK.LAST_ASSIGNED_TEAM_ID   )
                                        AND ( G.LIMIT_ACCESS !='CUSTOM' OR EXISTS(SELECT 1 FROM TICKET.TEAM_GROUP_ACCESS TG WHERE TG.GROUP_ACCESS_ID=G.ID AND TG.TEAM_ID=TK.LAST_ASSIGNED_TEAM_ID AND TG.STATUS='ACTIVE') )
                                        )
                                )
			                )
                        )
                GROUP BY {0}
            ";

            //AND AT.TEAM_ID = TK.LAST_ASSIGNED_TEAM_ID

            if (TicketItemDashboard.TICKET_ITEM_DHB.AGENT == type)
            {
                sql = string.Format(sql, "EMP.DISPLAY_NAME", emp.id);
            }
            else if (TicketItemDashboard.TICKET_ITEM_DHB.TEAM == type)
            {
                sql = string.Format(sql, "TM.TEAM_NAME", emp.id);
            }

            else if (TicketItemDashboard.TICKET_ITEM_DHB.CATE == type)
            {
                sql = string.Format(sql, "CATE.CATE_NAME", emp.id);
            }
            else if (TicketItemDashboard.TICKET_ITEM_DHB.SUB_CATE == type)
            {
                sql = string.Format(sql, "SCATE.SUB_CATE_NAME", emp.id);
            }

            else if (TicketItemDashboard.TICKET_ITEM_DHB.ITEM == type)
            {
                sql = string.Format(sql, "IT.ITEM_NAME", emp.id);
            }
            else if (TicketItemDashboard.TICKET_ITEM_DHB.SOURCE == type)
            {
                sql = string.Format(sql, "SRC.SOURCE", emp.id);
            }

            var result = ticketLookupRepo.SqlQuery<TicketItemDashboard>(sql).ToList();
            return result;
        }



        /*
            When ticket listing change, it needs to be changed
        */
        public List<HierarchyDashB> getItemPerformance(int type, HierarchyDashB.TIME_FRAME timeFrame, HierarchyDashB.TIME_FILTER timeFilter , EmployeeDto emp)
        {
            var sql = string.Empty;
            if (type == 1)
            {
                sql = @"
                    SELECT {0} [Time], 
                    cate.CATE_NAME + '==>' + SCATE.SUB_CATE_NAME + '==>' + it.ITEM_NAME KeyPath,
                    COUNT(*) Value
                    FROM TICKET.TICKETING TK
                        INNER JOIN TICKET.ITEM IT ON TK.ITEM_ID = IT.ID
                        INNER JOIN TICKET.TEAM TM ON TK.LAST_ASSIGNED_TEAM_ID = TM.ID
                        INNER JOIN TICKET.SUB_CATEGORY SCATE ON SCATE.ID = IT.SUB_CATE_ID
                        INNER JOIN TICKET.CATEGORY CATE ON CATE.ID = SCATE.CATE_ID
                        LEFT JOIN TICKET.STATUS ST ON ST.ID = TK.STATUS_ID
                    WHERE ( ST.STATE_ID!=3 OR TK.STATUS_ID IS NULL)
                        AND ( 
                                TK.LAST_ASSIGNED_EMP_ID = {2} OR  
		  	                    EXISTS(
			                    SELECT TOP 1 1 FROM TICKET.TEAM TM 
				                    INNER JOIN TICKET.TEAM_AGENT_ASSIGN AT ON TM.ID=AT.TEAM_ID
				                    INNER JOIN TICKET.AGENT A ON A.ID = AT.AGENT_ID
				                    INNER JOIN TICKET.GROUP_POLICY G ON G.ID = A.GROUP_POLICY_ID
			                    WHERE TM.STATUS	='Active'
				                    AND A.STATUS ='ACTIVE'
				                    AND AT.STATUS ='ACTIVE'
				                    AND A.EMP_ID = {2}  
				                    AND (
                                        G.LIMIT_ACCESS = 'DEPT_TICKET'
                                        OR (G.LIMIT_ACCESS != 'OWN_TICKET'
                                            AND (G.LIMIT_ACCESS != 'TEAM_TICKET' OR AT.TEAM_ID = TK.LAST_ASSIGNED_TEAM_ID   )
                                            AND ( G.LIMIT_ACCESS !='CUSTOM' OR EXISTS(SELECT 1 FROM TICKET.TEAM_GROUP_ACCESS TG WHERE TG.GROUP_ACCESS_ID=G.ID AND TG.TEAM_ID=TK.LAST_ASSIGNED_TEAM_ID AND TG.STATUS='ACTIVE') )
                                            )
                                    )
			                    )
                            )
                        AND {1}
                    GROUP BY
	                   {0},
	                   cate.CATE_NAME + '==>' + SCATE.SUB_CATE_NAME + '==>' + it.ITEM_NAME
                    order by {0}
                ";
            }

            var item = " CONVERT(VARCHAR(11),tk.CREATED_DATE,6) ";

            if (HierarchyDashB.TIME_FRAME.WEEKLY == timeFrame)
            {
                item = " DATENAME(year, tk.CREATED_DATE) + '-' +DATENAME(week, tk.CREATED_DATE) ";
            }else if (HierarchyDashB.TIME_FRAME.MONTHLY == timeFrame)
            {
                item = " DATENAME(year, tk.CREATED_DATE) + '-' + DATENAME(month, tk.CREATED_DATE) ";
            }
            else if (HierarchyDashB.TIME_FRAME.QUATERLY == timeFrame)
            {
                item = " DATENAME(year,tk.CREATED_DATE) +'-'+'Q'+ DATENAME(qq,tk.CREATED_DATE)  ";
            }
            else if (HierarchyDashB.TIME_FRAME.YEARLY == timeFrame)
            {
                item = "DATENAME(year,tk.CREATED_DATE)";
            }


            var filter = ""  ;
            if (HierarchyDashB.TIME_FILTER.CURR_MONTH == timeFilter)
            {
                filter = " YEAR(TK.CREATED_DATE) = YEAR(GETDATE()) AND  MONTH(TK.CREATED_DATE) = MONTH(GETDATE()) ";
            }
            else if (HierarchyDashB.TIME_FILTER.LAST_MONTH == timeFilter)
            {
                filter = "YEAR(TK.CREATED_DATE) = YEAR(DATEADD(MONTH, -1,GETDATE())) AND  MONTH(TK.CREATED_DATE) = MONTH( DATEADD(MONTH, -1,GETDATE())  ) ";
            }else if (HierarchyDashB.TIME_FILTER.LAST_MONTH_TODAY == timeFilter)
            {
                filter = "YEAR(TK.CREATED_DATE) = YEAR(DATEADD(MONTH, -1,GETDATE())) AND  MONTH(TK.CREATED_DATE) >= MONTH( DATEADD(MONTH, -1,GETDATE())  ) ";
            }



            else if (HierarchyDashB.TIME_FILTER.CURR_QUATER == timeFilter)
            {
                filter = " YEAR(TK.CREATED_DATE) = YEAR(GETDATE()) AND  DATEPART(Q,TK.CREATED_DATE) = DATEPART(Q,GETDATE())  ";
            }
            else if (HierarchyDashB.TIME_FILTER.LAST_QUATER == timeFilter)
            {
                filter = " YEAR(TK.CREATED_DATE) = YEAR(DATEADD(MONTH, -3,GETDATE())) AND DATEPART(Q,TK.CREATED_DATE) = DATEPART(Q,DATEADD(MONTH, -3,GETDATE()) ) ";
            }
            else if (HierarchyDashB.TIME_FILTER.LAST_QUATER_TODAY == timeFilter)
            {
                filter = "YEAR(TK.CREATED_DATE) = YEAR(DATEADD(MONTH, -3,GETDATE())) AND  DATEPART(Q,TK.CREATED_DATE) >=DATEPART(Q,DATEADD(MONTH, -3,GETDATE())  ) ";
            }


            else if (HierarchyDashB.TIME_FILTER.CURR_YEAR == timeFilter)
            {
                filter = "YEAR(TK.CREATED_DATE) = YEAR(GETDATE()) ";
            }
            else if (HierarchyDashB.TIME_FILTER.LAST_YEAR == timeFilter)
            {
                filter = "YEAR(TK.CREATED_DATE) = YEAR(GETDATE())-1 ";
            }
            else
            {
                throw new Exception(" Time filter is not supported");
            }


            sql = string.Format(sql, item, filter, emp.id );
            var result = ticketLookupRepo.SqlQuery<HierarchyDashB>(sql).ToList();
            return result;
        }

        public bool canAccessAsAgent(int agentId, int teamId)
        {

            try
            {
                var sql = @"
                        SELECT 1 id
                        FROM TICKET.AGENT AG
                        INNER JOIN TICKET.GROUP_POLICY GP ON AG.GROUP_POLICY_ID = GP.ID
                        LEFT JOIN TICKET.TEAM_AGENT_ASSIGN TA ON TA.AGENT_ID=AG.ID AND TA.STATUS ='ACTIVE' AND TA.TEAM_ID = {1}
                        WHERE 
	                        AG.ID= {0}
	                        AND (GP.LIMIT_ACCESS !='DEPT_TICKET' OR 1=1)
	                        AND GP.LIMIT_ACCESS !='OWN_TICKET'
	                        AND (GP.LIMIT_ACCESS !='TEAM_TICKET' OR  TA.TEAM_ID = {1})
	                        AND (GP.LIMIT_ACCESS !='CUSTOM' 
		                        OR ( 
			                        EXISTS(SELECT TOP 1 1 FROM TICKET.TEAM_GROUP_ACCESS  T WHERE T.TEAM_ID ={1} AND T.GROUP_ACCESS_ID=GP.ID  AND T.STATUS='ACTIVE' )
		                        )
	                        )

                    ";
                sql = string.Format(sql, agentId, teamId);
                var re=  ticketActivityRepo.SqlQuery<ResultId>(sql).Single();
                if (re.id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public void saveSubTicketLink(TicketSubTkLink ticketSubTkLink)
        {
            ticketSubTkLinkRepo.Add(ticketSubTkLink);
        }

        public TicketListing getSubTitket(int activityId)
        {
            var sql = @"
                SELECT TK.ID Id, 
                TK.TICKET_NO TicketNo, 
                TK.SUBJECT Subject, 
                TK.CREATED_DATE CreatedDate,
                ISNULL(EMP.DISPLAY_NAME,'None Emp') Requestor, 
                EMP1.DISPLAY_NAME Assignee, 
                TK.DUE_DATE DueDate, 
                ST.STATUS Status, 
                ST.ID StatusId,
                s.STATE State, 
                PR.PRIORITY_NAME Priority, 
                TM.TEAM_NAME TeamName, 
                TK.LAST_ACTION LastAction, 
                TK.LAST_ACTION_DATE LastActionDate, 
                emp.DISPLAY_NAME LastActionBy, 
                TK.WAIT_ACTIONED_BY WaitActionedBy ,
                TK.ACTUAL_MINUTES ActualMinutes,
                CASE 
                    WHEN ST.STATE_ID = 3 THEN 'Removed' 
                    WHEN ST.STATE_ID = 2 THEN 'Closed'
                    WHEN TK.STATUS_ID IN(1,2,4) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'Opened'
                    WHEN TK.STATUS_ID IN(3) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'OnHold'
                    WHEN TK.DUE_DATE<GETDATE() and TK.LAST_ASSIGNED_EMP_ID !=0 THEN 'OverDue'
                    WHEN TK.LAST_ASSIGNED_EMP_ID =0 THEN 'UnAssigned'
                END StatusFlag
            FROM TICKET.ACITVITY ACT
                INNER JOIN TICKET.SUB_TICKET_LINK TL ON ACT.ID = TL.ACTIVITY_ID
                INNER JOIN TICKET.ACITVITY ACT1 ON ACT1.ID = TL.SUB_TICKET_ACT_ID
                INNER JOIN TICKET.TICKETING TK ON TK.ID =ACT1.TICKET_ID
                LEFT JOIN HR.EMPLOYEE EMP ON TK.REQUESTOR_ID = EMP.ID 
                INNER JOIN TICKET.STATUS ST ON ST.ID = TK.STATUS_ID 
                INNER JOIN TICKET.STATE S ON S.ID = ST.STATE_ID 
                LEFT JOIN TICKET.PRIORITY PR ON PR.ID = TK.PRIORITY_ID 
                LEFT JOIN HR.EMPLOYEE EMP1 ON TK.LAST_ASSIGNED_EMP_ID = EMP1.ID 
                LEFT JOIN TICKET.TEAM TM ON TM.ID = tk.LAST_ASSIGNED_TEAM_ID 
                INNER JOIN HR.EMPLOYEE EMP2 ON TK.LAST_ACTION_BY = EMP2.ID 
            WHERE 
                ACT.ACTIVITY_TYPE = 'SUB_TICKET_POSTING'
                AND ACT.ID ={0}
                ";
            sql = string.Format(sql, activityId);
            try
            {
                var result = ticketActivityRepo.SqlQuery<TicketListing>(sql).Single();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public TicketListing getMainTicket(int activityId)
        {
            var sql = @"
               SELECT TK.ID Id, 
                    TK.TICKET_NO TicketNo, 
                    TK.SUBJECT Subject, 
                    TK.CREATED_DATE CreatedDate,
                    ISNULL(EMP.DISPLAY_NAME,'None Emp') Requestor, 
                    EMP1.DISPLAY_NAME Assignee, 
                    TK.DUE_DATE DueDate, 
                    ST.STATUS Status, 
                    ST.ID StatusId,
                    s.STATE State, 
                    PR.PRIORITY_NAME Priority, 
                    TM.TEAM_NAME TeamName, 
                    TK.LAST_ACTION LastAction, 
                    TK.LAST_ACTION_DATE LastActionDate, 
                    emp.DISPLAY_NAME LastActionBy, 
                    TK.WAIT_ACTIONED_BY WaitActionedBy ,
                    TK.ACTUAL_MINUTES ActualMinutes,
                    CASE 
                        WHEN ST.STATE_ID = 3 THEN 'Removed' 
                        WHEN ST.STATE_ID = 2 THEN 'Closed'
                        WHEN TK.STATUS_ID IN(1,2,4) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'Opened'
                        WHEN TK.STATUS_ID IN(3) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'OnHold'
                        WHEN TK.DUE_DATE<GETDATE() and TK.LAST_ASSIGNED_EMP_ID !=0 THEN 'OverDue'
                        WHEN TK.LAST_ASSIGNED_EMP_ID =0 THEN 'UnAssigned'
                    END StatusFlag
                FROM TICKET.ACITVITY ACT
                    INNER JOIN TICKET.SUB_TICKET_LINK TL ON ACT.ID = TL.SUB_TICKET_ACT_ID
                    INNER JOIN TICKET.ACITVITY ACT1 ON ACT1.ID = TL.ACTIVITY_ID
                    INNER JOIN TICKET.TICKETING TK ON TK.ID =ACT1.TICKET_ID
                    LEFT JOIN HR.EMPLOYEE EMP ON TK.REQUESTOR_ID = EMP.ID 
                    INNER JOIN TICKET.STATUS ST ON ST.ID = TK.STATUS_ID 
                    INNER JOIN TICKET.STATE S ON S.ID = ST.STATE_ID 
                    LEFT JOIN TICKET.PRIORITY PR ON PR.ID = TK.PRIORITY_ID 
                    LEFT JOIN HR.EMPLOYEE EMP1 ON TK.LAST_ASSIGNED_EMP_ID = EMP1.ID 
                    LEFT JOIN TICKET.TEAM TM ON TM.ID = tk.LAST_ASSIGNED_TEAM_ID 
                    INNER JOIN HR.EMPLOYEE EMP2 ON TK.LAST_ACTION_BY = EMP2.ID 
                WHERE 
	                ACT.ACTIVITY_TYPE = 'TICKET_POSTING'
	                AND ACT.ID ={0}
                ";
            sql = string.Format(sql, activityId);
            try
            {
                var result = ticketActivityRepo.SqlQuery<TicketListing>(sql).Single();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TicketListing> GetSubtickets(int ticketId)
        {
            var sql = @"
                    SELECT TK.ID Id, 
                        TK.TICKET_NO TicketNo, 
                        TK.SUBJECT Subject, 
                        TK.CREATED_DATE CreatedDate,
                        ISNULL(EMP.DISPLAY_NAME,'None Emp') Requestor, 
                        EMP1.DISPLAY_NAME Assignee, 
                        TK.DUE_DATE DueDate, 
                        ST.STATUS Status, 
                        ST.ID StatusId,
                        s.STATE State, 
                        PR.PRIORITY_NAME Priority, 
                        TM.TEAM_NAME TeamName, 
                        TK.LAST_ACTION LastAction, 
                        TK.LAST_ACTION_DATE LastActionDate, 
                        emp.DISPLAY_NAME LastActionBy, 
                        TK.WAIT_ACTIONED_BY WaitActionedBy ,
                        TK.ACTUAL_MINUTES ActualMinutes,
                        CASE 
                            WHEN ST.STATE_ID = 3 THEN 'Removed' 
                            WHEN ST.STATE_ID = 2 THEN 'Closed'
                            WHEN TK.STATUS_ID IN(1,2,4) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'Opened'
                            WHEN TK.STATUS_ID IN(3) and TK.LAST_ASSIGNED_EMP_ID !=0 and (TK.DUE_DATE>GETDATE() OR TK.DUE_DATE IS NULL) THEN 'OnHold'
                            WHEN TK.DUE_DATE<GETDATE() and TK.LAST_ASSIGNED_EMP_ID !=0 THEN 'OverDue'
                            WHEN TK.LAST_ASSIGNED_EMP_ID =0 THEN 'UnAssigned'
                        END StatusFlag
                    FROM TICKET.ACITVITY ACT
                        INNER JOIN TICKET.SUB_TICKET_LINK TL ON ACT.ID = TL.ACTIVITY_ID
                        INNER JOIN TICKET.ACITVITY ACT1 ON ACT1.ID = TL.SUB_TICKET_ACT_ID
                        INNER JOIN TICKET.TICKETING TK ON TK.ID =ACT1.TICKET_ID
                        LEFT JOIN HR.EMPLOYEE EMP ON TK.REQUESTOR_ID = EMP.ID 
                        INNER JOIN TICKET.STATUS ST ON ST.ID = TK.STATUS_ID 
                        INNER JOIN TICKET.STATE S ON S.ID = ST.STATE_ID 
                        LEFT JOIN TICKET.PRIORITY PR ON PR.ID = TK.PRIORITY_ID 
                        LEFT JOIN HR.EMPLOYEE EMP1 ON TK.LAST_ASSIGNED_EMP_ID = EMP1.ID 
                        LEFT JOIN TICKET.TEAM TM ON TM.ID = tk.LAST_ASSIGNED_TEAM_ID 
                        INNER JOIN HR.EMPLOYEE EMP2 ON TK.LAST_ACTION_BY = EMP2.ID 
                    WHERE 
                        ACT.ACTIVITY_TYPE = 'SUB_TICKET_POSTING'
                        AND ACT.TICKET_ID ={0}
                        ";
            sql = string.Format(sql, ticketId);
            var result = ticketRepository.SqlQuery<TicketListing>(sql).ToList();
            return result;
        }

        public bool noActiveSubticket(int mainTicketId)
        {
            var sql = @"
                    SELECT TOP 1 1 id
                    FROM TICKET.SUB_TICKET_LINK SL 
	                    INNER JOIN TICKET.ACITVITY ACT ON SL.ACTIVITY_ID = ACT.ID
	                    INNER JOIN TICKET.ACITVITY ACT1 ON SL.SUB_TICKET_ACT_ID =ACT1.ID
	                    INNER JOIN TICKET.TICKETING TK ON TK.ID = ACT1.TICKET_ID
	                    INNER JOIN TICKET.STATUS ST ON ST.ID = TK.STATUS_ID
	                    INNER JOIN TICKET.STATE SA ON SA.ID = ST.STATE_ID
                    WHERE ACT.ACTIVITY_TYPE ='SUB_TICKET_POSTING'
	                    AND SA.ID =1
	                    AND ACT.TICKET_ID = {0}
                    ";
            sql = string.Format(sql, mainTicketId);
            try
            {
                var result = ticketActivityRepo.SqlQuery<TicketListing>(sql).Single();
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void saveNotificatin(TicketNotification notification)
        {
            ticketNotificationRepo.Add(notification);
        }

        public int getUnreadNotify(EmployeeDto emp)
        {
            var sql = "SELECT COUNT(*) id FROM  [TICKET].[NOTIFICATION]  where EMP_ID = {0} and STATUS = 'UNREAD' ";
            sql = string.Format(sql, emp.id);
            try
            {
                var result = ticketActivityRepo.SqlQuery<ResultId>(sql).Single();
                return result.id;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public List<TKNotifyDto> getNotificationList(EmployeeDto emp)
        {
            var sql = @"
                    SELECT 
                        NT.ID NotifyId,
	                    NT.ACTIVITY_ID ActivityId,
	                    ACT.ACTIVITY_TYPE ActivityType, 
	                    ACTT.ACT_TYPE_NAME ActivityName, 
	                    NT.EMP_ID UserId,
	                    EMP.DISPLAY_NAME UserName,
	                    NT.STATUS Status,
                        NT.CREATED_DATE CreatedDate
                    FROM [TICKET].[NOTIFICATION] NT
	                    INNER JOIN TICKET.ACITVITY ACT ON NT.ACTIVITY_ID = ACT.ID
	                    INNER JOIN TICKET.ACTIVITY_TYPE ACTT ON ACT.ACTIVITY_TYPE = ACTT.CODE
	                    INNER JOIN HR.EMPLOYEE EMP ON EMP.ID = NT.EMP_ID
                    WHERE NT.EMP_ID = {0} AND NT.STATUS = 'UNREAD'	 
                    ORDER BY NT.CREATED_DATE DESC ";
            sql = string.Format(sql, emp.id);
            try
            {
                var result = ticketActivityRepo.SqlQuery<TKNotifyDto>(sql).ToList();
                return result;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public void markAsRead(int notifyId)
        {
            var notify = ticketNotificationRepo.GetById(notifyId);
            if (!notify.Status.Equals("READ"))
            {
                notify.Status = "READ";
                ticketNotificationRepo.Update(notify);
            }
            
        }

        public void saveTicketNoneReqEmp(TicketNoneReqEmp ticketNoneReqEmp)
        {
            this.ticketNoneReqEmpsRepo.Add(ticketNoneReqEmp);
        }

        public Employee getUnknowRequestor(Ticket ticket)
        {
            try
            {
                var item = ticketNoneReqEmpsRepo.Get(t => t.TicketId == ticket.Id);
                if (item == null)
                {
                    throw new Exception(string.Format("Cannot find unknow employee request ticket {0}", ticket.TicketNo));
                }
                var emp = new Employee()
                {
                    Id = -1,
                    Email = item.Originator,
                    EmpNo = "Unknown",
                    DisplayName = item.Originator
                };

                return emp;
            }catch(Exception e)
            {
                throw e;
            }
            
        }

        public bool isTeamMenber(int teamId, int agentId)
        {
            try
            {
                var sql = @"
                        SELECT 1 id
                        FROM TICKET.TEAM_AGENT_ASSIGN
                        WHERE TEAM_ID={0}
	                        AND AGENT_ID={1} 
	                        AND STATUS ='ACTIVE'
                    ";
                sql = string.Format(sql, teamId, agentId);
                var resultId = ticketActivityRepo.SqlQuery<ResultId>(sql).Single();
                return (resultId != null);
            }
            catch (Exception)
            {
                return false;
            }
            

            throw new NotImplementedException();
        }

        public TicketDepartment getDept(int deptId)
        {
            try
            {
                return ticketDeptRepo.GetById(deptId);
            }
            catch
            {
                return null;
            }

            
        }

        public MailList getMailConfig(string email)
        {
            return mailListRepo.Get(t => t.EmailAddress.Equals(email));
        }

        public FormIntegratedDto getFormIntegrated(int ticketId)
        {
            try
            {
                var sql = @"
                   SELECT RH.ID Id, RH.TITLE  FormNo, RA.NONE_SMART_FORM_URL Url, RH.PROCESS_INSTANCE_ID PNo,CAST (1 AS bit) FormIntegrated 
                    FROM BPMDATA.REQUEST_HEADER RH
	                    INNER JOIN BPMDATA.REQUEST_APPLICATION RA ON RH.REQUEST_CODE=RA.REQUEST_CODE 
	                    INNER JOIN TICKET.E_FORM_INTERGRATED FI ON FI.REQUEST_HEADER_ID=RH.ID
                    WHERE  FI.TICKET_ID  = {0}
                ";

                sql = string.Format(sql, ticketId);
                return formIntegratedRepo.SqlQuery<FormIntegratedDto>(sql).Single();
            }
            catch
            {
                return null;
            }
            
        }

        public ProcInst GetWorkListItem(int ticketId, string loginUser)
        {
            try
            {
                var sql = @"
                    SELECT TOP(100)
	                    RH.PROCESS_INSTANCE_ID ProcInstId, 
	                    RH.ID RequestHeaderId, RH.REQUEST_CODE RequestCode, 
	                    RH.LAST_ACTIVITY LastActivity 
                    FROM TICKET.E_FORM_INTERGRATED EI
                    INNER JOIN BPMDATA.REQUEST_HEADER RH ON EI.REQUEST_HEADER_ID  = RH.ID
                    WHERE EI.TICKET_ID = {0}
                ";

                sql = string.Format(sql, ticketId);
                var result = ticketActivityRepo.SqlQuery<ResultRequestHeader>(sql).Single();

                var provider = new ProcInstProvider(loginUser);
                var task = provider.OpenWorklistItem(result.ProcInstId);
                task.RequestHeaderId = result.RequestHeaderId;
                task.requestCode = result.RequestCode;
                task.ActivityName = result.LastActivity;
                return task;
            }
            catch
            {
                return null;
            }
        }

        public object GetEmailItem(int? id)
        {
            var item = mailItemRepo.Get(p => p.Id == id);
            if(item != null)
            {
                string pattern = @"k2admin\@nagaworld\.com;|itservicedesk\@nagaworld\.com;|k2service\@nagaworld\.com;|k2admin\@nagaworld\.com|itservicedesk\@nagaworld\.com|k2service\@nagaworld\.com";
                string to = item.Receipient + ";" + item.Originator;
                to = Regex.Replace(to, pattern, string.Empty);
                string cc = Regex.Replace(item.Cc, pattern, string.Empty);
                var nItem = new
                {
                    cc = cc,
                    to = to
                };
                return nItem;

            }
            return item;
        }

        public string GetITFormContent(int id)
        {
            string content = string.Empty;
            string itemsHtml = string.Empty;
            var items = requestItemRepository.GetMany(p => p.RequestHeaderId == id);

            if(items != null && items.Count() > 0)
            {
                items.Each(i =>
                {
                    string tr = string.Format(@"<tr><td>{0}</td>
                                                <td>{1}</td>
                                                <td>{2}</td>
                                                <td>{3}</td>
                                                <td>{4}</td>
                                                <td>{5}</td></tr>",
                                                i.Item.Session != null ? i.Item.Session.SessionName : "",
                                                i.Item != null ? i.Item.ItemName : "",
                                                i.ItemType != null ? i.ItemType.ItemTypeName : "",
                                                i.ItemRole != null ? i.ItemRole.RoleName : "",
                                                i.Qty,
                                                i.Comment);

                    itemsHtml += tr;
                });
                return GetHtml(itemsHtml);
            } else
            {
                return "Ticket posted by K2 form integration. For more detail, please go to the form";
            }            
        }

        public string GetHtml(string items)
        {
            string html = @"<!DOCTYPE html>
                            <html>
                            <head>
                            <style>
                            table {
                                border-collapse: collapse;
                                width: 100%;
                            }

                            th, td {
                                text-align: left;
                                padding: 8px;
                            }

                            tr:nth-child(even){background-color: #f2f2f2}

                            th {
                                background-color: #636f80;
                                color: white;
                            }
                            </style>
                            </head>
                            <body>
Ticket posted by K2 form integration. For more detail, please go to the form                                                          
                            <br/>
                            <br/>
                             <table>
                              <tr>
                                <th>SESSION</th>
                                <th>ITEM NAME</th>
                                <th>ITEM TYPE</th>
                                <th>ROLE</th>
                                <th>QTY</th>
                                <th>COMMENT</th>
                              </tr>
                              " + items + @"                              
                            </table>
                            </body>
                            </html>";
            return html;
        }


        #endregion
    }
    class ResultId
    {
        public int id { get; set; }
        public int id1 { get; set; }
    }

    class ResultRequestHeader
    {
        public int ProcInstId { get; set; }
        public int RequestHeaderId { get; set; }
        public string RequestCode { get; set; }
        public string LastActivity { get; set; }
    }

    class ResultString
    {
        public string description { get; set; }
    }
}
