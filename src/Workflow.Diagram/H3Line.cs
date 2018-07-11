using System.Drawing;

namespace Workflow.Diagram {
    public class H3Line : Line {
        public H3Line() {
        }

        public override void Init(string name, Activity src, Activity dest, Point spoint, Point mpoint, Point fpoint) {
            base.Init(name, src, dest, spoint, mpoint, fpoint);
            m_StartOffset = m_Start.Y - m_StartActivity.Top;
            m_FinishOffset = m_Finish.Y - m_FinishActivity.Top;
            Adjust(DragType.DragNothing);
        }

        public override void Draw(Graphics g) {
            SolidBrush solidBrush = new SolidBrush(BorderColor);
            DrawLine(g);
            if (m_Start.X < m_Finish.X) {
                DrawRightArrow(g, (Brush)solidBrush, new Point(m_Finish.X, m_Finish.Y + 1));
                g.FillRectangle((Brush)solidBrush, m_Start.X + 1, m_Start.Y - 3, 6, 7);
            } else {
                DrawLeftArrow(g, (Brush)solidBrush, new Point(m_Finish.X + 1, m_Finish.Y + 1));
                g.FillRectangle((Brush)solidBrush, m_Start.X - 6, m_Start.Y - 3, 6, 7);
            }
            g.FillRectangle((Brush)solidBrush, m_Middle.X - 3, m_Middle.Y - 3, 7, 7);
            m_Label.Draw(g);
        }

        private void DrawLine(Graphics g) {
            DrawLines(new Point[4]
            {
        new Point(m_Start.X + (m_Start.X < m_Start.Y ? -1 : 1), m_Start.Y),
        new Point(m_Middle.X, m_Start.Y),
        new Point(m_Middle.X, m_Finish.Y),
        m_Finish
            }, g);
        }

        public override void Adjust(DragType drag) {
            if (drag == DragType.DragFinish) {
                if (m_Finish.X > m_StartActivity.Left + m_StartActivity.Width / 2)
                    m_Start.X = m_StartActivity.Right;
                else
                    m_Start.X = m_StartActivity.Left;
            } else if (drag == DragType.DragStart) {
                if (m_Start.X > m_FinishActivity.Left + m_FinishActivity.Width / 2)
                    m_Finish.X = m_FinishActivity.Right;
                else
                    m_Finish.X = m_FinishActivity.Left;
            } else {
                if (m_StartActivity.Left + m_StartActivity.Width / 2 > m_FinishActivity.Left + m_FinishActivity.Width / 2) {
                    m_Start.X = m_StartActivity.Left;
                    m_Finish.X = m_FinishActivity.Right;
                } else {
                    m_Start.X = m_StartActivity.Right;
                    m_Finish.X = m_FinishActivity.Left;
                }
                if (m_StartOffset > m_StartActivity.Height)
                    m_StartOffset = m_StartActivity.Height;
                if (m_FinishOffset > m_FinishActivity.Height)
                    m_FinishOffset = m_FinishActivity.Height;
                m_Start.Y = m_StartActivity.Top + m_StartOffset;
                m_Finish.Y = m_FinishActivity.Top + m_FinishOffset;
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
    }
}
