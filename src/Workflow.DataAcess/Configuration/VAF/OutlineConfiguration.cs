using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.VAF;

namespace Workflow.DataAcess.Configuration.VAF {
    public class OutlineConfiguration: EntityTypeConfiguration<Outline> {
        public OutlineConfiguration() {
            HasKey(t => t.Id);
            ToTable("VAF_OUTLINE", "FINANCE");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ProcessInstanceId).HasColumnName("PROCESS_INSTANCE_ID");
            Property(t => t.IncidentRptRef).HasColumnName("INCIDENT_RPT_REF");
            Property(t => t.McidLocn).HasColumnName("MCID_LOCN");
            Property(t => t.RptComparison).HasColumnName("RPT_COMPARISON");
            Property(t => t.Subject).HasColumnName("SUBJECT");
            Property(t => t.VarianceType).HasColumnName("VARIANCE_TYPE");
            Property(t => t.Amount).HasColumnName("AMOUNT");
            Property(t => t.Area).HasColumnName("AREA");
            Property(t => t.GamingDate).HasColumnName("GAMING_DATE");
            Property(t => t.Comment).HasColumnName("COMMENT");
        }
    }
}
