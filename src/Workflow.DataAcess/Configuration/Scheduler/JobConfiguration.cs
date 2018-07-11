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
    public class JobConfiguration : EntityTypeConfiguration<Job>
    {

        public JobConfiguration() {


            // Primary Key

            this.HasKey(t => t.Id);

            this.Property(t => t.Name);

            this.Property(t => t.StartDate);

            this.Property(t => t.CronExpression);

            this.Property(t => t.IsActive);

            this.Property(t => t.Status);

            this.Property(t => t.KeyValue);

            this.ToTable("JOBS", "SCHEDULER");

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.StartDate).HasColumnName("START_DATE");
            this.Property(t => t.CronExpression).HasColumnName("CRON_EXPRESSION");
            this.Property(t => t.IsActive).HasColumnName("IS_ACTIVE");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.KeyValue).HasColumnName("KEY_VALUE");
        }


    }
}
