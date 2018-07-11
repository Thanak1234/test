


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class AdGroupConfiguration : EntityTypeConfiguration<AdGroup>
    {
        public AdGroupConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Name)
                .HasMaxLength(250);


            this.Property(t => t.DisplayName)
                .HasMaxLength(250);


            this.Property(t => t.Description)
                .HasMaxLength(500);


            // Table & Column Configurationpings

            this.ToTable("AD_GROUP", "HR");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.Name).HasColumnName("NAME");

            this.Property(t => t.DisplayName).HasColumnName("DISPLAY_NAME");

            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
