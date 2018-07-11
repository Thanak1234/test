using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Workflow.Diagram.views {
    public class ActivityView {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool bSelected { get; set; }
        public int ExpandAnimate { get; set; }
        public int X { get; set; }
        public int Width { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int ExpandStart { get; set; }
        public bool bExpand { get; set; }
        public int AnimateStart { get; set; }
        public string Description { get; set; }
        public int Instances { get; set; }
        public bool bStart { get; set; }
        public bool bMaximized { get; set; }
        public bool bTransaction { get; set; }
        public string MetaData { get; set; }
        public int ExpectedDuration { get; set; }
        public int Priority { get; set; }
        public List<EventView> Events { get; set; }
        public int Status { get; set; }
        public int ActInstID { get; set; }

        public static ActivityView From(Activity act) {
            return new ActivityView() {
                ID = act.m_ID,
                Name = act.m_Name,
                bSelected = act.m_bSelected,
                ExpandAnimate = act.m_ExpandAnimate,
                X = act.m_Rect.X,
                Y = act.m_Rect.Y,
                Width = act.m_Rect.Width,
                Height = act.m_Rect.Height,
                ExpandStart = act.m_ExpandStart,
                bExpand = act.m_bExpand,
                AnimateStart = act.m_AnimateStart,
                Description = act.m_Description,
                Instances = act.m_Instances,
                bStart = act.m_bStart,
                bMaximized = act.m_bMaximized,
                bTransaction = act.m_bTransaction,
                MetaData = act.m_MetaData,
                ExpectedDuration = act.m_ExpectedDuration,
                Priority = act.m_Priority,
                Status = act.Status,
                Events = GetEvents(act),
                ActInstID = act.ActInstID

            };
        }

        private static List<EventView> GetEvents(Activity act) {
            var events = new List<EventView>();
            foreach(Event evt in act.m_Events) {
                events.Add(EventView.From(evt));
            }
            return events;
        }
    }
}
