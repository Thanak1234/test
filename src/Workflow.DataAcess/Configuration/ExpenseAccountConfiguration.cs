


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class ExpenseAccountConfiguration : EntityTypeConfiguration<ExpenseAccount>
    {
        public ExpenseAccountConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.ExpenseAccountId);


            // Properties

            this.Property(t => t.ExpenseAccountName)
                .HasMaxLength(50);


            this.Property(t => t.Description)
                .HasMaxLength(255);


            // Table & Column Configurationpings

            this.ToTable("EXPENSE_ACCOUNT", "FINANCE");

            this.Property(t => t.ExpenseAccountId).HasColumnName("EXPENSE_ACCOUNT_ID");

            this.Property(t => t.ExpenseAccountName).HasColumnName("EXPENSE_ACCOUNT_NAME");

            this.Property(t => t.Description).HasColumnName("DESCRIPTION");

            this.Property(t => t.Status).HasColumnName("STATUS");

        }
    }
}
