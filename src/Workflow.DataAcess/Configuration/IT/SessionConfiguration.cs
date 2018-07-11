using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IT;

namespace Workflow.DataAcess.Configuration.IT
{
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.ToTable("DEPT_SESSION", "IT");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.SessionName).HasColumnName("DEPT_SESSION_NAME");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

        }
    }
}
