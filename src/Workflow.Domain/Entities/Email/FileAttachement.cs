using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.Domain.Entities.Email {

    [DataContract]
    public class FileAttachement {

        private string _serial;

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "mailItemId")]
        public int MailItemId { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "ext")]
        public string Ext { get; set; }

        [DataMember(Name = "dataContent")]
        public byte[] DataContent { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "emailItems")]
        public virtual EmailItem EmailItem { get; set; }

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
