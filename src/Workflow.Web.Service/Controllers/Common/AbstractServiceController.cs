/**
*@author : Phanny
*/

using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.DataObject.Security;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Models;
using Workflow.Web.Models;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;

namespace Workflow.Web.Service.Controllers.Common
{
    public abstract class AbstractServiceController<T, E>: BaseController
        where T : AbstractWorkflowInstance 
        where E : AbstractFormDataViewModel
    {
        private Type _attachmentType;
        private IAttachmentService _attachmentService;
        private IEmployeeService _employeeService;
        private IRequestFormService<T> _requestFormService = null;
        private Repository<ActivityHistoryViewModel> _actHist;
        private int RequestHeaderId { get; set; }
        
        public AbstractServiceController() {
            _attachmentService = new AttachmentService();
            _employeeService = new EmployeeService();
            _actHist = new Repository<ActivityHistoryViewModel>();

            if (_requestFormService == null)
            {
                _requestFormService = GetRequestformService();
            }
        }

        public HttpResponseMessage Get()
        {
            return Get(RequestContext.Url.Request.RequestUri.LocalPath.Split('/').Last());
        }

        public HttpResponseMessage Get(string serial)
        {
            try
            {
                //string managedUser = null;
                //if (!string.IsNullOrEmpty(sharedUser))
                //{
                //    sharedUser = sharedUser.GetLoginWithoutLabel();
                //    managedUser = RequestContext.Principal.Identity.Name.GetLoginWithoutLabel();
                //}
                
                var instance = MapDataBC(serial, null, null);
                var data = this.MapDataView(_requestFormService.GetRequestInstanceData(instance));

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                var _logger = LogManager.GetLogger("FORM");
                _logger.Error(e.ToAllMessages());
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.GetBaseException().Message);
            }
        }

        public HttpResponseMessage Post([FromBody] E value)
        {
            //return Request.CreateResponse(HttpStatusCode.OK, "OSHA - Submit");
            try
            {
                string responseText = _requestFormService.TakeAction(this.MapDataBC(value));
                var message = JsonConvert.DeserializeObject<ResponseText>(responseText);
                return Request.CreateResponse(HttpStatusCode.OK, message);

            }
            catch (Exception e)
            {
                var _logger = LogManager.GetLogger("FORM");
                _logger.Error(e.ToAllMessages());
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.ToAllMessages());
            }
        }
        
        protected E MapDataView(T workflowInstance)
        {
            RequestHeaderId = workflowInstance.RequestHeaderId;
            IEmployeeService employeeService = new EmployeeService();
            var viewData = CreateNewFormDataViewModel();
            viewData.requestHeaderId = workflowInstance.RequestHeaderId;
            viewData.requestNo = workflowInstance.RequestNo; 
            viewData.serial = workflowInstance.SerialNo;
            viewData.activity = workflowInstance.Activity;
            viewData.state = workflowInstance.State;
            viewData.actions = workflowInstance.Actions;
            viewData.priority = workflowInstance.Priority;
            viewData.lastActivity = workflowInstance.LastActivity;
            viewData.status = workflowInstance.Status;
            
            EmployeeDto requestor = employeeService.GetRequestor(RequestHeaderId, workflowInstance.Requestor.Id);
            RequestorViewModel requestorVM = new RequestorViewModel(){
                id          = requestor.id,
                employeeNo  = requestor.employeeNo,
                fullName    = requestor.fullName,
                position    = requestor.position,
                email       = requestor.email,
                phone       = requestor.phone,
                ext         = requestor.ext,
                deptName    = requestor.deptName,
                subDept     = requestor.subDept,
                devision    = requestor.devision,
                groupName   = requestor.groupName,
                hod         = requestor.hod
            };

            viewData.requestor = requestorVM;

            viewData.activities = _actHist.SqlQuery(string.Format(@"SELECT 
	            C.ACTIVITY activity, 
	            C.APPROVAL_DATE actionDate,
	            ISNULL(SL.[User],C.APPROVER) approver,
	            ISNULL(E.DISPLAY_NAME, C.APPROVER_DISPLAY_NAME) appriverDisplayName, 
	            C.DECISION decision, 
	            C.COMMENTS comment
            FROM BPMDATA.APPROVAL_COMMENT C
            LEFT JOIN [BPMDATA].[REQUEST_HEADER] H ON H.ID = C.REQUEST_HEADER_ID
            LEFT JOIN [K2].[ServerLog].[ActInstSlot] SL ON SL.ActInstID = C.ACT_INST_ID AND SL.ProcInstID = H.PROCESS_INSTANCE_ID
            LEFT JOIN HR.EMPLOYEE E ON E.LOGIN_NAME = REPLACE(SL.[User], 'K2:', '')
            WHERE C.REQUEST_HEADER_ID = {0}
            ORDER BY C.APPROVAL_DATE", RequestHeaderId)); 
            //Parse(workflowInstance.ActivityHistories);

            viewData.comment = workflowInstance.Comment;
            viewData.fileUploads = new FileUploadItemsViewModel(){
                allItems = ParseFileAttachements(workflowInstance.UploadFiles)
            };

            MoreMapDataView(workflowInstance, viewData);
            viewData.acl = GetPropsAcl();
            return viewData;
        }

        protected IList<AccessControl> GetPropsAcl()
        {
             return new PreviewService().GetFormAcl(
                RequestHeaderId,
                RequestContext.Principal.Identity.Name
            );
        }

        protected IList<EmployeeModel> LoadEmployeeList()
        {
            return new Repository().ExecSqlQuery<EmployeeModel>(@"SELECT 
	            E.Id, 
	            (E.[EMP_NO] + ' - ' + E.[DISPLAY_NAME]) [Value],
	            E.[EMP_NO] EmployeeNo,
	            E.[DISPLAY_NAME] EmployeeName,
	            D.FULL_DEPT_NAME Department,
	            E.JOB_TITLE Position
            FROM [HR].[EMPLOYEE] E INNER JOIN [HR].[VIEW_DEPARTMENT] D ON D.TEAM_ID = E.DEPT_ID
            ORDER BY [DISPLAY_NAME] ");
        }

        protected  IEnumerable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters) where TEntity : class {            
            var dbContext = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow).init();
            return dbContext.Database.SqlQuery<TEntity>(sql, parameters);
        }

        private ICollection<ActivityHistoryViewModel> Parse(ICollection<ActivityHistory> activityHistories)
        {
            ICollection<ActivityHistoryViewModel> activityHistoryVMs = new List<ActivityHistoryViewModel>();

            ActivityHistoryViewModel activityHistoryVM = null;
            foreach (ActivityHistory activityHistory in activityHistories)
            {
                activityHistoryVM = new ActivityHistoryViewModel();
                activityHistoryVM.actionDate = activityHistory.CreatedDate;
                activityHistoryVM.activity = activityHistory.Activity;
                activityHistoryVM.approver = activityHistory.Approver;
                activityHistoryVM.appriverDisplayName = activityHistory.ApproverDisplayName;
                activityHistoryVM.decision = activityHistory.Decision;
                activityHistoryVM.comment = activityHistory.Comments;
                activityHistoryVMs.Add(activityHistoryVM);
            }

            return activityHistoryVMs.OrderBy(x => x.actionDate).ToList();
        }


        private IEnumerable<FileUploadViewModel> ParseFileAttachements(IEnumerable<FileAttachement> fileUploads)
        {
            var fileUploadViews = new List<FileUploadViewModel>();

            if(fileUploads != null) {
                fileUploads.Each(p =>
                {
                    fileUploadViews.Add(new FileUploadViewModel() {
                        id = p.Id,
                        description = p.Comment,
                        fileName = p.FileName,
                        name = p.Name,
                        requestHeaderId = p.RequestHeaderId,
                        uploadDate = p.CreatedDate,
                        activity = p.Status != null ? p.Status.CapitalLetter().Replace("Originator", "Submission") : string.Empty,
                        readOnly = p.ReadOnlyRecord
                    });
                });
            }
            return fileUploadViews;
        }

        private IEnumerable<FileAttachement> ParseFileAttachements(IEnumerable<FileUploadViewModel> fileUploadViews) {
            IList<FileAttachement> attachements = new List<FileAttachement>();

            if (fileUploadViews == null)
                return null;

            fileUploadViews.Each((p) => {
                var attach = new FileAttachement() {
                    Id = p.id,
                    Serial = p.serial,
                    FileName = p.fileName,
                    Comment = p.description,
                    Name = p.name
                };
                attachements.Add(attach);
            });
            return attachements;
        }

        protected T MapDataBC(string serialNo, string sharedUser, string managedUser)
        {
            T workflowInstance = (T)Activator.CreateInstance(typeof(T));
            workflowInstance.SerialNo = serialNo;
            workflowInstance.CurrentUser = RequestContext.Principal.Identity.Name;
            workflowInstance.SharedUser = sharedUser;
            workflowInstance.ManagedUser = managedUser;
            return workflowInstance;
        }

        protected T MapDataBC(E viewData)
        {
            T workflowInstance = (T)Activator.CreateInstance(typeof(T));
            var employee = _employeeService.GetEmpByLoginName(RequestContext.Principal.Identity.Name);
            workflowInstance.Action = viewData.action;
            workflowInstance.Actions = viewData.actions;
            workflowInstance.Activity = viewData.activity;
            workflowInstance.Comment = viewData.comment;
            workflowInstance.Priority = viewData.priority;
            workflowInstance.SerialNo = viewData.serial;
            workflowInstance.AttachmentType = this._attachmentType;
            workflowInstance.UploadFiles = ParseFileAttachements(viewData.fileUploads.allItems);
            workflowInstance.AddUploadFiles = ParseFileAttachements(viewData.fileUploads.newItems);
            workflowInstance.EditUploadFiles = ParseFileAttachements(viewData.fileUploads.updatedItems);
            workflowInstance.DelUploadFiles = ParseFileAttachements(viewData.fileUploads.removedItems);
            workflowInstance.CurrentUser = RequestContext.Principal.Identity.Name;
            workflowInstance.fullName = employee.fullName;
            workflowInstance.loginName = employee.loginName;
            workflowInstance.RuntimeURL = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/";
            if (viewData.requestor !=null )
            {
                Employee requestor = new Employee();
                requestor.Id = viewData.requestor.id;
                workflowInstance.Requestor = requestor;
            }

            MoreMapDataBC(viewData, workflowInstance);
            return workflowInstance;
        }

        protected abstract IRequestFormService<T> GetRequestformService();
        protected abstract E CreateNewFormDataViewModel();
        protected abstract void MoreMapDataBC(E viewData, T workflowInstance);
        protected abstract void MoreMapDataView(T workflowInstance, E viewData);
    }
}