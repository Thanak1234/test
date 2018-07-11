using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Business.Ticketing.Dto
{
    public enum NOTIFICATION_TYPE
    {
        NONE,
        EMAIL,
        SMS,
        ALL,
        EMAIL_UI,
        UI
    }

    public enum DESTINATION_ROUTING
    {
        REQUESTOR,
        SUBMITTER,
        ORIGINATOR, //both requestor and submitter

    }

    public enum REQURIED
    {
        NONE,
        OPTIONAL,
        REQUIRED
    }

    public enum ACTIVITY_OP
    {
        SAVE,
        EDIT,
        DELETE
    }

    public class ActivityConfig
    {

        public NOTIFICATION_TYPE NotifyType { get; set; } = NOTIFICATION_TYPE.NONE;
        //public REQURIED HasAssignee { get; set; } = REQURIED.NONE;
        public REQURIED HasComment { get; set; } = REQURIED.REQUIRED;
        public bool AutoAssigned { get; set; } = true;
        public REQURIED HasAttachment { get; set; } = REQURIED.OPTIONAL;
        public bool TeamMemberAvailibility { get; set; } = false;
        public bool UpdateLastAction { get; set; } = true;

        public string ActivityName { get; set; }
        public string ActivityCode { get; set; }
        public string Action { get; set; }

        public bool FirstResponseMarked { get; set; } = false;

        public ACTIVITY_OP Opr { get; set; } = ACTIVITY_OP.SAVE;
    }
}
