using System.Drawing;

namespace Workflow.Diagram {
    public class V3Line : Line {
        public V3Line() {
        }

        public override void Init(string name, Activity src, Activity dest, Point spoint, Point mpoint, Point fpoint) {
            base.Init(name, src, dest, spoint, mpoint, fpoint);
            m_StartOffset = m_Start.X - m_StartActivity.Left;
            m_FinishOffset = m_Finish.X - m_FinishActivity.Left;
            Adjust(DragType.DragNothing);
        }

        public override void Draw(Graphics g) {
            SolidBrush solidBrush = new SolidBrush(BorderColor);
            DrawLine(g);
            if (m_Start.Y < m_Finish.Y) {
                DrawDownArrow(g, (Brush)solidBrush, m_Finish);
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y + 1, 7, 6);
            } else {
                DrawUpArrow(g, (Brush)solidBrush, new Point(m_Finish.X - 1, m_Finish.Y + 1));
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y - 6, 7, 6);
            }
            g.FillRectangle((Brush)solidBrush, m_Middle.X - 3, m_Middle.Y - 3, 7, 7);
            m_Label.Draw(g);
        }

        private void DrawLine(Graphics g) {
            DrawLines(new Point[4]
            {
        new Point(m_Start.X, m_Start.Y + (m_Start.Y < m_Finish.Y ? 1 : -1)),
        new Point(m_Start.X, m_Middle.Y),
        new Point(m_Finish.X, m_Middle.Y),
        m_Finish
            }, g);
        }

        public override void Adjust(DragType drag) {
            if (drag == DragType.DragFinish) {
                if (m_Finish.Y > m_StartActivity.Top + m_StartActivity.Height / 2)
                    m_Start.Y = m_StartActivity.Bottom;
                else
                    m_Start.Y = m_StartActivity.Top;
            } else if (drag == DragType.DragStart) {
                if (m_Start.Y > m_FinishActivity.Top + m_FinishActivity.Height / 2)
                    m_Finish.Y = m_FinishActivity.Bottom;
                else
                    m_Finish.Y = m_FinishActivity.Top;
            } else {
                if (m_StartActivity.Top + m_StartActivity.Height / 2 > m_FinishActivity.Top + m_FinishActivity.Height / 2) {
                    m_Start.Y = m_StartActivity.Top;
                    m_Finish.Y = m_FinishActivity.Bottom;
                } else {
                    m_Start.Y = m_StartActivity.Bottom;
                    m_Finish.Y = m_FinishActivity.Top;
                }
                if (m_StartOffset > m_StartActivity.Width)
                    m_StartOffset = m_StartActivity.Width;
                if (m_FinishOffset > m_FinishActivity.Width)
                    m_FinishOffset = m_FinishActivity.Width;
                m_Start.X = m_StartActivity.Left + m_StartOffset;
                m_Finish.X = m_FinishActivity.Left + m_FinishOffset;
            }
            AdjustMiddle();
            m_Label.Adjust(drag);
        }

        private void AdjustMiddle() {
            if (m_MiddlePinned) {
                if ((m_Start.X <= m_Middle.X || m_Finish.X <= m_Middle.X) && (m_Start.X >= m_Middle.X || m_Finish.X >= m_Middle.X) && ((m_Start.Y <= m_Middle.Y || m_Finish.Y <= m_Middle.Y) && (m_Start.Y >= m_Middle.Y || m_Finish.Y >= m_Middle.Y)))
                    return;
                m_MiddlePinned = false;
            } else {
                m_Middle.X = (m_Finish.X - m_Start.X) / 2 + m_Start.X;
                m_Middle.Y = (m_Finish.Y - m_Start.Y) / 2 + m_Start.Y;
            }
        }

        public new bool HitMiddle(Point point) {
            return new Rectangle(m_Middle.X - 3, m_Middle.Y - 3, 6, 6).Contains(point);
        }

        public bool SetCursor(Point point) {
            return false;
        }

        public new Rectangle GetRect() {
            Rectangle rectangle = Rectangle.FromLTRB(m_Start.X, m_Start.Y, m_Finish.X, m_Finish.Y);
            rectangle.Inflate(8, 8);
            return rectangle;
        }

        private bool Hit(Point point) {
            Rectangle rectangle1 = Rectangle.FromLTRB(m_Start.X - 4, m_Start.Y, m_Start.X + 4, m_Middle.Y);
            Rectangle rectangle2 = Rectangle.FromLTRB(m_Start.X, m_Middle.Y - 4, m_Finish.X, m_Middle.Y + 4);
            Rectangle rectangle3 = Rectangle.FromLTRB(m_Finish.X - 4, m_Middle.Y, m_Finish.X + 4, m_Finish.Y);
            if (!rectangle1.Contains(point) && !rectangle2.Contains(point) && (!rectangle3.Contains(point) && !HitMiddle(point)) && (!HitStart(point) && !HitFinish(point)))
                return HitLabel(point);
            return true;
        }

        private bool HitFinish(Point point) {
            return (m_Start.Y >= m_Finish.Y ? Rectangle.FromLTRB(m_Finish.X - 3, m_Finish.Y + 1, m_Finish.X + 3, m_Finish.Y + 5) : Rectangle.FromLTRB(m_Finish.X - 3, m_Finish.Y - 5, m_Finish.X + 3, m_Finish.Y - 1)).Contains(point);
        }

        private bool HitStart(Point point) {
            return (m_Start.Y >= m_Finish.Y ? Rectangle.FromLTRB(m_Start.X - 3, m_Start.Y - 5, m_Start.X + 3, m_Start.Y - 1) : Rectangle.FromLTRB(m_Start.X - 3, m_Start.Y + 1, m_Start.X + 3, m_Start.Y + 5)).Contains(point);
        }

        private new void SetMiddlePoint(Point point) {
            if (m_Start.Y < m_Finish.Y) {
                if (point.Y < m_Start.Y + 10)
                    point.Y = m_Start.Y + 10;
                if (point.Y > m_Finish.Y - 10)
                    point.Y = m_Finish.Y - 10;
            } else {
                if (point.Y < m_Finish.Y + 10)
                    point.Y = m_Finish.Y + 10;
                if (point.Y > m_Start.Y - 10)
                    point.Y = m_Start.Y - 10;
            }
            if (m_Start.X <= m_Finish.X) {
                if (point.X < m_Start.X)
                    point.X = m_Start.X;
                if (point.X > m_Finish.X)
                    point.X = m_Finish.X;
            } else {
                if (point.X > m_Start.X)
                    point.X = m_Start.X;
                if (point.X < m_Finish.X)
                    point.X = m_Finish.X;
            }
            m_Middle = point;
        }
    }
}
