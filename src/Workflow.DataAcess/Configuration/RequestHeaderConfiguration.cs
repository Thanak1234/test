using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class RequestHeaderConfiguration : EntityTypeConfiguration<RequestHeader>
    {
        
        public RequestHeaderConfiguration()
        {
            // Properties

            this.Property(t => t.Title)
                .HasMaxLength(100);


            this.Property(t => t.RequestCode)
                .HasMaxLength(50);


            this.Property(t => t.SubmittedBy)
                .HasMaxLength(50);


            this.Property(t => t.Status)
                .HasMaxLength(50);


            this.Property(t => t.LastActivity)
                .HasMaxLength(100);


            this.Property(t => t.LastActionBy)
                .HasMaxLength(50);


            // Table & Column Mappings

            this.ToTable("REQUEST_HEADER", "BPMDATA");

            //this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.ProcessInstanceId).HasColumnName("PROCESS_INSTANCE_ID");

            this.Property(t => t.RequestorId).HasColumnName("REQUESTOR");

            this.Property(t => t.Title).HasColumnName("TITLE");

            this.Property(t => t.RequestCode).HasColumnName("REQUEST_CODE");

            this.Property(t => t.Priority).HasColumnName("PRIORITY");

            this.Property(t => t.SubmittedBy).HasColumnName("SUBMITTED_BY");

            this.Property(t => t.CreatedDate).HasColumnName("SUBMITTED_DATE");

            this.Property(t => t.Status).HasColumnName("STATUS");

            this.Property(t => t.LastActivity).HasColumnName("LAST_ACTIVITY");

            this.Property(t => t.LastActionDate).HasColumnName("LAST_ACTION_DATE");

            this.Property(t => t.LastActionBy).HasColumnName("LAST_ACTION_BY");

            this.Property(t => t.NoneK2).HasColumnName("NONE_K2");

            this.Property(t => t.Version).HasColumnName("VERSION");

            this.Property(t => t.CurrentActivity).HasColumnName("CURRENT_ACTIVITY");

            this.HasMany<ActivityHistory>(p => p.ActivityHistories)
                .WithRequired(p => p.RequestHeader)
                .HasForeignKey(p => p.RequestHeaderId);

            this.HasRequired<Employee>(p => p.Requestor).WithMany().HasForeignKey(p=>p.RequestorId);

        }


    }
}
