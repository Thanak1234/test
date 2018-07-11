using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.SO {

    [DataContract]
    public class UserPerformanceDto {

        private int _DestinationID;
        private int _ActivityInstanceID;
        private string _ProcessSetFullName;
        private string _ActivityName;
        private string _DestinationUser;
        private string _DestinationUserDisplay;
        private DateTime _FinishDate;
        private string _FinalAction;
        private int _InstanceCount;
        private int _Duration;
        private int _ProcessInstanceID;
        private DateTime _StartDate;
        private string _Status;
        private string _UserCsv;
        private string _ProcessVersion;
        private DateTime _RangeStartDate;
        private DateTime _RangeEndDate;

        [DataMember(Name = "destinationID")]
        public int DestinationID {
            get {
                return _DestinationID;
            }
            set {
                _DestinationID = value;
            }
        }

        [DataMember(Name = "activityInstanceID")]
        public int ActivityInstanceID {
            get {
                return _ActivityInstanceID;
            }
            set {
                _ActivityInstanceID = value;
            }
        }

        [DataMember(Name = "processSetFullName")]
        public string ProcessSetFullName {
            get {
                return _ProcessSetFullName;
            }
            set {
                _ProcessSetFullName = value;
            }
        }

        [DataMember(Name = "activityName")]
        public string ActivityName {
            get {
                return _ActivityName;
            }
            set {
                _ActivityName = value;
            }
        }

        [DataMember(Name = "destinationUser")]
        public string DestinationUser {
            get {
                return _DestinationUser;
            }
            set {
                _DestinationUser = value;
            }
        }

        [DataMember(Name = "destinationUserDisplay")]
        public string DestinationUserDisplay {
            get {
                return _DestinationUserDisplay;
            }
            set {
                _DestinationUserDisplay = value;
            }
        }

        [DataMember(Name = "finishDate")]
        public DateTime FinishDate {
            get {
                return _FinishDate;
            }
            set {
                _FinishDate = value;
            }
        }

        [DataMember(Name = "finalAction")]
        public string FinalAction {
            get {
                return _FinalAction;
            }
            set {
                _FinalAction = value;
            }
        }

        [DataMember(Name = "instanceCount")]
        public int InstanceCount {
            get {
                return _InstanceCount;
            }
            set {
                _InstanceCount = value;
            }
        }

        [DataMember(Name = "duration")]
        public int Duration {
            get {
                return _Duration;
            }
            set {
                _Duration = value;
            }
        }

        [DataMember(Name = "processInstanceID")]
        public int ProcessInstanceID {
            get {
                return _ProcessInstanceID;
            }
            set {
                _ProcessInstanceID = value;
            }
        }

        [DataMember(Name = "startDate")]
        public DateTime StartDate {
            get {
                return _StartDate;
            }
            set {
                _StartDate = value;
            }
        }

        [DataMember(Name = "status")]
        public string Status {
            get {
                return _Status;
            }
            set {
                _Status = value;
            }
        }

        [DataMember(Name = "userCsv")]
        public string UserCsv {
            get {
                return _UserCsv;
            }
            set {
                _UserCsv = value;
            }
        }

        [DataMember(Name = "processVersion")]
        public string ProcessVersion {
            get {
                return _ProcessVersion;
            }
            set {
                _ProcessVersion = value;
            }
        }

        [DataMember(Name = "rangeStartDate")]
        public DateTime RangeStartDate {
            get {
                return _RangeStartDate;
            }
            set {
                _RangeStartDate = value;
            }
        }

        [DataMember(Name = "rangeEndDate")]
        public DateTime RangeEndDate {
            get {
                return _RangeEndDate;
            }
            set {
                _RangeEndDate = value;
            }
        }

    }
}
