using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketFileUploadConfiguration : AbstractModelConfiguration<TicketFileUpload>
    {
        public TicketFileUploadConfiguration()
        {
            this.ToTable("FILE_UPLOAD", "TICKET");
            this.Property(t => t.ActivityId).HasColumnName("ACTIVITY_ID");
            this.Property(t => t.FileName).HasColumnName("FILE_NAME");
            this.Property(t => t.Ext).HasColumnName("EXT");
            this.Property(t => t.UploadSerial).HasColumnName("UPLOAD_SERIAL");
        }
    }
}
