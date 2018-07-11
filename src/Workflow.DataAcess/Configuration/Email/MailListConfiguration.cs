using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Email;

namespace Workflow.DataAcess.Configuration.Email {
    public class MailListConfiguration : EntityTypeConfiguration<MailList> {

        public MailListConfiguration() {
            ToTable("MAIL_LIST", "EMAIL");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.EmailAddress).HasColumnName("EMAIL_ADDRESS");
            Property(t => t.EmailPassword).HasColumnName("EMAIL_PASSWORD");
            Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
        }
    }
}
