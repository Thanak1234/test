using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.BCJ;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Configuration.Reservation
{

    public class AttachmentConfiguration : AttachmentTypeConfiguration<FnFAttachment> {
        public AttachmentConfiguration()
        {
            this.ToTable("F_F_BOOKING_REQUEST_FILES", "RESERVATION");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
        }
    }
}
