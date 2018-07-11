


using System.ComponentModel.DataAnnotations.Schema;

using Workflow.Domain.Entities.Core;
using System.Data.Entity.ModelConfiguration;

namespace Workflow.DataAcess.Configuration
{
    public class ExpenseClaimItemDetailConfiguration : EntityTypeConfiguration<ExpenseClaimItemDetail>
    {
        public ExpenseClaimItemDetailConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.ItemId);


            // Properties

            this.Property(t => t.ExpenseName)
                .IsRequired()
                .HasMaxLength(150);


            this.Property(t => t.Descritpion)
                .HasMaxLength(150);


            this.Property(t => t.Month)
                .HasMaxLength(10);


            this.Property(t => t.Day)
                .HasMaxLength(10);


            // Table & Column Configurationpings

            this.ToTable("EXPENSE_CLAIM_ITEM_DETAIL", "FINANCE");

            this.Property(t => t.ItemId).HasColumnName("ITEM_ID");

            this.Property(t => t.HeaderId).HasColumnName("HEADER_ID");

            this.Property(t => t.DepId).HasColumnName("DEP_ID");

            this.Property(t => t.AccountId).HasColumnName("ACCOUNT_ID");

            this.Property(t => t.ExpenseName).HasColumnName("EXPENSE_NAME");

            this.Property(t => t.CurrencyId).HasColumnName("CURRENCY_ID");

            this.Property(t => t.ExchangeRate).HasColumnName("EXCHANGE_RATE");

            this.Property(t => t.Amount).HasColumnName("AMOUNT");

            this.Property(t => t.AmountInUsd).HasColumnName("AMOUNT_IN_USD");

            this.Property(t => t.Descritpion).HasColumnName("DESCRITPION");

            this.Property(t => t.Date).HasColumnName("DATE");

            this.Property(t => t.Month).HasColumnName("MONTH");

            this.Property(t => t.Day).HasColumnName("DAY");

            this.Property(t => t.Status).HasColumnName("STATUS");

        }
    }
}
