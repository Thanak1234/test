using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Configuration
{
    public class ActivityHistoryConfiguration : AbstractModelConfiguration<ActivityHistory>
    {
        public ActivityHistoryConfiguration()
        {

            this.Property(t => t.Activity)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ApplicationName)
                .HasMaxLength(50);

            this.Property(t => t.Approver)
                .HasMaxLength(70);

            this.Property(t => t.ApproverDisplayName)
                .HasMaxLength(100);

            this.Property(t => t.Decision)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Comments)
                .HasMaxLength(2000);


            // Table & Column Mappings

            this.ToTable("APPROVAL_COMMENT", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

            this.Property(t => t.ActInstId).HasColumnName("ACT_INST_ID");

            this.Property(t => t.Activity).HasColumnName("ACTIVITY");

            this.Property(t => t.ApplicationName).HasColumnName("APPLICATION_NAME");

            this.Property(t => t.Approver).HasColumnName("APPROVER");

            this.Property(t => t.ApproverDisplayName).HasColumnName("APPROVER_DISPLAY_NAME");

            this.Property(t => t.CreatedDate).HasColumnName("APPROVAL_DATE");

            this.Property(t => t.Decision).HasColumnName("DECISION");

            this.Property(t => t.Comments).HasColumnName("COMMENTS");

            this.Property(t => t.Version).HasColumnName("VERSION");

            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany(p => p.ActivityHistories).HasForeignKey(p => p.RequestHeaderId);
        }

    }
}
