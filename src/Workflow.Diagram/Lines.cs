using System.Collections;
using System.Drawing;

namespace Workflow.Diagram {
    public class Lines : IEnumerable {
        private ArrayList m_Items;

        public int Count
        {
            get
            {
                return m_Items.Count;
            }
        }

        public Lines() {
            m_Items = new ArrayList();
        }

        public IEnumerator GetEnumerator() {
            return m_Items.GetEnumerator();
        }

        public void Add(Line line) {
            m_Items.Add((object)line);
        }

        public Line Find(int id) {
            foreach (Line mItem in m_Items) {
                if (mItem.m_ID == id)
                    return mItem;
            }
            return (Line)null;
        }

        private Line Create(LineType type) {
            Line line = (Line)null;
            switch (type) {
                case LineType.H3Line:
                    line = (Line)new H3Line();
                    break;
                case LineType.V3Line:
                    line = (Line)new V3Line();
                    break;
                case LineType.H2Line:
                    line = (Line)new H2Line();
                    break;
                case LineType.V2Line:
                    line = (Line)new V2Line();
                    break;
                case LineType.H1Line:
                    line = (Line)new H1Line();
                    break;
                case LineType.V1Line:
                    line = (Line)new V1Line();
                    break;
            }
            return line;
        }

        private Line Create(string name, Activity src, Activity dest, LineType type, Point spoint, Point mpoint, Point fpoint) {
            Line line = Create(type);
            line.Init(name, src, dest, spoint, mpoint, fpoint);
            return line;
        }

        public Line Add(string name, Activity src, Activity dest, LineType type, Point spoint, Point mpoint, Point fpoint) {
            Line line = Create(name, src, dest, type, spoint, mpoint, fpoint);
            src.AddStartLine(line);
            dest.AddFinishLine(line);
            Add(line);
            return line;
        }
    }
}
