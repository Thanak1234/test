

using System;
using System.Collections.Generic;

namespace Workflow.Domain.Entities.Core
{
    public partial class ExpenseClaimHeader
    {

        public int Id { get; set; }

        public int RequestorId { get; set; }

        public Nullable<int> EmpId { get; set; }

        public Nullable<double> AdvanceFromOffice { get; set; }

        public Nullable<double> ReturedToOffice { get; set; }

        public Nullable<double> TotalExpense { get; set; }

        public string Descritpion { get; set; }

        public Nullable<bool> Status { get; set; }

        public Nullable<System.DateTime> DateFrom { get; set; }

        public Nullable<System.DateTime> DateTo { get; set; }

    }
}
