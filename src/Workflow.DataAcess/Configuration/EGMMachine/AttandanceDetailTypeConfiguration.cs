using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.EGM;
using System.Data.Entity.ModelConfiguration;
namespace Workflow.DataAcess.Configuration.EGMMachine
{
    public class AttandanceDetailTypeConfiguration : EntityTypeConfiguration<AttandanceDetailType>
    {
        public AttandanceDetailTypeConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.id);            
            //Table & Column Mapping
            this.ToTable("ATTANDANCE_DEAIL_TYPE", "EGM");
            this.Property(t => t.id).HasColumnName("ID");
            this.Property(t => t.type).HasColumnName("TYPE");
        }
    }
}
