namespace Workflow.Web.Models.JRAM
{
    public class JRAMRequestFormViewModel : AbstractFormDataViewModel
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
        public RamClearViewModel ramClear { get; set; }
    }
}
