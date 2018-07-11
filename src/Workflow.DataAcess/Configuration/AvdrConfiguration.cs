using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class AvdrConfiguration : EntityTypeConfiguration<Avdr>
    {
        public AvdrConfiguration()
        {
            this.HasKey(t => t.Id);
            this.ToTable("AVDR", "EVENT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.AT).HasColumnName("AT");
            this.Property(t => t.DCIRS).HasColumnName("DCIRS");
            this.Property(t => t.DLE).HasColumnName("DLE");
            this.Property(t => t.ECRR).HasColumnName("ECRR");
            this.Property(t => t.EIN).HasColumnName("EIN");
            this.Property(t => t.ELocation).HasColumnName("ELOCATION");
            this.Property(t => t.HEDL).HasColumnName("HEDL");
            this.Property(t => t.IncidentDate).HasColumnName("INCIDENT_DATE");
            this.Property(t => t.ReporterId).HasColumnName("REPORTER_ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.SDL).HasColumnName("SDL");
        }
    }
}
