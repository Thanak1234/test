

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class ExpenseClaimItemDetail
    {

        public int ItemId { get; set; }

        public int HeaderId { get; set; }

        public int DepId { get; set; }

        public int AccountId { get; set; }

        public string ExpenseName { get; set; }

        public int CurrencyId { get; set; }

        public double ExchangeRate { get; set; }

        public double Amount { get; set; }

        public double AmountInUsd { get; set; }

        public string Descritpion { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        public Nullable<bool> Status { get; set; }

    }
}
