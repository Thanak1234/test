


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class AssetCategoryConfiguration : EntityTypeConfiguration<AssetCategory>
    {
        public AssetCategoryConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.AssetId);


            // Properties

            this.Property(t => t.AssetName)
                .HasMaxLength(50);


            this.Property(t => t.Description)
                .HasMaxLength(255);


            // Table & Column Configurationpings

            this.ToTable("ASSET_CATEGORY", "FINANCE");

            this.Property(t => t.AssetId).HasColumnName("ASSET_ID");

            this.Property(t => t.AssetName).HasColumnName("ASSET_NAME");

            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.Property(t => t.Status).HasColumnName("STATUS");

        }
    }
}
