


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class RihgtConfiguration : EntityTypeConfiguration<Rihgt>
    {
        public RihgtConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Name)
                .HasMaxLength(250);


            // Table & Column Configurationpings

            this.ToTable("RIHGT", "ADMIN");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.Name).HasColumnName("NAME");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
