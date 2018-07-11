/**
*@author : Phanny
*/


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Workflow.Business.Interfaces;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.BPMDATA;
using Workflow.DataObject.Worklists;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Attachement;
using Workflow.Domain.Entities.Core;
using Workflow.Domain.Entities.BatchData;
using Workflow.Framework;
using Workflow.MSExchange;
using Workflow.MSExchange.Core;
using Workflow.ReportingService.Report;

namespace Workflow.Business
{
    public abstract class AbstractRequestFormBC<T,E> : IRequestFormBC<T> 
        where T : AbstractWorkflowInstance
        where E : IFormDataProcessing
    {
        IProcInstProvider _procInstProvider;
        public const string REQUESTOR_REWORKED = "Requestor Rework";
        public const string DEPT_HOD_APPROVAL = "HoD Approval";
        public const string FORM_SUBMISSION = "Submission";
        protected const string FORM_VIEW = "Form View";
        protected const string FORM_EDIT = "Modification";
        protected const string FORM_DRAFT = "Draft";
        protected enum DataOP { AddNew, DEL, EDIT };
        protected string _processFolderName = "";
        protected RequestApplication REQ_APP = null;
        protected IList<ActivityDto> ActivityList = new List<ActivityDto>();
        protected string REQ_CODE = "";
        
        #region Data members

        private ICollection<IActivity<E>> _activities = null;
        private ActivityHistory _activityHistory = null;

        protected readonly IRequestHeaderRepository requestHeaderRepository;
        protected readonly IActivityHistoryRepository activityHistoryRepository;
        protected readonly IWMRepository _wlRepo; 
        protected readonly IEmployeeRepository employeeRepository;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IAttachementRepository<FileTemp> fileAttachementRepository;

        protected readonly ITransactionHistoryRepository _transactionHistoryRepository;
        protected readonly IDocumentRepository _documentRepository;
        protected readonly Repository repository;

        private IActivity<E> _curActivity = null;
        
        #endregion

        #region Properties
        
        public ProcInst WorklistItem { get; set; }

        public T WorkflowInstance { get; set; }

        
        protected RequestHeader _requestHeader;

        //When access RequestHeader property data will be loaded from DB automatically if serial no is assigned
        public RequestHeader RequestHeader
        {
            get
            {
                return _requestHeader ?? (_requestHeader = GetRequestHeader());
            }
        }

 
        #endregion

        public AbstractRequestFormBC(IDbFactory dbFactory, IDbFactory dbDocFactory)
        {
            this._transactionHistoryRepository = new TransactionHistoryRepository(dbFactory);
            this._documentRepository = new DocumentRepository(dbFactory);

            this.requestHeaderRepository = new RequestHeaderRepository(dbFactory);
            this.activityHistoryRepository = new ActivityHistoryRepository(dbFactory);
            this._wlRepo = new WMRepository(dbFactory);
            this.employeeRepository = new EmployeeRepository(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
            this.fileAttachementRepository = new AttachementRepository<FileTemp>(dbDocFactory);
            this.repository = new Repository();

            //Adding by default since all work flow get view activity for users
            AddActivities(new DefaultActivity<E>(FORM_VIEW));
            
            REQ_APP = _wlRepo.GetReqAppByCode(GetRequestCode());
            REQ_CODE = REQ_APP.RequestCode;
            ActivityList = _wlRepo.GetActivitiesByReqCode(REQ_APP.RequestCode);
            _processFolderName = ConfigurationManager.AppSettings["ProcessFolderName"] ?? "Nagaworld\\";
            InitActivityConfiguration();
        }
        
        #region Methods
        protected virtual void InitActivityConfiguration() 
        {
            
        }

        protected virtual string getActivityforFileUpload() {
            if (FORM_SUBMISSION.Equals(CurrentActivity().ActivityName) || 
                REQUESTOR_REWORKED.Equals(CurrentActivity().ActivityName)) {
                return "ORIGINATOR";
            } else {
                string activityName = CurrentActivity().ActivityName;
                return activityName.Replace(" ", "_").ToUpper();
            }
        }

        protected virtual void CreateWorkflowInstance() {
            if (WorkflowInstance == null) {
                WorkflowInstance = (T)Activator.CreateInstance(typeof(T));
            }
        }
        protected virtual string getFullProccessName() {
            return REQ_APP.ProcessPath;
        }

        protected virtual string GetRequestCodePrefix() {
            return REQ_APP.ProcessCode;
        }

        public string TakeAction()
        {
            _procInstProvider = new ProcInstProvider(WorkflowInstance.CurrentUser);
            string message = Invalid();
			if(message != null){
                //return JsonConvert.SerializeObject(new ResponseText(){ Message = message,Show = true});
                throw new Exception(message);
			}
			
            if (CurrentActivity().CurrAction.FormDataProcessing.IsAddNewRequestHeader)
            {
                string invalideMsg = repository.ExecSingle<string>(string.Format(
                @"SELECT TOP(1) TITLE FROM [BPMDATA].[PROCESS_VALIDATOR] WHERE REQUEST_CODE = '{0}' AND REQUESTOR = {1}",
                REQ_CODE, WorkflowInstance.Requestor.Id));
                if (!string.IsNullOrEmpty(invalideMsg))
                {
                    return JsonConvert.SerializeObject(new ResponseText() { Message = invalideMsg, Show = true });
                }

                if (!_procInstProvider.CanStartWorkFlow(WorkflowInstance.CurrentUser, getFullProccessName()))
                {
                    throw new Exception(String.Format("{0} has no privilege to start work flow {1}", WorkflowInstance.CurrentUser, getFullProccessName()));
                }
            }

            AddNewRequestHeader();
            EditRequestHeader();
            _activityHistory = RecordActivityHistory();
            SaveAttachments();
            //UpdateLastActivity();
            TakeFormAction();
            
            unitOfWork.commit();

            if (!IsSaveDraft())
            {
                processWorkFlow(); /* skip out process instance */
            }

            RecordTransactionHistory(_activityHistory);
            SendMail();

            return JsonConvert.SerializeObject(new ResponseText()
            {
                Message = (!string.IsNullOrEmpty(WorkflowInstance.Message) ?
                    WorkflowInstance.Message.Replace("@FOLIO", RequestHeader.Title) : 
                    string.Format("Your request number is {0}", RequestHeader.Title)),
                Show = (!string.IsNullOrEmpty(WorkflowInstance.Message)) // custom message is empty then hide message
            });
        }

        protected virtual string GetJsonDataInstance() {
            return JsonConvert.SerializeObject(WorkflowInstance);
        } 

        private void RecordTransactionHistory(ActivityHistory activityHistory)
        {
            //if (activityHistory == null)
            //{
            //    activityHistory = activityHistoryRepository.GetLastActivityHistory(
            //        RequestHeader.Id,
            //        CurrentActivity().ActivityName,
            //        WorkflowInstance.loginName
            //    );
            //}

            string jsonData = GetJsonDataInstance();

            var transactionHistory = new TransactionHistory()
            {
                ObjectType = "PROCESS_REQUEST",
                ObjectName = (activityHistory != null? "[BPMDATA].[APPROVAL_COMMENT]":"[BPMDATA].[REQUEST_HEADER]"),
                ObjectId = (activityHistory != null? activityHistory.Id:RequestHeader.Id),
                JsonData = jsonData,
                CreatedDate = DateTime.Now,
                CreatedBy = WorkflowInstance.loginName
            };
            _transactionHistoryRepository.Add(transactionHistory);
            unitOfWork.commit();


        }

        protected virtual string Invalid(){
            try
            {
                var currAction = CurrentActivity().CurrAction;


                if(currAction.FormDataProcessing.IsRequiredComment && string.IsNullOrWhiteSpace(WorkflowInstance.Comment) ) {
                    return "Comment is required. Please give some comment.";
                }

                if (currAction.FormDataProcessing.IsRequiredAttachment && (WorkflowInstance.UploadFiles == null || WorkflowInstance.UploadFiles.Count()==0)) {
                    return "Attachment is required. Please upload some files.";
                }


                if (CurrentActivity().CurrAction.ActionName == "Edit" &&
                !requestHeaderRepository.Editable(RequestHeader.Id, GetRequestCode(), WorkflowInstance.CurrentUser))
                {
                    return "Your modification was failed because session expired.";
                }
                return null;



            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEmailData CreateEmailData(string notifyRole, IEnumerable<string> ccList = null, string subject = "")
        {
            var email = new DefaultEmailData();

            if (RequestHeader.Status != AbstractAction<E>.CANCELED_ACTION)
            {
                RequestHeader.Status = AbstractAction<E>.AMENDED_ACTION;
            }

            var body = @"<font style='font-family:Arial;font-size:11pt;font-weight:Normal;font-style:Normal;font-stretch:Normal;color:#000000;text-align:Left;text-decoration:None;line-height:1'>
                          Dear @@ORIGINATOR,<br/><br/>
                          <p>@@FORM_NAME process has been @@ACTION by: @@DECISION_BY.</p>
                          <span>Summary:</span><br/>
                          <span>Ref: @@FORM_NO</span><br/>
                          <span>Comment: @@COMMENT</span><br/>
                          <p>
                               Thanks &amp; Regards, <br/>Process Automation.
                          </p>
                        </font >
                        @@SIGNATURE";

            string signature = "";
            try
            {
                var contents = new Repository().ExecDynamicSqlQuery(@"SELECT CONTENT [content] FROM [SYSTEM].[SETTINGS] WHERE MODULE = 'EMAIL' AND [KEY] = 'EMAIL_SIGNATURE'");
                signature = contents[0].content;
            } catch
            {

            }            
            
            var originator = requestHeaderRepository.GetRequestorEmail(RequestHeader.SubmittedBy);
            
            email.Subject = string.Format(
                "Notification (Ref:{0}) ({1} by: {2}){3}", 
                RequestHeader.Title,
                RequestHeader.Status,
                WorkflowInstance.fullName,
                subject
            );

            email.Body = body
                    .Replace("@@ORIGINATOR", originator.DISPLAY_NAME)
                    .Replace("@@FORM_NAME", REQ_APP.ProcessName)
                    .Replace("@@ACTION", RequestHeader.Status.ToLower())
                    .Replace("@@DECISION_BY", WorkflowInstance.fullName)
                    .Replace("@@FORM_NO", RequestHeader.Title)
                    .Replace("@@COMMENT", WorkflowInstance.Comment)
                    .Replace("@@ORIGINATOR", "ITC Stone Sans Std Medium")
                    .Replace("@@SIGNATURE", signature);

            var recipients = new List<string>();
            recipients.Add(originator.EMAIL);
            email.Recipients = recipients;
            string[] roleCodes = notifyRole.Split(',');
            
            IEnumerable<string> ccParticipantList = new List<string>();
            foreach (var roleCode in roleCodes)
            {
                if (!string.IsNullOrEmpty(roleCode) && roleCode != ",")
                {
                    ccParticipantList = ccParticipantList.Concat(requestHeaderRepository.GetEmailNotification(-1, REQ_APP.RequestCode, roleCode, false));
                }
            }

            if (ccList != null)
            {
                ccParticipantList = ccParticipantList.Concat(ccList);

            }

            // Customize modification mailing list
            if (RequestHeader != null) {
                var destinationUsers = requestHeaderRepository.GetEmailModification(RequestHeader.Id);
                IList<string> mailingList = new List<string>();
                foreach (var destinationUser in destinationUsers)
                {
                    mailingList.Add(destinationUser.Email);
                }
                if (mailingList.Count > 0)
                {
                    ccParticipantList = ccParticipantList.Concat(mailingList);
                }
            }

            // Assign mailing list to CC list of an email.
            email.Ccs = ccParticipantList.Distinct().ToList();

            var genericForm = new GenericFormRpt();
            byte[] buffer = genericForm.Export(new GenericFormParam { RequestHeaderId = RequestHeader.Id }, REQ_APP.ReportPath, ExportType.Pdf);
            var FileName = string.Concat(RequestHeader.Title, "_", DateTime.Now.ToString("yyyyMMddhhmmss"), ".pdf");
            var fileAttachments = new EmailFileAttachment(FileName, buffer);

            email.AttachmentFiles.Add(fileAttachments);
            return email;
        }

        public bool IsSaveDraft()
        {
            if (CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.SAVE_ACTION ||
                CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.UPDATE_ACTION ||
                CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.UPDATED_ACTION)
            {
                return true;
            }
            return false;
        }

        private void SendMail()
        {
            try
            {
                IEmailData emailData = CurrentActivity().Email;
                if (emailData != null)
                {
                    if (CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.UPDATED_ACTION
                        || CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.EDITED_ACTION)
                    {
                        IEmailService emailService = new EmailService();
                        emailService.SendEmail(emailData);
                    }
                }
            }
            catch (Exception)
            {
                //TO DO NOTHING
            }
        }

        private void processWorkFlow()
        {
            var formProcessing = CurrentActivity().CurrAction.FormDataProcessing;

            //Start workflow
            if (formProcessing.IsAddNewRequestHeader)
            {
                if (RequestHeader.Id > 0)
                {
                   
                    

                    IDictionary<string, Object> dataField = GetDataField();
                    dataField.Add("RequestHeaderID", RequestHeader.Id);
                    dataField.Add("RuntimeURL", WorkflowInstance.RuntimeURL);
                    dataField.Add(GetUserComment(), string.Empty);
                    
                    Priority priority = Priority.LOW;
                    if(WorkflowInstance.Priority == 1)
                    {
                        priority = Priority.MEDIUM;
                    }else if (WorkflowInstance.Priority == 0)
                    {
                        priority = Priority.HIGH;
                    }

                    string proname = getFullProccessName();
                    if (formProcessing.TriggerWorkflow) {
                        int procInstId = _procInstProvider.StartProcInstance(new InstParam()
                        {
                            ProcName = proname,
                            Folio = RequestHeader.Title,
                            CurrentUser = WorkflowInstance.CurrentUser,
                            Priority = priority,
                            DataFields = dataField
                        });
                        RequestHeader.ProcessInstanceId = procInstId;
                    }
                    
                    //Update process instance id
                    requestHeaderRepository.Update(RequestHeader);
                    unitOfWork.commit();

                }
                else
                { 

                    throw new Exception(" No request header found to start workflow ");
                }
            }
            //Take action
            else if(formProcessing.TriggerWorkflow)
            {
                if (IsSubmitDraft())
                {
                    IDictionary<string, Object> dataField = GetDataField();
                    dataField.Add("RequestHeaderID", RequestHeader.Id);
                    dataField.Add("RuntimeURL", WorkflowInstance.RuntimeURL);
                    dataField.Add(GetUserComment(), string.Empty);

                    Priority priority = Priority.LOW;
                    if (WorkflowInstance.Priority == 1)
                    {
                        priority = Priority.MEDIUM;
                    }
                    else if (WorkflowInstance.Priority == 0)
                    {
                        priority = Priority.HIGH;
                    }

                    string proname = getFullProccessName();
                    RequestHeader.Title = requestHeaderRepository.GetRequestNo(GetRequestCodePrefix(), GetRequestCode());
                    if (formProcessing.TriggerWorkflow)
                    {
                        int procInstId = _procInstProvider.StartProcInstance(new InstParam()
                        {
                            ProcName = proname,
                            Folio = RequestHeader.Title,
                            CurrentUser = WorkflowInstance.CurrentUser,
                            Priority = priority,
                            DataFields = dataField
                        });
                        RequestHeader.ProcessInstanceId = procInstId;
                    }

                    requestHeaderRepository.Update(RequestHeader);
                    unitOfWork.commit();
                }
                else {
                    var dataFields = GetDataField();
                    dataFields.Add(GetUserComment(), WorkflowInstance.Comment);
                    _procInstProvider.Execute(new ExecInstParam() {
                        SerialNo = WorkflowInstance.SerialNo,
                        Action = WorkflowInstance.Action,
                        DataFields = dataFields
                    });
                }
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        
        protected virtual void SaveForm() {
            throw new NotImplementedException();
        }

        public void SaveDraft() {
            throw new NotImplementedException();
        }

        public void ReleaseDraft() {
            throw new NotImplementedException();
        }

        public void LoadData()
        {
            //Create workflow instance
            CreateWorkflowInstance();
            _procInstProvider = new ProcInstProvider(WorkflowInstance.CurrentUser);
            //Assign serail no to the instance
            var serialNo = WorkflowInstance.SerialNo;
            if (String.IsNullOrWhiteSpace(serialNo))
            {
                throw new Exception("No serial can be found");
            }

            if (serialNo.EndsWith("_99999"))
            {

                if (requestHeaderRepository.Editable(RequestHeader.Id, GetRequestCode(), WorkflowInstance.CurrentUser))
                {
                    WorkflowInstance.Activity = FORM_EDIT;
                }
                else
                {
                    WorkflowInstance.Activity = FORM_VIEW;
                }
            }
            else if (serialNo.EndsWith("_00000")) {
                WorkflowInstance.Activity = FORM_DRAFT;
            }
            else
            {
                WorklistItem = _procInstProvider.OpenWorklistItem(serialNo);
                string activityName = WorklistItem.ActivityName;// ActivityInstanceDestination.Name;
                WorkflowInstance.Actions = GetActionList(activityName);
                WorkflowInstance.ViewConfig = GetViewConfig(activityName);
                WorkflowInstance.Activity = activityName;
            }
            
            WorkflowInstance.SerialNo = serialNo;
            
            //Load data to the instance
            LoadWorkflowInstance();
        }

        private IList<string> GetActionList(string activityName) {
            
            var actions = new List<string>();
            for (int i = 0; i < WorklistItem.Actions.Count; i++)
            {
                actions.Add(WorklistItem.Actions[i]);
            }

            try
            {
                var activity = ActivityList.Where(t => t.Name == activityName).Single();
                if (activity.Property != null) {
                    var propertyString = JObject.Parse(activity.Property).SelectToken("actionProperty").ToString();
                    var actionProperty = JsonConvert.DeserializeObject<List<ActionProperty>>(propertyString);
                    
                    if (actionProperty.Where(t => t.ActionName == AbstractAction<IDataProcessing>.UPDATED_ACTION).Count() > 0)
                    {
                        actions.Add(AbstractAction<IDataProcessing>.UPDATED_ACTION);
                    }
                }
            } catch (Exception) {
                // Need too implment configuration action in table [ADMIN].[ACTIVITY]
            }
            
            return actions;
        }

        private string GetViewConfig(string activityName) {
            try {
                var activity = ActivityList.Where(t => t.Name == activityName).Single();
                if (activity.Property != null) {
                    var configs = JsonConvert.SerializeObject(JObject.Parse(activity.Property).SelectTokens("viewConfig"));
                    return configs;
                }
            } catch (Exception) {

            }

            return null;
        }

        protected void AddActivities(IActivity<E> activity)
        {
            if(_activities == null)
            {
                _activities = new List<IActivity<E>>();
            }
            _activities.Add(activity);
        }

        protected void AddActivities(IList<IActivity<E>> activities) {
            foreach (var activity in activities) {
                AddActivities(activity);
            }
        }

        protected string GetApprovalRoleFromStoreProcedure()
        {
            string result = new Repository().ExecSingle<string>("EXEC BPMDATA.GET_SPECIFIC_ROLE @REQUEST_HEADER_ID=@RequestHeaderId", new object[] {
                new SqlParameter("@RequestHeaderId", RequestHeader.Id)
            });
            return result;
        }

        #endregion

        #region Private Methodss
        private RequestHeader GetRequestHeader()
        {
            if (String.IsNullOrWhiteSpace(WorkflowInstance.SerialNo)) //Add request form
            {
                return null;
            }
            else
            {
                String[] serialNo = WorkflowInstance.SerialNo.Split('_');
                if(serialNo.Length ==0 && String.IsNullOrWhiteSpace(serialNo[0]) )
                {
                    throw new Exception(String.Format("Process instance id could not be found in serial : {0} ", serialNo));
                }

                RequestHeader requestHeader = new RequestHeader();
                int processInstanceId = Int32.Parse(serialNo[0]);
                if (WorkflowInstance.SerialNo.EndsWith("_00000"))
                {
                    requestHeader = requestHeaderRepository.Get(s => s.Id == processInstanceId);
                }
                else {   
                    requestHeader = requestHeaderRepository.Get(s => s.ProcessInstanceId == processInstanceId);
                }
                

                if (requestHeader == null)
                {
                    throw new Exception(String.Format("Request header could not be found by process instance id; {0}, serial No : {1}", processInstanceId, WorkflowInstance.SerialNo));
                }

                var requestor = employeeRepository.GetById(requestHeader.RequestorId);
                requestHeader.Requestor = requestor;

                return requestHeader;
            }
        }

        //Add new reuest header for form submission 
        private void AddNewRequestHeader()
        {
            if (!CurrentActivity().CurrAction.FormDataProcessing.IsAddNewRequestHeader)
            {
                return;
            }

            string folio = null;
            string activity = CurrentActivity().ActivityName;
            if (!IsSaveDraft()){
                folio = requestHeaderRepository.GetRequestNo(GetRequestCodePrefix(), GetRequestCode());
            }

            var requestHeader = new RequestHeader();
            requestHeader.LastActionBy = "k2:"+WorkflowInstance.CurrentUser;
            requestHeader.LastActivity = activity;
            requestHeader.LastActionDate = DateTime.Now;
            requestHeader.Priority = WorkflowInstance.Priority;
            requestHeader.RequestorId = WorkflowInstance.Requestor.Id;
            
            requestHeader.SubmittedBy = "k2:" + WorkflowInstance.CurrentUser;
            requestHeader.CurrentActivity = activity;
            requestHeader.Status = CurrentActivity().CurrAction.ActionName;
            requestHeader.RequestCode = GetRequestCode();
            requestHeader.Title = folio;
            requestHeader.NoneK2 = true;
            requestHeaderRepository.Add(requestHeader);
            _requestHeader = requestHeader;

            // record requestor log
            if (requestHeader.Id > 0) {
                requestHeaderRepository.executeSqlCommand(
                    string.Format("EXEC [BPMDATA].[REQUESTOR_TRACKING] @RequestHeaderId = {0}", 
                    requestHeader.Id
                ));
            }
        }
        //Edit request header for rework: change requestor or priority
        private void EditRequestHeader()
        {
            var dataProcess = CurrentActivity().CurrAction.FormDataProcessing;
            var actionName = CurrentActivity().CurrAction.ActionName;
            bool edit = false;
            if (!dataProcess.IsAddNewRequestHeader)
            {
               if(dataProcess.IsEditRequestor || dataProcess.IsEditPriority || dataProcess.IsUpdateLastActivity)
                {
                    edit = true;
                    if (RequestHeader == null)
                    {
                        throw new Exception(String.Format("Request header could not be found by serial: {0}", WorkflowInstance.SerialNo));
                    }
                }
            }

            if (dataProcess.IsEditRequestor)
            {
                RequestHeader.RequestorId = WorkflowInstance.Requestor.Id;
            }

            if (dataProcess.IsEditPriority)
            {
                RequestHeader.Priority = WorkflowInstance.Priority;
            }

            if (dataProcess.IsUpdateLastActivity)
            {
                RequestHeader.LastActionDate = DateTime.Now;
                RequestHeader.LastActionBy = WorkflowInstance.CurrentUser;
                if (IsSubmitDraft())
                {
                    RequestHeader.LastActivity = FORM_SUBMISSION;
                    RequestHeader.Status = AbstractAction<IDataProcessing>.SUBMITTED_ACTION;
                }
                else {
                    RequestHeader.LastActivity = CurrentActivity().ActivityName;
                    RequestHeader.Status = actionName;
                }
            }

            if (edit)
            {
                requestHeaderRepository.Update(RequestHeader);
            }

        }

        private bool IsSubmitDraft() {
            return (string.IsNullOrEmpty(RequestHeader.Title) &&
                (CurrentActivity().ActivityName == FORM_SUBMISSION || (CurrentActivity().ActivityName == FORM_DRAFT)) &&
                (CurrentActivity().CurrAction.ActionName == AbstractAction<IDataProcessing>.SUBMITTED_ACTION));
        }

        private ActivityHistory RecordActivityHistory()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveActivityHistory)
            {
                string activityName = CurrentActivity().ActivityName;
                if (IsSubmitDraft()) {
                    activityName = FORM_SUBMISSION;
                }
                var activityHistory = new ActivityHistory();
                activityHistory.RequestHeader = RequestHeader;
                activityHistory.Decision = CurrentActivity().CurrAction.ActionName;
                activityHistory.Activity = activityName;
                activityHistory.Approver = WorkflowInstance.CurrentUser;
                activityHistory.ApproverDisplayName = WorkflowInstance.CurrentUser;
                activityHistory.Approver = WorkflowInstance.CurrentUser;
                activityHistory.ApproverDisplayName = WorkflowInstance.fullName;
                activityHistory.Comments = WorkflowInstance.Comment;
                activityHistory.ApplicationName = GetRequestCode();
                activityHistoryRepository.Add(activityHistory);
                return activityHistory;
            }

            return null;
        }        

        private void UpdateLastActivity()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsUpdateLastActivity)
            {
                RequestHeader.Status = CurrentActivity().CurrAction.ActionName;
                RequestHeader.LastActionDate = DateTime.Now;
                RequestHeader.LastActionBy = WorkflowInstance.CurrentUser;
            }
        }

        //TODO: file attachment

        private void SaveAttachments()
        {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveAttachments)
            {
                if(fileAttachementRepository != null){
                    SaveAttachmentFiles();
                }
            }
        }


        private void LoadWorkflowInstance()
        {
            //Load request header: When access RequestHeader property, data will be loaded from DB automatically if serial is assigned

            WorkflowInstance.RequestHeaderId = RequestHeader.Id;
            WorkflowInstance.RequestNo = RequestHeader.Title;
            WorkflowInstance.Priority = RequestHeader.Priority;
            WorkflowInstance.Requestor = RequestHeader.Requestor;
            WorkflowInstance.ActivityHistories = RequestHeader.ActivityHistories;
            WorkflowInstance.UploadFiles = GetUploadFiles();

            RequestHeaderRepository.WorkflowStatus workflowStatus = requestHeaderRepository.GetWorkflowStatus(RequestHeader.Id);
            WorkflowInstance.LastActivity = workflowStatus.LAST_ACTIVITY;
            WorkflowInstance.Status = (RequestHeader.Status == "Save Draft")? RequestHeader.Status:workflowStatus.STATUS;
            //Load data from specific form
            LoadFormData();

        }

     
        #endregion

        #region Abstraction

        protected abstract void TakeFormAction();
        //protected abstract void SaveForm();
        protected abstract void LoadFormData();
        protected abstract String GetRequestCode();
        protected IActivity<E> CurrentActivity() {

            if (_curActivity == null)
            {
                _activities.Each(p => {
                    if (p.ActivityName.ToUpper().Equals(WorkflowInstance.Activity.ToUpper()))
                    {
                        if (p == null) {
                            throw new Exception("No activity was found.");
                        }
                        _curActivity = p;
                        _curActivity.SetCurrActionName(WorkflowInstance.Action);
                    }
                });
            }

            return _curActivity;
           
        }
        //protected abstract void CreateWorkflowInstance();
        //protected abstract string getFullProccessName();
        //protected abstract IEnumerable<FileAttachement> GetUploadFiles();
        //protected abstract string GetRequestCodePrefix();
        //protected abstract void SaveAttachmentFiles();
        protected virtual string GetUserComment() { return "UserComment"; }
        protected virtual Dictionary<string, object> GetDataField() { return new Dictionary<string, object>(); }
        public string CurrentActivityCode { get; set; }

        #endregion

        #region Method - Attach Document
        protected virtual bool IsRemoveableAttachment {get; set;} // Typically canot removed, depend on difference activity

        protected virtual bool IsAuthorizeRemoveAttachment(string activityCode) {
            return (CurrentActivityCode == activityCode);
        }

        protected virtual IEnumerable<FileAttachement> GetUploadFiles()
        {
            CurrentActivityCode = getActivityforFileUpload();
            var collection = new List<FileAttachement>();
            var documents = _documentRepository.GetDocumentList(RequestHeader.Id);
           
            string currentUser = string.Empty;
            if (!string.IsNullOrEmpty(WorkflowInstance.CurrentUser)) {
                currentUser = WorkflowInstance.CurrentUser.Replace("K2:", string.Empty);
            }
           
            foreach (var document in documents) {
                var isAuthorizeRemoveAttachment = IsAuthorizeRemoveAttachment(document.ActivityCode);
                if (currentUser == document.CreatedBy || IsRemoveableAttachment) {
                    isAuthorizeRemoveAttachment = true;
                }

                collection.Add(new FileAttachement
                {
                    Id = document.Id,
                    RequestHeaderId = RequestHeader.Id,
                    Name = document.Name,
                    Comment = document.Description,
                    Serial = document.DocumentId != null?document.DocumentId.ToString():string.Empty,
                    FileName = document.FileName,
                    Status = document.ActivityCode,
                    ReadOnlyRecord = !isAuthorizeRemoveAttachment
                });
            }
            return collection;
        }
        
        protected virtual void SaveAttachmentFiles()
        {
            // Attach document to request instance
            foreach (var file in WorkflowInstance.AddUploadFiles)
            {
                var document = _documentRepository.GetById(file.Id);
                

                if (document != null)
                {
                    document.ObjectId = RequestHeader.Id;
                    document.ObjectName = "[BPMDATA].[REQUEST_HEADER]";
                    document.CreatedBy = WorkflowInstance.loginName;
                    document.Status = "ATTACH_TO_PROCESS";
                }
                if (_activityHistory != null)
                {
                    document.ObjectId = _activityHistory.Id;
                    document.ObjectName = "[BPMDATA].[APPROVAL_COMMENT]";
                }
                _documentRepository.Update(document);
            }

            // Remove document
            foreach (var file in WorkflowInstance.DelUploadFiles)
            {
                var document = _documentRepository.GetById(file.Id);
                if (document == null) // Removed temporary
                {
                    var documents = _documentRepository.GetMany(p => p.DocumentId == new Guid(file.Serial));
                    documents.Each(p =>
                    {
                        p.DeletedBy = WorkflowInstance.loginName;
                        p.DeletedDate = DateTime.Now;
                        _documentRepository.Update(p);
                    });
                } else { 
                    document.DeletedBy = WorkflowInstance.loginName;
                    document.DeletedDate = DateTime.Now;
                    _documentRepository.Update(document);
                }
            }
        }


       

        #endregion
    }
}
