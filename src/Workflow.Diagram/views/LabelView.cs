using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Workflow.Diagram.views {
    public class LabelView {
        public PointView Rect { get; set; }
        public string Name { get; set; }
        public PointView Offset { get; set; }
        public bool bStart { get; set; }
        public bool bPos { get; set; }
        public LabelHV StartOr { get; set; }
        public LabelHV FinishOr { get; set; }        

        public static LabelView From(Label label) {            
            return new LabelView() {
                Rect = new PointView() { X = label.m_Rect.Left + 2, Y = label.m_Rect.Top + 2 },
                Name = label.m_Name,
                Offset = PointView.From(label.m_Offset),
                bStart = label.m_bStart,
                bPos = label.m_bPos,
                StartOr = label.m_StartOr,
                FinishOr = label.m_FinishOr
            };
        }
    }
}
