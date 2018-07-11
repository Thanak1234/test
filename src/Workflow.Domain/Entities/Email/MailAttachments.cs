using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Email
{
    public class MailAttachments
    {
        private string _serial;
        public int Id { get; set; }
        public int MailItemId { get; set; }
        public string FileName { get; set; }
        public string Ext { get; set; }
        public byte[] DataContent { get; set; }


        public string Serial
        {
            get
            {
                if (string.IsNullOrEmpty(_serial))
                {
                    _serial = string.Format("{0}_{1}_{2:N}", "ticket", FileName, Guid.NewGuid());
                }

                return _serial;
            }

            set
            {
                _serial = value;
            }
        }

    }
}
