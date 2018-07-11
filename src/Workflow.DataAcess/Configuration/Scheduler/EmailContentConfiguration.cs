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
    public class EmailContentConfiguration : EntityTypeConfiguration<EmailContent>
    {

        public EmailContentConfiguration() {


            // Primary Key

            this.HasKey(t => t.Id);

            this.Property(t => t.JobId);

            this.Property(t => t.ContentType);

            this.Property(t => t.MessageBody);

            this.Property(t => t.Subject);
            this.ToTable("EMAIL_CONTENTS", "SCHEDULER");

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.JobId).HasColumnName("JOB_ID");
            this.Property(t => t.ContentType).HasColumnName("CONTENT_TYPE");
            this.Property(t => t.MessageBody).HasColumnName("MESSAGE_BODY");            
            this.Property(t => t.Subject).HasColumnName("SUBJECT");            
        }


    }
}
