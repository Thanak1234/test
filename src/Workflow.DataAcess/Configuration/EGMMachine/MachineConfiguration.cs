using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration.EGMMachine
{
    public class MachineConfiguration : EntityTypeConfiguration<Machine>
    {
        public MachineConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("MACHINE_ISSUE", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.mcid).HasColumnName("MCID");
            this.Property(t => t.gamename).HasColumnName("GAMENAME");
            this.Property(t => t.zone).HasColumnName("ZONE");
            this.Property(t => t.type).HasColumnName("TYPE");
            this.Property(t => t.remarks).HasColumnName("REMARKS");
            this.Property(t => t.request_header_id).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.created_date).HasColumnName("CREATED_DATE");
            this.Property(t => t.created_by).HasColumnName("CREATED_BY");
            this.Property(t => t.modified_date).HasColumnName("MODIFIED_DATE");
            this.Property(t => t.modified_by).HasColumnName("MODIFIED_BY");
        }
    }
}
