using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.DataObject.Employee
{
    [Table("EMPLOYEE", Schema = "HR")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Column("DEPT_ID")]
        public int TeamId { get; set; }
        [Column("LOGIN_NAME")]
        public string Account { get; set; }
        [Column("EMP_NO")]
        public string Code { get; set; }
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }
        [Column("LAST_NAME")]
        public string LastName { get; set; }
        [Column("DISPLAY_NAME")]
        public string Name { get; set; }
        [Column("JOB_TITLE")]
        public string Position { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("TELEPHONE")]
        public string ExtNum { get; set; }
        [Column("MOBILE_PHONE")]
        public string PhoneNum { get; set; }
        [Column("HIRED_DATE")]
        public DateTime JoinDate { get; set; }
        [Column("EMP_TYPE")]
        public string Type { get; set; }
    }
}