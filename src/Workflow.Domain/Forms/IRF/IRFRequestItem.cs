using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities.IRF {
    public class IRFRequestItem {
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public string ItemName { get; set; }
        public string ItemModel { get; set; }
        public string SerialNo { get; set; }
        public string PartNo { get; set; }
        public int Qty { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Remark { get; set; }
    }
}
