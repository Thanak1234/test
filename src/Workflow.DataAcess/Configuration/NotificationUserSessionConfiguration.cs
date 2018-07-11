


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class NotificationUserSessionConfiguration : EntityTypeConfiguration<NotificationUserSession>
    {
        public NotificationUserSessionConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.UserName)
                .HasMaxLength(50);


            this.Property(t => t.ConnectionId)
                .HasMaxLength(50);


            // Table & Column Configurationpings

            this.ToTable("NOTIFICATION_USER_SESSION", "ASP");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.UserName).HasColumnName("USER_NAME");

            this.Property(t => t.ConnectionId).HasColumnName("CONNECTION_ID");

            this.Property(t => t.NotificationCount).HasColumnName("NOTIFICATION_COUNT");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");

        }
    }
}
