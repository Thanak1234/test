using Workflow.MSExchange;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Workflow.DataObject.BPMDATA;
using Workflow.DataObject.Worklists;

namespace Workflow.Business
{
    public class ActivityEngine : AbstractActivity<IDataProcessing>, IActivity<IDataProcessing>
    {

        private const string SUBMISSION_ACT = "Submission";
        private const string MODIFICATION_ACT = "Modification";
        private const string REQ_REWORKED_ACT = "Requestor Rework";
        private Func<IEmailData> _fnEmailData;

        public override IEmailData Email { get { return _fnEmailData(); } }

        public ActivityEngine(string activityName = SUBMISSION_ACT)
            : base(activityName)
        {
            if (activityName.ToUpper() == SUBMISSION_ACT.ToUpper())
            {
                AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.SUBMITTED_ACTION, SUBMISSION_DATA_PROCESSING));
            }
        }

        public ActivityEngine(ActivityDto activity) : base(activity.Name)
        {
            InitActivity(activity);
        }

        public ActivityEngine(ActivityDto activity, Func<IEmailData> GetEmailData) : base(activity.Name)
        {
            InitActivity(activity);
            _fnEmailData = GetEmailData;
        }

        public ActivityEngine(Func<IEmailData> GetEmailData, IDataProcessing dataProcessing = null) : base(MODIFICATION_ACT)
        {
            if (dataProcessing == null)
            {
                dataProcessing = MODIFICATION_DATA_PROCESSING;
            }

            AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.EDITED_ACTION, dataProcessing));
            AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.CANCELED_ACTION, CANCELLED_EDIT_DATA_PROCESSING));
            _fnEmailData = GetEmailData;
        }

        private void InitActivity(ActivityDto activity)
        {
            Dictionary<string, bool> actions = new Dictionary<string, bool>();
            actions.Add(AbstractAction<IDataProcessing>.RE_SUBMITTED_ACTION, true);
            actions.Add(AbstractAction<IDataProcessing>.CANCELED_ACTION, true);
            actions.Add(AbstractAction<IDataProcessing>.UPDATED_ACTION, true);

            AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.RE_SUBMITTED_ACTION, RESUBMITTED_DATA_PROCESSING));
            AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.CANCELED_ACTION, CANCELLED_DATA_PROCESSING));
            AddAction(new DefaultAction<IDataProcessing>(AbstractAction<IDataProcessing>.UPDATED_ACTION, UPDATED_DATA_PROCESSING));

            try
            {
                if (activity != null && !string.IsNullOrEmpty(activity.Property))
                {
                    var propertyString = JObject.Parse(activity.Property).SelectToken("actionProperty").ToString();
                    var actionProperty = JsonConvert.DeserializeObject<List<ActionProperty>>(propertyString);

                    foreach (var actionConfig in actionProperty)
                    {
                        IDataProcessing dataProcessing = new FormDataProcessing()
                        {
                            IsAddNewRequestHeader = actionConfig.IsAddNewRequestHeader,
                            IsEditPriority = actionConfig.IsEditPriority,
                            IsEditRequestor = actionConfig.IsEditRequestor,
                            IsSaveActivityHistory = actionConfig.IsSaveActivityHistory,
                            IsUpdateLastActivity = actionConfig.IsUpdateLastActivity,
                            IsSaveRequestData = actionConfig.IsSaveRequestData,
                            IsSaveAttachments = actionConfig.IsSaveAttachments,
                            TriggerWorkflow = (actionConfig.TriggerWorkflow ?? true),
                            IsRequiredAttachment = actionConfig.IsRequiredAttachment,
                            IsRequiredComment = actionConfig.IsRequiredComment
                        };

                        if (activity.Name == REQ_REWORKED_ACT)
                        {
                            if (actionConfig.ActionName == AbstractAction<IDataProcessing>.RE_SUBMITTED_ACTION)
                            {
                                dataProcessing = RESUBMITTED_DATA_PROCESSING;
                            }
                            else if (actionConfig.ActionName == AbstractAction<IDataProcessing>.CANCELED_ACTION)
                            {
                                dataProcessing = CANCELLED_DATA_PROCESSING;
                            }
                        }

                        AddAction(new DefaultAction<IDataProcessing>(actionConfig.ActionName, dataProcessing));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private readonly IDataProcessing SUBMISSION_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = true,
            IsEditPriority = false,
            IsEditRequestor = false,
            IsSaveActivityHistory = false,
            IsUpdateLastActivity = false,
            IsSaveRequestData = true,
            IsSaveAttachments = true
        };
        private readonly IDataProcessing RESUBMITTED_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = false,
            IsEditPriority = true,
            IsEditRequestor = true,
            IsSaveActivityHistory = false,
            IsUpdateLastActivity = false,
            IsSaveRequestData = true,
            IsSaveAttachments = true
        };
        private readonly IDataProcessing UPDATED_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = false,
            IsEditPriority = false,
            IsEditRequestor = false,
            IsSaveActivityHistory = true,
            IsUpdateLastActivity = true,
            IsSaveRequestData = false,
            IsSaveAttachments = true
        };
        private readonly IDataProcessing CANCELLED_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = false,
            IsEditPriority = false,
            IsEditRequestor = false,
            IsSaveActivityHistory = false,
            IsUpdateLastActivity = false,
            IsSaveRequestData = false,
            IsSaveAttachments = false
        };
        private readonly IDataProcessing CANCELLED_EDIT_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = false,
            IsEditPriority = false,
            IsEditRequestor = false,
            IsSaveActivityHistory = false,
            IsUpdateLastActivity = false,
            IsSaveRequestData = false,
            IsSaveAttachments = false,
            TriggerWorkflow = false
        };
        private readonly IDataProcessing MODIFICATION_DATA_PROCESSING = new FormDataProcessing()
        {
            IsAddNewRequestHeader = false,
            IsEditPriority = false,
            IsEditRequestor = false,
            IsSaveActivityHistory = true,
            IsUpdateLastActivity = true,
            IsSaveRequestData = false,
            IsSaveAttachments = true,
            TriggerWorkflow = false
        };
    }

    public interface IDataProcessing : IFormDataProcessing { }
    public class FormDataProcessing : AbstractFormDataProcessing, IDataProcessing
    {
        public bool IsSaveRequestData { get; set; }
    }
}