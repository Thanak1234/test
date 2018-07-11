using System.Drawing;

namespace Workflow.Diagram {
    public class V1Line : Line {
        public V1Line() {
        }

        public override void Init(string name, Activity src, Activity dest, Point spoint, Point mpoint, Point fpoint) {
            base.Init(name, src, dest, spoint, mpoint, fpoint);
            m_StartOffset = m_Start.Y - m_StartActivity.Top;
            m_FinishOffset = m_Finish.X - m_FinishActivity.Left;
            Adjust(DragType.DragNothing);
        }

        public override void Draw(Graphics g) {
            SolidBrush solidBrush = new SolidBrush(BorderColor);
            DrawLine(g);
            if (m_Start.Y < m_Finish.Y)
                DrawDownArrow(g, (Brush)solidBrush, m_Finish);
            else
                DrawUpArrow(g, (Brush)solidBrush, new Point(m_Finish.X - 1, m_Finish.Y));
            if (m_Start.X < m_Finish.X)
                g.FillRectangle((Brush)solidBrush, m_Start.X + 1, m_Start.Y - 3, 6, 7);
            else
                g.FillRectangle((Brush)solidBrush, m_Start.X - 6, m_Start.Y - 3, 6, 7);
            m_Label.Draw(g);
        }

        private void DrawLine(Graphics g) {
            DrawLines(
                new Point[3]
                {
                    new Point(m_Start.X + (m_Start.X < m_Finish.X ? 1 : -1), m_Start.Y),
                    new Point(m_Finish.X, m_Start.Y),
                    m_Finish
                }, 
                g
            );
        }

        public override void Adjust(DragType drag) {
            if (drag == DragType.DragFinish) {
                if (m_Finish.X > m_StartActivity.Left + m_StartActivity.Width / 2)
                    m_Start.X = m_StartActivity.Right;
                else
                    m_Start.X = m_StartActivity.Left;
            } else if (drag == DragType.DragStart) {
                if (m_Start.Y > m_FinishActivity.Top + m_FinishActivity.Height / 2)
                    m_Finish.Y = m_FinishActivity.Bottom;
                else
                    m_Finish.Y = m_FinishActivity.Top;
            } else {
                if (m_StartOffset > m_StartActivity.Height)
                    m_StartOffset = m_StartActivity.Height;
                if (m_FinishOffset > m_FinishActivity.Width)
                    m_FinishOffset = m_FinishActivity.Width;
                m_Start.Y = m_StartActivity.Top + m_StartOffset;
                m_Finish.X = m_FinishActivity.Left + m_FinishOffset;
                if (m_Finish.X > m_StartActivity.Left + m_StartActivity.Width / 2)
                    m_Start.X = m_StartActivity.Right;
                else
                    m_Start.X = m_StartActivity.Left;
                if (m_Start.Y > m_FinishActivity.Top + m_FinishActivity.Height / 2)
                    m_Finish.Y = m_FinishActivity.Bottom;
                else
                    m_Finish.Y = m_FinishActivity.Top;
            }
            m_Label.Adjust(drag);
        }
    }
}
