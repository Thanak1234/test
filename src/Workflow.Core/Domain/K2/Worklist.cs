using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataContract.K2
{
    public class WorklistHeader
    {
        public int RequestHeaderId { get; set; }
        public int ProcInstId { get; set; }
        public string RequestCode { get; set; }
        public string Requestor { get; set; }
        public string ProcessName { get; set; }
        public DateTime LastActionDate { get; set; }
    }

    public partial class Worklist : WorklistHeader
    {
        private string _openFormUrl;
        public Worklist()
        {
            Actions = new List<string>();
        }

        public Worklist Update(WorklistHeader header)
        {
            RequestHeaderId = header.RequestHeaderId;
            RequestCode = header.RequestCode;
            Requestor = header.Requestor;
            ProcessName = header.ProcessName;
            LastActionDate = header.LastActionDate;

            return this;
        }

        public int ActInstDestId { get; set; }
        public string Serial { get; set; }
        public string Folio { get; set; }
        public string WorkflowPath { get; set; }
        public string WorkflowUrl {
            get
            {
                return string.Format(@"#k2/flow/{0}/{1}", ProcInstId, Folio);
            }
        }
        public string OpenFormUrl {
            get
            {
                if (!string.IsNullOrEmpty(_openFormUrl))
                {
                    var segments = _openFormUrl.Split('#');
                    if (segments.Length > 1) {
                        _openFormUrl = ("#" + segments[1]);
                    }
                }

                return _openFormUrl;
            }
            set
            {
                _openFormUrl = value;
            }
        }
        public string ActivityName { get; set; }
        public string Originator { get; set; }
        /* Owner user or current user */
        public string ManagedUser { get; set; }
        public string OpenedBy { get; set; }
        public string AllocatedUser { get;  set; }
        public DateTime? OpenedDateTime { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public List<string> Actions { get; set; }
        [NotMapped]
        public bool IsOutOfficeInst {
            get {
                // Define as OOF for status that "Available", but allocated user is not current user
                return (ManagedUser.ToUpper() != AllocatedUser.ToUpper()) && (Status == "Available");
            }
        }
        [NotMapped]
        public bool IsEscalatable
        {
            get
            {
                // Set disabled escaltion on OOF process instance.
                // Set disabled escaltion on OPENED process instance.
                return !IsOutOfficeInst && (Status != "Open");
            }
        }
    }
}
