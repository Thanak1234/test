using System.Drawing;
using System.Drawing.Text;

namespace Workflow.Diagram {
    public class Label {
        public Line m_Line;
        public Rectangle m_Rect;
        public string m_Name;
        public Point m_Offset;
        public bool m_bStart;
        public bool m_bPos;
        public LabelHV m_StartOr;
        public LabelHV m_FinishOr;

        public Label() {
        }

        private void Init(Line line, LabelHV startOr, LabelHV finishOr) {
            m_Line = line;
            m_StartOr = startOr;
            m_FinishOr = finishOr;
        }

        public void Draw(Graphics g) {
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.DrawString(m_Name, new Font("Tahoma", 7f, FontStyle.Bold), (Brush)new SolidBrush(SystemColors.WindowText), m_Rect.Left + 2, m_Rect.Top + 2);
        }

        public void Adjust(DragType drag) {
            LabelHV labelHv;
            Point point;
            Rectangle rect;
            if (m_bStart) {
                labelHv = m_StartOr;
                point = m_Line.m_Start;
                rect = m_Line.m_StartActivity.GetRect();
            } else {
                labelHv = m_FinishOr;
                point = m_Line.m_Finish;
                rect = m_Line.m_FinishActivity.GetRect();
            }
            if (labelHv == LabelHV.Hor) {
                if (point.X == rect.Left && m_bPos && drag == DragType.DragNothing) {
                    m_Offset.X += m_Rect.Width;
                    m_Offset.X = -m_Offset.X;
                    m_bPos = false;
                }
                if (point.X == rect.Right && !m_bPos && drag == DragType.DragNothing) {
                    m_Offset.X += m_Rect.Width;
                    m_Offset.X = -m_Offset.X;
                    m_bPos = true;
                }
            } else {
                if (point.Y == rect.Top && m_bPos && drag == DragType.DragNothing) {
                    m_Offset.Y += m_Rect.Height;
                    m_Offset.Y = -m_Offset.Y;
                    m_bPos = false;
                }
                if (point.Y == rect.Bottom && !m_bPos && drag == DragType.DragNothing) {
                    m_Offset.Y += m_Rect.Height;
                    m_Offset.Y = -m_Offset.Y;
                    m_bPos = true;
                }
            }
            m_Rect = new Rectangle(point.X, point.Y, point.X + m_Rect.Width, point.Y + m_Rect.Height);
            m_Rect.Offset(m_Offset);
        }

        private void SetTopLeft(Point point) {
            Point pos = new Point(point.X - m_Rect.Left, point.Y - m_Rect.Top);
            m_Offset.Offset(pos.X, pos.Y);
            m_Rect.Offset(pos);
        }

        private int sqr(int x) {
            return x * x;
        }

        private void CheckAnchor() {
            Point point1 = m_Line.m_Start;
            Point point2 = m_Line.m_Finish;
            Point point3 = new Point(m_Rect.Left + m_Rect.Width / 2, m_Rect.Top + m_Rect.Height / 2);
            Point point4 = new Point(m_Rect.Left, m_Rect.Top);
            float num = (float)(sqr(point3.X - point1.X) + sqr(point3.Y - point1.X) - (sqr(point3.X - point2.X) + sqr(point3.Y - point2.Y)));
            if ((double)num < 0.0 && !m_bStart) {
                Rectangle rect = m_Line.m_StartActivity.GetRect();
                m_bStart = true;
                m_Offset = new Point(point4.X - point1.X, point4.Y - point1.Y);
                m_bPos = m_StartOr != LabelHV.Hor ? point1.Y != rect.Top : point1.X != rect.Left;
            }
            if ((double)num <= 0.0 || !m_bStart)
                return;
            Rectangle rect1 = m_Line.m_FinishActivity.GetRect();
            m_bStart = false;
            m_Offset = new Point(point4.X - point2.X, point4.Y - point2.Y);
            if (m_FinishOr == LabelHV.Hor) {
                if (point2.X == rect1.Left)
                    m_bPos = false;
                else
                    m_bPos = true;
            } else if (point2.Y == rect1.Top)
                m_bPos = false;
            else
                m_bPos = true;
        }
    }
}
