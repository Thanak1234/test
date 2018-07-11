using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.EGM;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration.EGMAttandance
{
    public class AttandanceConfiguration : EntityTypeConfiguration<Attandance>
    {
        public AttandanceConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("ATTANDANCE", "EGM");
            this.Property(t => t.detail).HasColumnName("DETAIL");
            this.Property(t => t.remarks).HasColumnName("REMARKS");
            this.Property(t => t.created_by).HasColumnName("CREATED_BY");
            this.Property(t => t.created_date).HasColumnName("CREATED_DATE");
            this.Property(t => t.modified_by).HasColumnName("MODIFIED_BY");
            this.Property(t => t.modified_date).HasColumnName("MODIFIED_DATE");
            
        }


    }
}
