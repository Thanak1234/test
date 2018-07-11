using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Diagram.views {
    public class EventView {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool bSelected { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
        public int ExpectedDuration { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }

        public static EventView From(Event evt) {
            return new EventView() {
                ID = evt.m_ID,
                Name = evt.m_Name,
                bSelected = evt.m_bSelected,
                Type = evt.m_Type,
                Description = evt.m_Description,
                MetaData = evt.m_MetaData,
                ExpectedDuration = evt.m_ExpectedDuration,
                Priority = evt.m_Priority,
                Status = evt.Status
            };
        }
    }
}
