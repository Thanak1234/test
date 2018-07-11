using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Workflow.Diagram {
    public abstract class Line {
        public int m_ID;
        private bool m_bSelected;
        public Label m_Label;
        private Icon m_Icon;
        public string m_Name;
        public string m_Description;
        private string m_MetaData;
        public Activity m_StartActivity;
        public Activity m_FinishActivity;
        public Point m_Start;
        public Point m_Finish;
        public Point m_Middle;
        public int m_StartOffset;
        public int m_FinishOffset;
        public bool m_MiddlePinned;
        public int m_Status;

        public LineType LineType { get; set; }

        public Color BorderColor
        {
            get
            {
                switch (this.m_Status) {
                    case 0:
                        return Color.FromArgb(96, 125, 139);
                    case 1:
                        return Color.FromArgb(24, 90, 0);
                    case 2:
                        return Color.FromArgb(156, 31, 41);
                    default:
                        return Color.FromArgb(144, 164, 174);
                }
            }
        }

        public Color BodyColor
        {
            get
            {
                switch (this.m_Status) {
                    case 0:
                        return Color.White;
                    case 1:
                        return Color.FromArgb(115, 198, 0);
                    case 2:
                        return Color.FromArgb((int)byte.MaxValue, 143, 134);
                    default:
                        return Color.FromArgb(144, 164, 174);
                }
            }
        }

        public Line() {
            this.m_bSelected = false;
            this.m_MiddlePinned = false;
            this.m_Status = 0;
            this.m_Label = new Label();
            this.m_Label.m_Line = this;
        }

        public virtual void Init(string name, Activity src, Activity dest, Point spoint, Point mpoint, Point fpoint) {
            this.m_Name = name;
            this.m_StartActivity = src;
            this.m_FinishActivity = dest;
            this.m_Start = spoint;
            this.m_Middle = mpoint;
            this.m_Finish = fpoint;
            var cal = (m_Start.X - m_Middle.X);
            if ((cal > -10 && cal < 10)) {
                m_Start.X = m_Finish.X;
            }

            cal = (m_Start.Y - m_Middle.Y);
            if ((cal > -15 && cal < 10)) {
                m_Start.Y = m_Finish.Y;
            }
        }

        public abstract void Draw(Graphics g);

        public void DrawLeftArrow(Graphics g, Brush b, Point point) {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point.X, point.Y, point.X, point.Y - 2);
            path.AddLine(point.X, point.Y - 2, point.X + 4, point.Y - 6);
            path.AddLine(point.X + 4, point.Y - 6, point.X + 4, point.Y + 4);
            path.CloseFigure();
            g.FillPath(b, path);
        }

        public void DrawRightArrow(Graphics g, Brush b, Point point) {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point.X, point.Y, point.X, point.Y - 2);
            path.AddLine(point.X, point.Y - 2, point.X - 4, point.Y - 6);
            path.AddLine(point.X - 4, point.Y - 6, point.X - 4, point.Y + 4);
            path.CloseFigure();
            g.FillPath(b, path);
        }

        public void DrawDownArrow(Graphics g, Brush b, Point point) {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point.X, point.Y, point.X + 1, point.Y);
            path.AddLine(point.X + 1, point.Y, point.X + 5, point.Y - 4);
            path.AddLine(point.X + 5, point.Y - 4, point.X - 4, point.Y - 4);
            g.FillPath(b, path);
        }

        public void DrawUpArrow(Graphics g, Brush b, Point point) {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(point.X, point.Y, point.X + 3, point.Y);
            path.AddLine(point.X + 3, point.Y, point.X + 7, point.Y + 4);
            path.AddLine(point.X + 7, point.Y + 4, point.X - 4, point.Y + 4);
            g.FillPath(b, path);
        }

        private Rectangle Normalize(Rectangle rect) {
            return Rectangle.FromLTRB(Math.Min(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom), Math.Max(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom));
        }

        private Rectangle Inflate(Rectangle rect, int l, int t, int r, int b) {
            return Rectangle.FromLTRB(rect.Left - l, rect.Top - t, rect.Right + r, rect.Bottom + b);
        }

        public void DrawLines(Point[] p, Graphics g) {
            SolidBrush solidBrush = new SolidBrush(this.BodyColor);
            Pen pen = new Pen(this.BorderColor, 1f);
            Rectangle[] rectangleArray = new Rectangle[3];
            bool[] flagArray = new bool[3];
            int length = p.Length;
            for (int index = 0; index < length - 1; ++index) {
                if (p[index].X == p[index + 1].X) {
                    int num1 = p[index].Y > p[index + 1].Y ? -2 : 2;
                    int num2 = index == 0 ? p[index].Y : p[index].Y - num1;
                    int num3 = index + 2 == length ? p[index + 1].Y - num1 : p[index + 1].Y + num1;
                    rectangleArray[index] = Rectangle.FromLTRB(p[index].X - 2, num2, p[index].X + 2, num3);
                    flagArray[index] = true;
                    g.DrawLine(pen, p[index].X - 2, num2, p[index].X - 2, num3);
                    g.DrawLine(pen, p[index].X + 2, num2, p[index].X + 2, num3);
                } else {
                    int num1 = p[index].X > p[index + 1].X ? -2 : 2;
                    int num2 = index == 0 ? p[index].X : p[index].X - num1;
                    int num3 = index + 2 == length ? p[index + 1].X - num1 : p[index + 1].X + num1;
                    rectangleArray[index] = Rectangle.FromLTRB(num2, p[index].Y - 2, num3, p[index].Y + 2);
                    flagArray[index] = false;
                    g.DrawLine(pen, num2, p[index].Y - 2, num3, p[index].Y - 2);
                    g.DrawLine(pen, num2, p[index].Y + 2, num3, p[index].Y + 2);
                }
            }
            for (int index = 0; index < length - 1; ++index) {
                rectangleArray[index] = this.Normalize(rectangleArray[index]);
                rectangleArray[index] = this.Inflate(rectangleArray[index], -1, -1, 0, 0);
                Rectangle rect = rectangleArray[index];
                rect = !flagArray[index] ? Rectangle.FromLTRB(rect.Left + 2, rect.Top, rect.Right - 2, rect.Bottom) : Rectangle.FromLTRB(rect.Left, rect.Top + 2, rect.Right, rect.Bottom - 2);
                RectShadow.DrawShade(g, rect);
            }
            for (int index = 0; index < length - 1; ++index)
                g.FillRectangle((Brush)solidBrush, rectangleArray[index]);
        }

        public void SetFinishPoint(Point point) {
            this.SetFinishPoint(point, DragType.DragIgnore);
        }

        public void SetFinishPoint(Point point, DragType drag) {
            this.m_Finish = point;
            if (drag == DragType.DragIgnore)
                return;
            this.Adjust(drag);
        }

        public void SetStartPoint(Point point) {
            this.SetStartPoint(point, DragType.DragIgnore);
        }

        public void SetStartPoint(Point point, DragType drag) {
            this.m_Start = point;
            if (drag == DragType.DragIgnore)
                return;
            this.Adjust(drag);
        }

        public void SetMiddlePoint(Point point) {
        }

        public bool HitMiddle(Point point) {
            return false;
        }

        public bool HitLabel(Point point) {
            return false;
        }

        public abstract void Adjust(DragType drag);

        public Rectangle GetRect() {
            return Rectangle.Empty;
        }
    }
}
