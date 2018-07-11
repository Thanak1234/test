using System.Collections;

namespace Workflow.Diagram {
    public class Activities : IEnumerable {
        private ArrayList m_Items;

        public Activities() {
            m_Items = new ArrayList();
        }

        public IEnumerator GetEnumerator() {
            return m_Items.GetEnumerator();
        }

        public void Add(Activity act) {
            m_Items.Add((object)act);
        }

        public Activity Find(int id) {
            foreach (Activity mItem in m_Items) {
                if (mItem.m_ID == id)
                    return mItem;
            }
            return (Activity)null;
        }

        public void BringToFront(Activity act) {
            m_Items.Remove((object)act);
            m_Items.Insert(m_Items.Count, (object)act);
        }
    }
}
