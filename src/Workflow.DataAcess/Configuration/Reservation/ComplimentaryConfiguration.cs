using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Configuration.Reservation
{
    public class ComplimentaryConfiguration : EntityTypeConfiguration<Complimentary>
    {
        public ComplimentaryConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("COMPLIMENTARY_ROOM", "RESERVATION");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.RoomCategoryId).HasColumnName("ROOM_CATEGORY_ID");
            this.Property(t => t.RoomSubCategoryId).HasColumnName("ROOM_SUB_CATEGORY_ID");
            this.Property(t => t.VipStatusId).HasColumnName("VIP_STATUS_ID");
            this.Property(t => t.PurposeId).HasColumnName("PURPOSE_ID");
            
            this.Property(t => t.ArrivalFlightDetail).HasColumnName("ARRIVAL_FLIGHT_DETAIL");
            this.Property(t => t.ArrivalTransfer).HasColumnName("ARRIVAL_TRANSFER");
            this.Property(t => t.ArrivalVehicleTypeId).HasColumnName("ARRIVAL_VEHICLE_TYPE_ID");
            this.Property(t => t.DepartureFlightDetail).HasColumnName("DEPARTURE_FLIGHT_DETAIL");
            this.Property(t => t.DepartureTransfer).HasColumnName("DEPARTURE_TRANSFER");
            this.Property(t => t.DepartureVehicleTypeId).HasColumnName("DEPARTURE_VEHICLE_TYPE_ID");
            this.Property(t => t.RoomCharge).HasColumnName("ROOM_CHARGE");
            this.Property(t => t.Room).HasColumnName("ROOM");
            this.Property(t => t.Adult).HasColumnName("ADULT");
            this.Property(t => t.Childrent).HasColumnName("CHILDREN");
            this.Property(t => t.RoomCharge).HasColumnName("ROOM_CHARGE");
            this.Property(t => t.DepartmentIncharge).HasColumnName("DEPARTMENT_INCHARGE");
            this.Property(t => t.SpecialRequest).HasColumnName("SPECIAL_REQUEST");
            this.Property(t => t.Remark).HasColumnName("REMARK");
            this.Property(t => t.ExtraBed).HasColumnName("EXTRA_BED");
            this.Property(t => t.ConfirmationNumber).HasColumnName("CONFIRMATION_NUMBER");
            this.Property(t => t.ArrivalDate).HasColumnName("ARRIVAL_DATE");
            this.Property(t => t.DepartureDate).HasColumnName("DEPARTURE_DATE");

            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            this.HasRequired<Lookup>(p => p.RoomCategory).WithMany().HasForeignKey(p => p.RoomCategoryId);
            this.HasRequired<Lookup>(p => p.VipStatus).WithMany().HasForeignKey(p => p.VipStatusId);
            this.HasRequired<Lookup>(p => p.Purpose).WithMany().HasForeignKey(p => p.PurposeId);
        }
    }
}