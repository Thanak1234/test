using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.INCIDENT;

namespace Workflow.DataAcess.Configuration.Incident
{
    public class IncidentConfiguration : EntityTypeConfiguration<Workflow.Domain.Entities.INCIDENT.Incident>
    {
        
        public IncidentConfiguration()
        {
            //Primary key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("INCIDENT", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.requestheaderid).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.mcid).HasColumnName("MCID");
            this.Property(t => t.gamename).HasColumnName("GAMENAME");
            this.Property(t => t.zone).HasColumnName("ZONE");
            this.Property(t => t.customername).HasColumnName("CUSTOMERNAME");
            this.Property(t => t.cctv).HasColumnName("CCTV");
            this.Property(t => t.subject).HasColumnName("SUBJECT");
            this.Property(t => t.outline).HasColumnName("OUTLINE");
            this.Property(t => t.remarks).HasColumnName("REMARKS");
            this.Property(t => t.created_date).HasColumnName("CREATED_DATE");
            this.Property(t => t.created_by).HasColumnName("CREATED_BY");
            this.Property(t => t.modified_date).HasColumnName("MODIFIED_DATE");
            this.Property(t => t.modified_by).HasColumnName("MODIFIED_BY");
        }

    }
}
