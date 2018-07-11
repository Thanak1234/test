using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration.EGMMachine
{
    public class MachineIssueTypeConfiguration : EntityTypeConfiguration<MachineIssueType>
    {
        
        public MachineIssueTypeConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);

            //Table & Column Mapping
            this.ToTable("MACHINE_ISSUE_TYPE", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.type).HasColumnName("TYPE");
        }
    }
}
