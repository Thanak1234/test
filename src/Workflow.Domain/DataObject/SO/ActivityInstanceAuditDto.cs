using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.SO {

    [DataContract]
    public class ActivityInstanceAuditDto {

        private int _ProcessInstanceID;
        private int _ActivityInstanceID;
        private string _ProcessName;
        private string _ActivityName;
        private string _Folio;
        private string _UserName;
        private DateTime _Date;
        private string _AuditDescription;

        [DataMember(Name = "procInstID")]
        public int ProcessInstanceID    {
            get {
                return _ProcessInstanceID;
            }
            set {
                _ProcessInstanceID = value;
            }
        }

        [DataMember(Name = "actInstID")]
        public int ActivityInstanceID {
            get {
                return _ActivityInstanceID;
            }
            set {
                _ActivityInstanceID = value;
            }
        }

        [DataMember(Name = "procName")]
        public string ProcessName {
            get {
                return _ProcessName;
            }
            set {
                _ProcessName = value;
            }
        }

        [DataMember(Name = "actName")]
        public string ActivityName {
            get {
                return _ActivityName;
            }
            set {
                _ActivityName = value;
            }
        }

        [DataMember(Name = "folio")]
        public string Folio {
            get {
                return _Folio;
            }
            set {
                _Folio = value;
            }
        }

        [DataMember(Name = "userName")]
        public string UserName {
            get {
                return _UserName;
            }
            set {
                _UserName = value;
            }
        }

        [DataMember(Name = "date")]
        public DateTime Date {
            get {
                return _Date;
            }
            set {
                _Date = value;
            }
        }

        [DataMember(Name = "auditDescription")]
        public string AuditDescription {
            get {
                return _AuditDescription;
            }
            set {
                _AuditDescription = value;
            }
        }

    }
}
