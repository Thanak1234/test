using System.Collections.Generic;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Models.TAS
{
    public class TASCRRequestFormViewModel : AbstractFormDataViewModel
    {
        private DataItem _dataItem;

        public DataItem dataItem
        {
            get
            {
                return _dataItem ?? (_dataItem = new DataItem());
            }
            set { _dataItem = value; }
        }
    }

    public class DataItem
    {
        public CourseRegistrationViewModel courseRegistration { get; set; }
        public IEnumerable<CourseEmployee> courseEmployees { get; set; }
        public IEnumerable<CourseEmployee> addCourseEmployees { get; set; }
        public IEnumerable<CourseEmployee> editCourseEmployees { get; set; }
        public IEnumerable<CourseEmployee> delCourseEmployees { get; set; }
    }
}
