using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Email;

namespace Workflow.DataAcess.Configuration.Email
{
    public class MailAttachmentsConfiguration : EntityTypeConfiguration<MailAttachments>
    {
        public MailAttachmentsConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("FILE_ATTACHMENTS", "EMAIL");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.MailItemId).HasColumnName("MAIL_ITEM_ID");
            this.Property(t => t.FileName).HasColumnName("FILE_NAME");
            this.Property(t => t.Ext).HasColumnName("EXT");
            this.Property(t=>t.DataContent).HasColumnName("DATA_CONTENT");
            this.Ignore(t => t.Serial);

        }
    }
}
