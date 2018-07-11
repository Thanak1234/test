using System.Collections;

namespace Workflow.Diagram {
    public class Events : IEnumerable {
        private ArrayList m_Items;

        public int Count
        {
            get
            {
                return m_Items.Count;
            }
        }

        public Events() {
            m_Items = new ArrayList();
        }

        public IEnumerator GetEnumerator() {
            return m_Items.GetEnumerator();
        }

        public void Add(Event ev) {
            m_Items.Add((object)ev);
        }

        public Event Add(int type, int index, string name) {
            Event ev = new Event();
            ev.m_Name = name;
            Add(ev);
            return ev;
        }
    }
}
