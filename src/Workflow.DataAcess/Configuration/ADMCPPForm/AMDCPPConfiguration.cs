using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.ADMCPPForm;

namespace Workflow.DataAcess.Configuration.ADMCPPForm {
    public class ADMCPPConfiguration : EntityTypeConfiguration<ADMCPP> {

        public ADMCPPConfiguration() {
            ToTable("ADMCPP", "HR");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Color).HasColumnName("COLOR");
            Property(t => t.CPSN).HasColumnName("CPSN");
            Property(t => t.IssueDate).HasColumnName("ISSUE_DATE");
            Property(t => t.Model).HasColumnName("MODEL");
            Property(t => t.PlateNo).HasColumnName("PLATE_NO");
            Property(t => t.Remark).HasColumnName("REMARK");
        }
    }
}
