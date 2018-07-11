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
    public class BookingConfiguration : EntityTypeConfiguration<Booking>
    {
        public BookingConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("F_F_BOOKING_REQUEST", "RESERVATION");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.RoomCategoryId).HasColumnName("ROOM_CATEGORY_ID");
            this.Property(t => t.GuestFullName).HasColumnName("GUEST_FULL_NAME");
            this.Property(t => t.PassportNo).HasColumnName("ID_PASSPORT_NO");
            this.Property(t => t.Relationship).HasColumnName("RELATIONSHIP");
            this.Property(t => t.ExtraBed).HasColumnName("EXTRA_BED");
            this.Property(t => t.NumberOfRoom).HasColumnName("NO_OF_ROOMS");
            this.Property(t => t.PaxsAdult).HasColumnName("NO_PAXS_ADULT");
            this.Property(t => t.PaxsChild).HasColumnName("NO_PAXS_CHILD");
            this.Property(t => t.Remark).HasColumnName("REMARK");
            this.Property(t => t.Agree).HasColumnName("IS_AGREED");
            this.Property(t => t.TermConditionId).HasColumnName("TERMS_CONDITIONS_ID");
            this.Property(t => t.ConfirmationNumber).HasColumnName("CONFIRMATION_NUMBER");
            this.Property(t => t.CheckOutDate).HasColumnName("CHECK_OUT_DATE");
            this.Property(t => t.CheckInDate).HasColumnName("CHECK_IN_DATE");
            this.Property(t => t.ReceiveDate).HasColumnName("RECEIVED_DATE");
            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            this.HasRequired<RoomCategory>(p => p.RoomCategory).WithMany().HasForeignKey(p => p.RoomCategoryId);
        }
    }
}