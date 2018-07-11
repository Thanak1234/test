using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.DataObject
{
    public class DepartmentTree
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Text { get; set; }
        public bool Checked { get; set; }
        public bool Leaf { get; set; }
        public bool Expanded { get; set; }
        public bool Active { get; set; }
        [NotMapped]
        public IEnumerable<dynamic> Children { get; set; }
    }
}
