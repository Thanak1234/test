using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.K2Service.Cores {
    [Serializable]
    public class K2Task
    {
        private int _eventID;
        private string _name;
        private DateTime _startDate;

        public K2Task()
        { }

        public K2Task(int eventId, string name, DateTime startDate)
        {
            _eventID = eventId;
            _name = name;
            _startDate = startDate;
        }

        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
    }
}
