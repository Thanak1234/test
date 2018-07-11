using System.Drawing;

namespace Workflow.Diagram {
    public class Event {
        public int m_ID;
        public string m_Name;
        public bool m_bSelected;
        public int m_Type;
        public string m_Description;
        public Rectangle m_Rect;
        public string m_MetaData;
        public int m_ExpectedDuration;
        public int m_Priority;
        public Icon m_Icon;

        public int Status { get; set; }

        public Event() {
        }

        public void Draw(Graphics g) {
            Rectangle rectangle = Rectangle.FromLTRB(m_Rect.Left + 20, m_Rect.Top + 1, m_Rect.Right, m_Rect.Bottom);
            StringFormat format = new StringFormat();
            format.FormatFlags = format.FormatFlags | StringFormatFlags.NoWrap;
            format.Trimming = StringTrimming.EllipsisCharacter;
            g.DrawString(m_Name, new Font("Tahoma", 8f), (Brush)new SolidBrush(SystemColors.WindowText), (RectangleF)rectangle, format);
            g.DrawIcon(m_Icon, m_Rect.Left + 1, m_Rect.Top + 1);
        }
    }
}
