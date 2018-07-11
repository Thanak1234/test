using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Email;

namespace Workflow.DataAcess.Configuration.Email {

    public class FileAttachementConfiguration : EntityTypeConfiguration<FileAttachement> {

        public FileAttachementConfiguration() {
            ToTable("FILE_ATTACHMENTS", "EMAIL");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
            Property(t => t.DataContent).HasColumnName("DATA_CONTENT");
            Property(t => t.Ext).HasColumnName("EXT");
            Property(t => t.FileName).HasColumnName("FILE_NAME");
            Property(t => t.MailItemId).HasColumnName("MAIL_ITEM_ID");
            HasRequired(p => p.EmailItem).WithMany(p => p.FileAttachements).HasForeignKey(p => p.MailItemId);
            this.Ignore(t => t.Serial);
        }
    }
}
