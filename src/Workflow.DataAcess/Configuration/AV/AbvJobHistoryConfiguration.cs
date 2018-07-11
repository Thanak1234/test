using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.AV;

namespace Workflow.DataAcess.Configuration.AV
{
    public class AvbJobHistoryConfiguration : EntityTypeConfiguration<AvbJobHistory>
    {
        public AvbJobHistoryConfiguration ()
        {

            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("ACTUAL_EVENT", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ProjectName).HasColumnName("PROJECT_NAME");
            this.Property(t => t.Locaiton).HasColumnName("LOCATION");
            this.Property(t => t.SetupDate).HasColumnName("SETUP_DATE");
            this.Property(t => t.ActualDate).HasColumnName("ACTUAL_EVENT_DATE");
            this.Property(t => t.ProjectBrief).HasColumnName("PROJECT_BRIEF");
            this.Property(t => t.Other).HasColumnName("OTHER");

            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
        }
    }
}
