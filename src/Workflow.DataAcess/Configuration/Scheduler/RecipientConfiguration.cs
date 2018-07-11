using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Scheduler;

namespace Workflow.DataAcess.Configuration.Scheduler
{
    public class RecipientConfiguration : EntityTypeConfiguration<Recipient>
    {

        public RecipientConfiguration() {


            // Primary Key

            this.HasKey(t => t.Id);

            this.Property(t => t.EmailId);

            this.Property(t => t.Email);

            this.Property(t => t.Name);

            this.Property(t => t.Type);

            this.ToTable("RECIPIENT_LIST", "SCHEDULER");

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.EmailId).HasColumnName("EMAIL_ID");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Type).HasColumnName("TYPE");
        }


    }
}
