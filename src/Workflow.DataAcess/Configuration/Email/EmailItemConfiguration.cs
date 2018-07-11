using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Email;

namespace Workflow.DataAcess.Configuration.Email {
    public class EmailItemConfiguration : EntityTypeConfiguration<EmailItem> {

        public EmailItemConfiguration() {
            ToTable("MAIL_ITEM", "EMAIL");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Body).HasColumnName("BODY");
            Property(t => t.Cc).HasColumnName("CC");
            Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
            Property(t => t.Description).HasColumnName("DESCRIPTION");
            Property(t => t.Directory).HasColumnName("DIRECTORY");
            Property(t => t.Originator).HasColumnName("ORIGINATOR");
            Property(t => t.Receipient).HasColumnName("RECEIPIENT");
            Property(t => t.Status).HasColumnName("STATUS");
            Property(t => t.Subject).HasColumnName("SUBJECT");
            Property(t => t.UniqueIdentifier).HasColumnName("UNIQUE_IDENTIFIER");

            HasMany(p => p.FileAttachements)
                .WithRequired(p => p.EmailItem)
                .HasForeignKey(p => p.MailItemId);
        }
    }
}
