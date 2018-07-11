


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class DeptHistoryConfiguration : EntityTypeConfiguration<DeptHistory>
    {
        public DeptHistoryConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            // Table & Column Configurationpings

            this.ToTable("DEPT_HISTORY", "BPMDATA");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");

            this.Property(t => t.ParticipantActionId).HasColumnName("PARTICIPANT_ACTION_ID");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");

        }
    }
}
