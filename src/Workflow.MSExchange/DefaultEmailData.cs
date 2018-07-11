using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.MSExchange.Core;

namespace Workflow.MSExchange
{
    public class DefaultEmailData : IEmailData
    {
        private List<EmailFileAttachment> _fileAttachments;

        public List<EmailFileAttachment> AttachmentFiles
        {
            get { return _fileAttachments ?? (_fileAttachments = new List<EmailFileAttachment>()); }
            set { _fileAttachments = value; }
        }

        public List<string> Bccs
        {
            get; set;
        }

        public string Body
        {
            get; set;
        }

        public List<string> Ccs
        {
            get; set;
        } = new List<string>();

        public object Model
        {
            get; set;
        } = new List<string>();

        public List<string> Recipients
        {
            get; set;
        } = new List<string>();

        public string Subject
        {
            get; set;
        }
    }
}