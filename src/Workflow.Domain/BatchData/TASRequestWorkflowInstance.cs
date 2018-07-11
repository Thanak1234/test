/**
*@author : Yim Samaune
*/
using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Domain.Entities.BatchData
{
    public class TASCRRequestWorkflowInstance : AbstractWorkflowInstance
    {
        public CourseRegistration CourseRegistration { get; set; }

        public IEnumerable<CourseEmployee> CourseEmployees { get; set; }
        public IEnumerable<CourseEmployee> DelCourseEmployees { get; set; }
        public IEnumerable<CourseEmployee> EditCourseEmployees { get; set; }
        public IEnumerable<CourseEmployee> AddCourseEmployees { get; set; }
    }
}