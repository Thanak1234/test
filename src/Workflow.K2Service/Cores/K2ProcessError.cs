using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.K2Service.Cores {
    [Serializable]
    public class K2ProcessError
    {
        private int _errorId;
        private int _procInstId;
        private string _processName;
        private string _folio;
        private string _source;
        private string _description;
        private DateTime _errorLogDate;
        private DateTime _processStartDate;
        private string _originator;
        private List<K2Task> _tasks = new List<K2Task>();
        private bool _reported = false;

        public int ErrorId
        {
            get { return _errorId; }
            set { _errorId = value; }
        }

        public int ProcInstId
        {
            get { return _procInstId; }
            set { _procInstId = value; }
        }

        public string ProcessName
        {
            get { return _processName; }
            set { _processName = value; }
        }

        public string Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime ErrorLogDate
        {
            get { return _errorLogDate; }
            set { _errorLogDate = value; }
        }

        public DateTime ProcessStartDate
        {
            get { return _processStartDate; }
            set { _processStartDate = value; }
        }

        public string Originator
        {
            get { return _originator; }
            set { _originator = value; }
        }

        public List<K2Task> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public bool Reported
        {
            get { return _reported; }
            set { _reported = value; }
        } 

        public K2ProcessError()
        { }

        public K2ProcessError(int errorId, int procInstId, string processName, string folio, string source, string description, DateTime errorLogDate, DateTime startDate, string originator)
        {
            _errorId = errorId;
            _procInstId = procInstId;
            _processName = processName;
            _folio = folio;
            _source = source;
            _description = description;
            _errorLogDate = errorLogDate;
            _processStartDate = startDate;
            _originator = originator;
        }
    }
}
