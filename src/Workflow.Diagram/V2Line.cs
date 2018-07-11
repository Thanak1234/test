using System;
using System.Drawing;

namespace Workflow.Diagram {
    public class V2Line : Line {
        public V2Line() {
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
            if (m_Start.Y < m_Middle.Y) {
                DrawUpArrow(g, (Brush)solidBrush, new Point(m_Finish.X - 1, m_Finish.Y + 1));
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y + 1, 7, 6);
            } else {
                DrawDownArrow(g, (Brush)solidBrush, m_Finish);
                g.FillRectangle((Brush)solidBrush, m_Start.X - 3, m_Start.Y - 6, 7, 6);
            }
            g.FillRectangle((Brush)solidBrush, m_Middle.X - 3, m_Middle.Y - 3, 7, 7);
            m_Label.Draw(g);
        }

        private void DrawLine(Graphics g) {
            DrawLines(new Point[4]
            {
        new Point(m_Start.X, m_Start.Y + (m_Start.Y < m_Middle.Y ? 1 : -1)),
        new Point(m_Start.X, m_Middle.Y),
        new Point(m_Finish.X, m_Middle.Y),
        m_Finish
            }, g);
        }

        public override void Adjust(DragType drag) {
            if (drag == DragType.DragFinish) {
                if (m_Finish.Y > m_FinishActivity.Top + m_FinishActivity.Height / 2) {
                    m_Start.Y = m_StartActivity.Bottom;
                    if (!m_MiddlePinned)
                        m_Middle.Y = Math.Max(m_StartActivity.Bottom, m_Finish.Y) + 30;
                } else {
                    m_Start.Y = m_StartActivity.Top;
                    if (!m_MiddlePinned)
                        m_Middle.Y = Math.Min(m_StartActivity.Top, m_Finish.Y) - 30;
                }
            } else if (drag == DragType.DragStart) {
                if (m_Start.Y > m_StartActivity.Top + m_StartActivity.Height / 2) {
                    m_Finish.Y = m_FinishActivity.Bottom;
                    if (!m_MiddlePinned)
                        m_Middle.Y = Math.Max(m_FinishActivity.Bottom, m_Start.Y) + 30;
                } else {
                    m_Finish.Y = m_FinishActivity.Top;
                    if (!m_MiddlePinned)
                        m_Middle.Y = Math.Min(m_FinishActivity.Top, m_Start.Y) - 30;
                }
            } else {
                if (m_Middle.Y > m_Start.Y) {
                    m_Start.Y = m_StartActivity.Bottom;
                    m_Finish.Y = m_FinishActivity.Bottom;
                    if (!m_MiddlePinned) {
                        if (m_Start.Y > m_Finish.Y)
                            m_Middle.Y = m_Start.Y + 30;
                        else
                            m_Middle.Y = m_Finish.Y + 30;
                    }
                } else {
                    m_Start.Y = m_StartActivity.Top;
                    m_Finish.Y = m_FinishActivity.Top;
                    if (!m_MiddlePinned) {
                        if (m_Start.Y < m_Finish.Y)
                            m_Middle.Y = m_Start.Y - 30;
                        else
                            m_Middle.Y = m_Finish.Y - 30;
                    }
                }
                if (m_StartOffset > m_StartActivity.Width)
                    m_StartOffset = m_StartActivity.Width;
                if (m_FinishOffset > m_FinishActivity.Width)
                    m_FinishOffset = m_FinishActivity.Width;
                m_Start.X = m_StartActivity.Left + m_StartOffset;
                m_Finish.X = m_FinishActivity.Left + m_FinishOffset;
            }
            if (!m_MiddlePinned) {
                m_Middle.X = (m_Start.X + m_Finish.X) / 2;
            } else {
                if (m_Start.Y < m_Middle.Y && m_Start.Y + 30 > m_Middle.Y) {
                    m_MiddlePinned = false;
                    m_Middle.Y = m_Start.Y + 30;
                }
                if (m_Start.Y > m_Middle.Y && m_Start.Y - 30 < m_Middle.Y) {
                    m_MiddlePinned = false;
                    m_Middle.Y = m_Start.Y - 30;
                }
                if (m_Finish.Y < m_Middle.Y && m_Finish.Y + 30 > m_Middle.Y) {
                    m_MiddlePinned = false;
                    m_Middle.Y = m_Finish.Y + 30;
                }
                if (m_Finish.Y > m_Middle.Y && m_Finish.Y - 30 < m_Middle.Y) {
                    m_MiddlePinned = false;
                    m_Middle.Y = m_Finish.Y - 30;
                }
                if (m_Start.X > m_Middle.X && m_Finish.X > m_Middle.X)
                    m_MiddlePinned = false;
                if (m_Start.X < m_Middle.X && m_Finish.X < m_Middle.X)
                    m_MiddlePinned = false;
            }
            m_Label.Adjust(drag);
        }
    }
}
