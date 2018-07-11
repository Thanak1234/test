using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.Business.Ticketing.Dto
{
    public class DestUser
    {
        public enum DEST_TYPE {
            TO,
            CC,
            BCC
        }

        public Employee User{ get; set; }
        public DEST_TYPE DestType { get; set; }
    }
}
