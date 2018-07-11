using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Entities.Forms
{
    [Table("COURSE_REGISTRATION", Schema = "HR")]
    public partial class CourseRegistration
    {
        [Key]
        public int Id { get; set; }
        public int RequestHeaderId { get; set; }
        public int OriginalCourseId { get; set; }
        public int CourseId { get; set; }
        public string OriginalCourseDate { get; set; }
        public string CourseDate { get; set; }
        public string TrainerName { get; set; }
        public string Venue { get; set; }
        public DateTime? ReminderOn { get; set; }
        public string Duration { get; set; }
    }
}
