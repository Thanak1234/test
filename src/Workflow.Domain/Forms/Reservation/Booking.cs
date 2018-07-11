using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [DataContract]
    public class Booking
    {
        [IgnoreDataMember]
        private DateTime? _receiveDate;

        [IgnoreDataMember]
        private DateTime _checkInDate;

        [IgnoreDataMember]
        private DateTime _checkOutDate;

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public int RequestHeaderId { get; set; }
        
        [IgnoreDataMember]
        public virtual RequestHeader RequestHeader { get; set; }

        [DataMember(Name = "roomCategoryId")]
        public int RoomCategoryId { get; set; }

        [IgnoreDataMember]
        public virtual RoomCategory RoomCategory { get; set; }

        [DataMember(Name = "guestFullName")]
        public string GuestFullName { get; set; }

        [DataMember(Name = "passportNo")]
        public string PassportNo { get; set; }

        [DataMember(Name = "relationship")]
        public string Relationship { get; set; }

        [DataMember(Name = "extraBed")]
        public virtual byte ExtraBed { get; set; }

        [DataMember(Name = "numberOfRoom")]
        public int NumberOfRoom { get; set; }

        [DataMember(Name = "paxsAdult")]
        public int PaxsAdult { get; set; }

        [DataMember(Name = "paxsChild")]
        public int PaxsChild { get; set; }

        [DataMember(Name = "remark")]
        public string Remark { get; set; }

        [DataMember(Name = "agree")]
        public bool Agree { get; set; }

        [DataMember(Name = "termConditionId")]
        public short TermConditionId { get; set; }

        [DataMember(Name = "confirmationNumber")]
        public string ConfirmationNumber { get; set; }

        [DataMember(Name = "checkOutDate")]
        public DateTime CheckOutDate
        {
            get
            {
                return _checkOutDate;
            }
            set
            {
                _checkOutDate = value;
            }
        }

        [DataMember(Name = "checkInDate")]
        public DateTime CheckInDate
        {
            get
            {
                return _checkInDate;
            }
            set
            {
                _checkInDate = value;
            }
        }
        [DataMember(Name = "receiveDate")]
        public DateTime? ReceiveDate {
            get {
                return _receiveDate.Nullable();
            }
            set {
                _receiveDate = value.Nullable();
            }
        }
    }
}
