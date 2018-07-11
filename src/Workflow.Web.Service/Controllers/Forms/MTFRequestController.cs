using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Workflow.Business;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities.MTF;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.MTFRequestForm;
using Workflow.DataObject.MTF;
using Workflow.Core.Attributes;
using Workflow.DataAcess.Repositories;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/mtfrequest")]
    public class MTFRequestController : AbstractServiceController<MTFRequestWorkflowInstance, MTFRequestFormViewModel>
    {
        Repository _repo = new Repository();

        public MTFRequestController()
        {
            
        }

        #region Business Form Logic
        protected override void MoreMapDataBC(MTFRequestFormViewModel viewData, MTFRequestWorkflowInstance workflowInstance)
        {
            // Treatment Form Base
            workflowInstance.Treatment = viewData.dataItem.treatment.TypeAs<Treatment>();

            // Medicine Precription - Collection
            workflowInstance.AddPrescriptions = (from p in viewData.dataItem.addPrescriptions select p.TypeAs<Prescription>());
            workflowInstance.DelPrescriptions = (from p in viewData.dataItem.delPrescriptions select p.TypeAs<Prescription>());
            workflowInstance.EditPrescriptions = (from p in viewData.dataItem.editPrescriptions select p.TypeAs<Prescription>());

            // Unfit To Work - Collection
            workflowInstance.AddUnfitToWorks = (from p in viewData.dataItem.addUnfitToWorks select p.TypeAs<UnfitToWork>());
            workflowInstance.DelUnfitToWorks = (from p in viewData.dataItem.delUnfitToWorks select p.TypeAs<UnfitToWork>());
            workflowInstance.EditUnfitToWorks = (from p in viewData.dataItem.editUnfitToWorks select p.TypeAs<UnfitToWork>());
        }

        protected override void MoreMapDataView(MTFRequestWorkflowInstance workflowInstance, MTFRequestFormViewModel viewData)
        {

            // Bind Treament data to view model
            viewData.dataItem.treatment = workflowInstance.Treatment.TypeAs<TreatmentViewModel>();

            // Cast and bind Medicine Precription data to view model
            if (workflowInstance.Prescriptions != null && workflowInstance.Prescriptions.Count() > 0)
            {
                var medicines = new LookupService().GetLookups<Medicine>();
                viewData.dataItem.prescriptions = (from p in workflowInstance.Prescriptions
                                                   join t in medicines on p.MedicineId equals t.Id
                                                   select new PrescriptionViewModel()
                                                   {
                                                       Id = p.Id,
                                                       Qty = p.Qty,
                                                       Usage = p.Usage,
                                                       TreatmentId = p.TreatmentId,
                                                       MedicineId = p.MedicineId,
                                                       Medicine = string.Concat(t.ItemCode, " - ", t.Description)
                                                   });
            }

            // Cast and bind Unfit To Work data list to view model
            if (workflowInstance.UnfitToWorks != null && workflowInstance.UnfitToWorks.Count() > 0)
            {
                viewData.dataItem.unfitToWorks = (from p in workflowInstance.UnfitToWorks select p.TypeAs<UnfitToWorkViewModel>());
            }
        } 
        #endregion

        #region Constructor
        // Initialize constructor of view model
        protected override MTFRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new MTFRequestFormViewModel();
        }

        protected override IRequestFormService<MTFRequestWorkflowInstance> GetRequestformService()
        {
            return new MTFRequestFormService();
        }
        #endregion

        [Route("resend-notification")]
        public HttpResponseMessage ResendNotificationEmail(int requestorId)
        {
            string sqlMailingList = @"EXEC [BPMDATA].[GET_EMAILS_BY_REQ]
                                    @requestID = {0},
                                    @requestCode = 'MT_REQ',
                                    @roleCode = 'FYI',
                                    @participantBased = 2,
                                    @returnString = 0,
                                    @seperator = ',',
	                                @package = 'BPMDATA.DEPT_APPROVAL_ROLE.ID.MTF.FYI'";

            var result = _repo.ExecDynamicSqlQuery(string.Format(sqlMailingList, requestorId));
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("unfit-towork-list")]
        public HttpResponseMessage GetUnfitToWorkList(int requestorId, int requestId) {
            var result = _repo.ExecDynamicSqlQuery(string.Format(@"
            SELECT U.*, H.PROCESS_INSTANCE_ID, H.TITLE FROM HR.UNFIT_TO_WORK U
            INNER JOIN BPMDATA.REQUEST_HEADER H ON H.ID = U.REQUEST_ID
            WHERE H.REQUESTOR = {0} AND H.ID NOT IN ({1}) AND H.[STATUS] IN ('Done', 'Edit')", requestorId, requestId));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route("patient-record")]
        public HttpResponseMessage GetPatientRecord(int empId) {
            
            try
            {
                string sql = @"SELECT 
	                        H.PROCESS_INSTANCE_ID ID,
	                        H.TITLE FOLIO,
	                        (ISNULL(P.EMP_NO,'') + ' - ' + ISNULL(P.DISPLAY_NAME, '')) PATIENT,
	                        T.SYMPTOM,
	                        T.DIAGNOSIS,
	                        H.SUBMITTED_DATE,
	                        (ISNULL(D.EMP_NO,'') + ' - ' + ISNULL(D.DISPLAY_NAME, '')) TREATMENT_BY,
	                        H.LAST_ACTION_DATE
                        FROM 
                        [BPMDATA].REQUEST_HEADER H 
                        INNER JOIN [HR].[TREATMENT] T ON T.REQUEST_HEADER_ID = H.ID
                        INNER JOIN [HR].[EMPLOYEE] P ON P.ID = H.REQUESTOR
                        LEFT JOIN [HR].[EMPLOYEE] D ON D.LOGIN_NAME = REPLACE(H.LAST_ACTION_BY, 'K2:', '')
                        WHERE H.REQUEST_CODE = 'MT_REQ' AND P.ID = {0} AND H.[STATUS] = 'Done'
						ORDER BY H.SUBMITTED_DATE DESC";
                var result = SqlQuery<PatientRecordView>(string.Format(sql, empId));
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }
        
        [Route("patient-list")]
        public HttpResponseMessage GetMyPatient(string state)
        {
            try
            {
                if (state == "PENDING_HOD")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, SqlQuery<PatientView>(
                            string.Format(@"SELECT (E.EMP_NO + ' - ' + E.DISPLAY_NAME) PATIENT, H.TITLE FOLIO, H.LAST_ACTION_DATE
                            FROM BPMDATA.REQUEST_HEADER H 
                            INNER JOIN HR.EMPLOYEE E ON E.ID = H.REQUESTOR
                            WHERE H.REQUEST_CODE = 'MT_REQ' AND
                                H.LAST_ACTIVITY IN ('Submission', 'Requestor Rework') AND 
                                H.[STATUS] IN ('Submitted', 'Resubmitted') AND
                                H.PROCESS_INSTANCE_ID > 0 AND 
								H.SUBMITTED_DATE > DATEADD(HOUR, -12, GETDATE())
                            ORDER BY H.LAST_ACTION_DATE DESC")
                    ));
                   
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, SqlQuery<CheckInView>(
                               string.Format(@"SELECT P.* FROM [QUEUE].[V_PATIENT] P 
                                WHERE QUEUE_STATUS = '{0}' AND P.SUBMITTED_DATE > DATEADD(HOUR, -12, GETDATE()) 
                                ORDER BY [PRIORITY], [LAST_MODIFIED_DATE] ASC", state)
                   ));
                }
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }
        
        [Route("patient-dashboard-piechart")]
        public HttpResponseMessage GetGraphPatientPie()
        {
            try
            {
                var result = SqlQuery<PieView>(@"EXEC [GRAPH].[MTF_DASHBOARD_PIE]");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }

        [Route("patient-dashboard-linechart")]
        public HttpResponseMessage GetGraphPatientLine(string datePath = "MONTH")
        {
            try
            {
                string sql = "EXEC [GRAPH].[MTF_DASHBOARD_LINE] @DATEPATH = '{0}' ";
                var result = SqlQuery<GraphView>(string.Format(sql, datePath));

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }

        [Route("patient-dashboard-columnchart")]
        public HttpResponseMessage GetGraphPatientColumn(string datePath = "MONTH")
        {
            try
            {
                string sql = "EXEC [GRAPH].[MTF_DASHBOARD_COLUMN] @DATEPATH = '{0}' ";
                var result = SqlQuery<GraphView>(string.Format(sql, datePath));

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }

        [Route("patient-dashboard-utw")]
        public HttpResponseMessage GetUnfitToWork()
        {
            try { 
                return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
                        string.Format(@"SELECT TOP(100)
	                    H.TITLE FOLIO, 
	                    T.WORK_SHIFT,
	                    (E.EMP_NO + ' - ' + E.DISPLAY_NAME) PATIENT,
	                    E.JOB_TITLE POSITION,
	                    D.FULL_DEPT_NAME DEPARTMENT,
	                    H.LAST_ACTION_DATE,
	                    (SELECT TOP(1) DOC.DISPLAY_NAME FROM HR.EMPLOYEE DOC 
	                    WHERE DOC.LOGIN_NAME = REPLACE(H.LAST_ACTION_BY, 'K2:', '')) LAST_ACTION_BY
                    FROM [BPMDATA].[REQUEST_HEADER] H 
                    INNER JOIN HR.[TREATMENT] T ON T.REQUEST_HEADER_ID = H.ID
                    INNER JOIN HR.[EMPLOYEE] E ON E.ID = H.REQUESTOR
                    INNER JOIN HR.[VIEW_DEPARTMENT] D ON D.TEAM_ID = E.DEPT_ID
                    /*WHERE H.SUBMITTED_DATE > DATEADD(HOUR, -12, GETDATE())*/ ")
                ));
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }
    }
}