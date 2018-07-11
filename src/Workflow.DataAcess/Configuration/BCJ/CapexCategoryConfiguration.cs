using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BCJ;

namespace Workflow.DataAcess.Configuration.BCJ
{
    public class CapexCategoryConfiguration : EntityTypeConfiguration<CapexCategory>
    {
        public CapexCategoryConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("CAPEX_CATEGORY", "FINANCE");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("CODE");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Vouching).HasColumnName("VOUCHING");
            this.Property(t => t.DepartmentId).HasColumnName("DEPT_ID");
        }
    }

}