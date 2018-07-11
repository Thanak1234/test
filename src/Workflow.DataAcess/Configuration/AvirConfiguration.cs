using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class AvirConfiguration : EntityTypeConfiguration<Avir>
    {
        public AvirConfiguration()
        {
            HasKey(t => t.Id);
            ToTable("AVIR", "EVENT");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ReceiverId).HasColumnName("RECEIVER_ID");
            Property(t => t.Location).HasColumnName("LOCATION");
            Property(t => t.IncidentDate).HasColumnName("INCIDENT_DATE");
            Property(t => t.ReporterId).HasColumnName("REPORTER_ID");
            Property(t => t.ComplaintRegarding).HasColumnName("COMPLAINT_REGARDING");
            Property(t => t.Complaint).HasColumnName("COMPLAINT");
        }
    }
}
