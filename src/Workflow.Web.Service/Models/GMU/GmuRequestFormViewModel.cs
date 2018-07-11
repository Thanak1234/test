namespace Workflow.Web.Models.GMU
{
    public class GmuRequestFormViewModel : AbstractFormDataViewModel
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
        public GmuRamClearViewModel gmuRamClear { get; set; }
    }
}
