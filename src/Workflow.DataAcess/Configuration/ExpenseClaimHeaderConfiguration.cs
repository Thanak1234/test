


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class ExpenseClaimHeaderConfiguration : EntityTypeConfiguration<ExpenseClaimHeader>
    {
        public ExpenseClaimHeaderConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);


            // Properties

            this.Property(t => t.Descritpion)
                .HasMaxLength(150);


            // Table & Column Configurationpings

            this.ToTable("EXPENSE_CLAIM_HEADER", "FINANCE");

            this.Property(t => t.Id).HasColumnName("ID");

            this.Property(t => t.RequestorId).HasColumnName("REQUESTOR_ID");

            this.Property(t => t.EmpId).HasColumnName("EMP_ID");

            this.Property(t => t.AdvanceFromOffice).HasColumnName("ADVANCE_FROM_OFFICE");

            this.Property(t => t.ReturedToOffice).HasColumnName("RETURED_TO_OFFICE");

            this.Property(t => t.TotalExpense).HasColumnName("TOTAL_EXPENSE");

            this.Property(t => t.Descritpion).HasColumnName("DESCRITPION");

            this.Property(t => t.Status).HasColumnName("STATUS");

            this.Property(t => t.DateFrom).HasColumnName("DATE_FROM");

            this.Property(t => t.DateTo).HasColumnName("DATE_TO");

        }
    }
}
