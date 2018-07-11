using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject.SO {

    [DataContract]
    public class ActivityInstanceDto {

        private int _ProcessInstanceID;
        private int _ActivityInstanceID;
        private string _ActivityName;
        private DateTime _StartDate;
        private DateTime _FinishDate;
        private string _Priority;
        private string _Status;
        private int _ExpectedDuration;
        private int _Duration;

        #region Constructors

        public ActivityInstanceDto() {
        }

        #endregion Constructors

        [DataMember(Name = "processInstanceID")]
        public int ProcessInstanceID {
            get {
                return _ProcessInstanceID;
            }
            set {
                _ProcessInstanceID = value;
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

        [DataMember(Name = "activityName")]
        public string ActivityName {
            get {
                return _ActivityName;
            }
            set {
                _ActivityName = value;
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

        [DataMember(Name = "finishDate")]
        public DateTime FinishDate {
            get {
                return _FinishDate;
            }
            set {
                _FinishDate = value;
            }
        }

        [DataMember(Name = "priority")]
        public string Priority {
            get {
                return _Priority;
            }
            set {
                _Priority = value;
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

        [DataMember(Name = "expectedDuration")]
        public int ExpectedDuration {
            get {
                return _ExpectedDuration;
            }
            set {
                _ExpectedDuration = value;
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
    }
}
