

using System.Collections.Generic;
/**
*@author : Veasna
*/
using Workflow.Domain.Entities.Reservation;

namespace Workflow.Domain.Entities.BatchData
{
    public class CRRRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public Complimentary Complimentary { get; set; }

        public IEnumerable<Guest> Guests { get; set; }        
        public IEnumerable<Guest> DelRequestGuests { get; set; }
        public IEnumerable<Guest> EditRequestGuests { get; set; }
        public IEnumerable<Guest> AddRequestGuests { get; set; }

        public IEnumerable<ComplimentaryCheckItemExt> CheckExpenseItem { get; set; }

        public ComplimentaryCheckItemLS CheckExpense { get; set; }
    }
}
