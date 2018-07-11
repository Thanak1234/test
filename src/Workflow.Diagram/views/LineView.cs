using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Workflow.Diagram.views {
    public class LineView {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartOffset { get; set; }
        public int FinishOffset { get; set; }
        public bool MiddlePinned { get; set; }
        public int Status { get; set; }
        public LabelView Label { get; set; }

        public string Color { get; set; }

        public List<PointView> Points { get; set; }

        public static LineView From(Line line) {
            var Points = new List<PointView>();

            if (line.LineType == LineType.V1Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X + (line.m_Start.X < line.m_Finish.X ? 1 : -1), line.m_Start.Y)));
                Points.Add(PointView.From(new Point(line.m_Finish.X, line.m_Start.Y)));
                Points.Add(PointView.From(line.m_Finish));
            } else if (line.LineType == LineType.V2Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Start.Y + (line.m_Start.Y < line.m_Middle.Y ? 1 : -1))));
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Middle.Y)));
                Points.Add(PointView.From(new Point(line.m_Finish.X, line.m_Middle.Y)));
                Points.Add(PointView.From(line.m_Finish));
            } else if (line.LineType == LineType.V3Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Start.Y + (line.m_Start.Y < line.m_Finish.Y ? 1 : -1))));
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Middle.Y)));
                Points.Add(PointView.From(new Point(line.m_Finish.X, line.m_Middle.Y)));
                Points.Add(PointView.From(line.m_Finish));
            } else if (line.LineType == LineType.H1Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Start.Y + (line.m_Start.Y < line.m_Finish.Y ? 1 : -1))));
                Points.Add(PointView.From(new Point(line.m_Start.X, line.m_Finish.Y)));
                Points.Add(PointView.From(line.m_Finish));
            } else if (line.LineType == LineType.H2Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X + (line.m_Start.X < line.m_Middle.X ? 1 : -1), line.m_Start.Y)));
                Points.Add(PointView.From(new Point(line.m_Middle.X, line.m_Start.Y)));
                Points.Add(PointView.From(new Point(line.m_Middle.X, line.m_Finish.Y)));
                Points.Add(PointView.From(line.m_Finish));
            } else if (line.LineType == LineType.H3Line) {
                Points.Add(PointView.From(new Point(line.m_Start.X + (line.m_Start.X < line.m_Start.Y ? -1 : 1), line.m_Start.Y)));
                Points.Add(PointView.From(new Point(line.m_Middle.X, line.m_Start.Y)));
                Points.Add(PointView.From(new Point(line.m_Middle.X, line.m_Finish.Y)));
                Points.Add(PointView.From(line.m_Finish));
            }

            return new LineView() {
                ID = line.m_ID,
                Name = line.m_Name,
                Description = line.m_Description,
                Points = Points,
                StartOffset = line.m_StartOffset,
                FinishOffset = line.m_FinishOffset,
                MiddlePinned = line.m_MiddlePinned,
                Label = LabelView.From(line.m_Label),
                Status = line.m_Status,
                Color = GetColor(line.m_Status)
            };
        }

        public static string GetColor(int status) {
            string color = "#607D8B";

            switch (status) {
                case 0:
                    color = "#CFD8DC";
                    break;
                case 1:
                    color = "rgb(115, 198, 0)";
                    break;
                case 2:
                    color = "rgb(255, 143, 134)";
                    break;
                default:
                    color = "rgb(144, 164, 174)";
                    break;
            }

            return color;
        }
    }
}
