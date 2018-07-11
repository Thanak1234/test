using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Workflow.Diagram {
    public class Activity {
        public int m_ID;
        public string m_Name;
        public Rectangle m_Rect;
        public Activities m_Activities;
        public Events m_Events;
        public Icon m_Icon;
        public bool m_bSelected;
        private Lines m_StartLines;
        private Lines m_FinishLines;
        public int m_ExpandAnimate;
        public int m_ExpandStart;
        public bool m_bExpand;
        public int m_AnimateStart;
        public string m_Description;
        public int m_Instances;
        public bool m_bStart;
        public bool m_bMaximized;
        public bool m_bTransaction;
        public string m_MetaData;
        public int m_ExpectedDuration;
        public int m_Priority;

        public int Status { get; set; }
        public int ActInstID { get; set; }

        public int Left
        {
            get
            {
                return m_Rect.Left;
            }
        }

        public int Right
        {
            get
            {
                return m_Rect.Right;
            }
        }

        public int Top
        {
            get
            {
                return m_Rect.Top;
            }
        }

        public int Bottom
        {
            get
            {
                return GetRect().Bottom;
            }
        }

        public int Height
        {
            get
            {
                return GetRect().Height;
            }
        }

        public int Width
        {
            get
            {
                return GetRect().Width;
            }
        }

        public Activity() {
            m_Events = new Events();
            m_StartLines = new Lines();
            m_FinishLines = new Lines();
            m_bStart = false;
        }

        public void Draw(Graphics g) {
            int num = (int)Control.MouseButtons;
            
            Pen pen = new Pen(Color.FromArgb(128, 128, 128), 1f);
            
            if (m_bSelected)
                DrawSelected(g);
            else
                DrawUnselected(g);
            
            Rectangle titleRect = GetTitleRect();
            Rectangle rectangle = Rectangle.FromLTRB(titleRect.Left + 44, titleRect.Top + 4, titleRect.Right - 2, titleRect.Bottom);
            
            StringFormat format = new StringFormat();
            format.FormatFlags = format.FormatFlags | StringFormatFlags.NoWrap;
            format.Trimming = StringTrimming.EllipsisCharacter;
            g.DrawString(m_Name, new Font("Tahoma", 8f, FontStyle.Bold), (Brush)new SolidBrush(SystemColors.WindowText), (RectangleF)rectangle, format);
            if (!m_bStart) {
                if (m_bMaximized) {
                    for (int index = 0; index < 4; ++index)
                        g.DrawLine(pen, m_Rect.Right - (12 - index), m_Rect.Top + 35 - index, m_Rect.Right - (5 + index), m_Rect.Top + 35 - index);
                } else {
                    for (int index = 0; index < 4; ++index)
                        g.DrawLine(pen, m_Rect.Right - (12 - index), m_Rect.Top + 32 + index, m_Rect.Right - (5 + index), m_Rect.Top + 32 + index);
                }
            }
            RectShadow.DrawShade(g, GetRect());
            
        }

        private void DrawSelected(Graphics g) {
            Rectangle rect = GetRect();
            Rectangle titleRect = GetTitleRect();
            g.FillRectangle(Brushes.Red, rect);
            g.FillRectangle(Brushes.Red, titleRect);
            Pen pen = new Pen(Color.FromArgb(128, 128, 128), 1f);
            g.DrawRectangle(pen, rect);
            g.DrawRectangle(pen, titleRect);
            ControlPaint.DrawImageDisabled(g, (Image)m_Icon.ToBitmap(), rect.Left, rect.Top, Color.Transparent);
            g.DrawIcon(m_Icon, rect.Left + 3, rect.Top + 3);
            DrawEvents(g);
        }

        private void DrawUnselected(Graphics g) {
            
            Rectangle rect = GetRect();
            Rectangle titleRect = GetTitleRect();
            LinearGradientBrush lgb = new LinearGradientBrush(rect, Color.LightGray, Color.White, 0f, true);
            if (Status == 2) {
                lgb = new LinearGradientBrush(rect, Color.LightSkyBlue, Color.White, 0f, true);
                g.FillRectangle(lgb, rect);
            } else if (Status == 4) {
                lgb = new LinearGradientBrush(rect, Color.LightGreen, Color.White, 0f, true);
                g.FillRectangle(lgb, rect);
            } else {
                lgb = new LinearGradientBrush(rect, Color.FromArgb(207, 216, 220), Color.White, 0f, true);
                g.FillRectangle(lgb, rect);
            }
                

            Pen pen = new Pen(Color.FromArgb(128, 128, 128), 1f);
            g.DrawRectangle(pen, rect);
            g.DrawRectangle(pen, titleRect);
            g.DrawIcon(m_Icon, rect.Left + 4, rect.Top + 4);
            DrawEvents(g);
        }

        private void DrawEvents(Graphics g) {
            Rectangle rectangle = new Rectangle(m_Rect.Left, GetTitleRect().Bottom, m_Rect.Right, m_Rect.Bottom);
            if (!m_bMaximized)
                return;
            foreach (Event mEvent in m_Events)
                mEvent.Draw(g);
        }

        public Rectangle GetRect() {
            if (!m_bMaximized)
                return GetTitleRect();
            return m_Rect;
        }

        public Rectangle GetTitleRect() {
            return Rectangle.FromLTRB(m_Rect.Left, m_Rect.Top, m_Rect.Right, m_Rect.Top + 40);
        }

        public bool Hit(Point point) {
            return Rectangle.Inflate(GetRect(), 1, 1).Contains(point);
        }

        public bool HitLeft(Point point) {
            Rectangle rect = GetRect();
            return new Rectangle(rect.Left - 1, rect.Top + 1, rect.Left + 3, rect.Bottom - 1).Contains(point);
        }

        public bool HitTop(Point point) {
            Rectangle rect = GetRect();
            return new Rectangle(rect.Left, rect.Top - 1, rect.Right, rect.Top + 3).Contains(point);
        }

        public bool HitRight(Point point) {
            Rectangle rect = GetRect();
            Rectangle rectangle = new Rectangle(rect.Right - 2, rect.Top, rect.Right + 1, rect.Bottom);
            return rect.Contains(point);
        }

        public bool HitBottom(Point point) {
            Rectangle rect = GetRect();
            return new Rectangle(rect.Left, rect.Bottom - 3, rect.Right, rect.Bottom + 1).Contains(point);
        }

        public bool HitMinMax(Point point) {
            if (m_bStart)
                return false;
            return GetMinMaxRect().Contains(point);
        }

        public void AddStartLine(Line line) {
            m_StartLines.Add(line);
        }

        public void AddFinishLine(Line line) {
            m_FinishLines.Add(line);
        }

        private Rectangle JoinRect(Rectangle r1, Rectangle r2) {
            return new Rectangle(Math.Max(r1.Left, r2.Left), Math.Max(r1.Top, r2.Top), Math.Max(r1.Right, r2.Right), Math.Max(r1.Bottom, r2.Bottom));
        }

        public Rectangle AdjustLines() {
            Rectangle a = Rectangle.Empty;
            foreach (Line mStartLine in m_StartLines) {
                a = Rectangle.Union(a, mStartLine.GetRect());
                mStartLine.Adjust(DragType.DragNothing);
                a = Rectangle.Union(a, mStartLine.GetRect());
            }
            foreach (Line mFinishLine in m_FinishLines) {
                a = Rectangle.Union(a, mFinishLine.GetRect());
                mFinishLine.Adjust(DragType.DragNothing);
                a = Rectangle.Union(a, mFinishLine.GetRect());
            }
            return a;
        }

        private Rectangle GetMinMaxRect() {
            return new Rectangle(m_Rect.Right - 14, m_Rect.Top + 29, 12, 9);
        }

        public void AdjustEvents() {
            int num = 43;
            foreach (Event mEvent in m_Events) {
                mEvent.m_Rect = Rectangle.FromLTRB(m_Rect.Left + 4, m_Rect.Top + num, m_Rect.Right - 4, m_Rect.Top + num + 16);
                num += 19;
            }
            m_Rect.Height = num + 1;
        }

        public Rectangle ToggleMinMax() {
            m_bMaximized = !m_bMaximized;
            AdjustLines();
            return Rectangle.Empty;
        }
    }
}
