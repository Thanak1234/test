using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Domain.Entities.Application
{
    [Table("MENU", Schema = "BPMDATA")]
    public class Navigation
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? RightId { get; set; }
        public int? ApplicationId { get; set; }
        [Column("Name")]
        public string Text { get; set; }
        public string Type { get; set; }
        public string View { get; set; }
        public string RouteId { get; set; }
        public string IconCls { get; set; }
        public bool ClosableTab { get; set; }
        public bool Leaf { get; set; }
        public int Sequence { get; set; }
        public bool Active { get; set; }

        public IEnumerable<Navigation> Children { get; set; }
    }
}
