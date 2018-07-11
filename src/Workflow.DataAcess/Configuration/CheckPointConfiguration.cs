


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class CheckPointConfiguration : EntityTypeConfiguration<CheckPoint>
    {
        public CheckPointConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);


            // Table & Column Configurationpings

            this.ToTable("CHECK_POINT", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.Code).HasColumnName("CODE");

            this.Property(t => t.Flag).HasColumnName("FLAG");

            this.Property(t => t.Last).HasColumnName("LAST");

        }
    }
}
