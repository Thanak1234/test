using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.DataObject.Employee
{
    [Table("EMPLOYEE_APPROVER", Schema = "HR")]
    public class EmployeeApprover
    {
        [Key]
        public int Id { get; set; }
        [Column("EMP_ID")]
        public int EmployeeId { get; set; }
        [Column("APPROVER_ID")]
        public int ApproverId { get; set; }
        [Column("ACT_IDS")]
        public string ActIds { get; set; }
        [Column("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("MODIFIED_DATE")]
        public DateTime ModifiedDate { get; set; }
        [Column("DELETED_DATE")]
        public DateTime DeletedDate { get; set; }
        [Column("CREATED_BY")]
        public string CreatedBy { get; set; }
        [Column("MODIFIED_BY")]
        public string ModifiedBy { get; set; }
        [Column("DELETED_BY")]
        public string DeletedBy { get; set; }

    }
}