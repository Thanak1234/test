

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class ExpenseAccount
    {

        public int ExpenseAccountId { get; set; }

        public string ExpenseAccountName { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Status { get; set; }

    }
}
