using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.Reservation
{
    [DataContract]
    public class Complimentary
    {
        [IgnoreDataMember]
        private DateTime? _departureDate;

        [IgnoreDataMember]
        private DateTime? _arrivalDate;
        
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public int RequestHeaderId { get; set; }
        
        [IgnoreDataMember]
        public virtual RequestHeader RequestHeader { get; set; }

        [DataMember(Name = "roomCategoryId")]
        public int RoomCategoryId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup RoomCategory { get; set; }

        [DataMember(Name = "vipStatusId")]
        public int VipStatusId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup VipStatus { get; set; }

        [DataMember(Name = "purposeId")]
        public int PurposeId { get; set; }

        [IgnoreDataMember]
        public virtual Lookup Purpose { get; set; }
        

        [DataMember(Name = "arrivalFlightDetail")]
        public string ArrivalFlightDetail { get; set; }

        [DataMember(Name = "arrivalTransfer")]
        public bool ArrivalTransfer { get; set; }

        [DataMember(Name = "arrivalVehicleTypeId")]
        public int ArrivalVehicleTypeId { get; set; }

        [DataMember(Name = "departureFlightDetail")]
        public string DepartureFlightDetail { get; set; }

        [DataMember(Name = "departureTransfer")]
        public bool DepartureTransfer { get; set; }

        [DataMember(Name = "departureVehicleTypeId")]
        public int DepartureVehicleTypeId { get; set; }

        [DataMember(Name = "roomCharge")]
        public bool RoomCharge { get; set; }

        [DataMember(Name = "room")]
        public int Room { get; set; }

        [DataMember(Name = "adult")]
        public int Adult { get; set; }

        [DataMember(Name = "childrent")]
        public int Childrent { get; set; }

        [DataMember(Name = "extraBed")]
        public bool ExtraBed { get; set; }

        [DataMember(Name = "specialRequest")]
        public string SpecialRequest { get; set; }

        [DataMember(Name = "remark")]
        public string Remark { get; set; }
        
        [DataMember(Name = "departmentIncharge")]
        public string DepartmentIncharge { get; set; }

        [DataMember(Name = "confirmationNumber")]
        public string ConfirmationNumber { get; set; }

        [DataMember(Name = "arrivalDate")]
        public DateTime? ArrivalDate
        {
            get
            {
                return _arrivalDate.Nullable();
            }
            set
            {
                _arrivalDate = value.Nullable();
            }
        }
        [DataMember(Name = "departureDate")]
        public DateTime? DepartureDate
        {
            get {
                return _departureDate.Nullable();
            }
            set {
                _departureDate = value.Nullable();
            }
        }

        [DataMember(Name = "RoomSubCategoryId")]
        public int RoomSubCategoryId
        {
            get;set;
        }
    }
}
