using System.Drawing;

namespace Workflow.Diagram {
    public class H1Line : Line {
        public H1Line() {
        }

        public override void Init(string name, Activity src, Activity dest, Point spoint, Point mpoint, Point fpoint) {
            base.Init(name, src, dest, spoint, mpoint, fpoint);
            m_StartOffset = m_Start.X - m_StartActivity.Left;
            m_FinishOffset = m_Finish.Y - m_FinishActivity.Top;
            Adjust(DragType.DragNothing);
        }

        public override void Draw(Graphics g) {
            SolidBrush solidBrush = new SolidBrush(BorderColor);
            DrawLine(g);
            if (m_Start.X < m_Finish.X)
                DrawRightArrow(g, (Brush)solidBrush, new Point(m_Finish.X, m_Finish.Y + 1));
            else
                DrawLeftArrow(g, (Brush)solidBrush, new Point(m_Finish.X + 1, m_Finish.Y + 1));
            if (m_Start.Y < m_Finish.Y)
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y + 1, 7, 6);
            else
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y - 6, 7, 6);
            m_Label.Draw(g);
        }

        private void DrawLine(Graphics g) {
            DrawLines(new Point[3]
            {
        new Point(m_Start.X, m_Start.Y + (m_Start.Y < m_Finish.Y ? 1 : -1)),
        new Point(m_Start.X, m_Finish.Y),
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
                if (m_Start.X > m_FinishActivity.Left + m_FinishActivity.Width / 2)
                    m_Finish.X = m_FinishActivity.Right;
                else
                    m_Finish.X = m_FinishActivity.Left;
            } else {
                if (m_StartOffset > m_StartActivity.Width)
                    m_StartOffset = m_StartActivity.Width;
                if (m_FinishOffset > m_FinishActivity.Height)
                    m_FinishOffset = m_FinishActivity.Height;
                m_Start.X = m_StartActivity.Left + m_StartOffset;
                m_Finish.Y = m_FinishActivity.Top + m_FinishOffset;
                if (m_Finish.Y > m_StartActivity.Top + m_StartActivity.Height / 2)
                    m_Start.Y = m_StartActivity.Bottom;
                else
                    m_Start.Y = m_StartActivity.Top;
                if (m_Start.X > m_FinishActivity.Left + m_FinishActivity.Width / 2)
                    m_Finish.X = m_FinishActivity.Right;
                else
                    m_Finish.X = m_FinishActivity.Left;
            }
            m_Label.Adjust(drag);
        }
    }
}
