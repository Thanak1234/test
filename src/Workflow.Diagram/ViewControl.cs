using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Workflow.Diagram.views;

namespace Workflow.Diagram {
    public class ViewControl {
        private Activities m_Acts;
        private Lines m_Lines;

        public string Xml
        {
            set
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(value);
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("Activities")) {
                    Activity act = new Activity();
                    act.m_ID = Convert.ToInt32(GetXmlAttr(node, "ID"));
                    act.m_Name = GetXmlAttr(node, "Name");
                    act.m_Description = GetXmlAttr(node, "Description");
                    string[] strArray = GetXmlAttr(node, "Rect").Split(',');
                    act.m_Rect = Rectangle.FromLTRB(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[3]));
                    act.m_bMaximized = Convert.ToBoolean(GetXmlAttr(node, "Maximized"));
                    act.m_Icon = new Icon(GetType(), "Icons.activity.ico");
                    m_Acts.Add(act);
                }
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("ActInsts")) {
                    Activity activity = m_Acts.Find(Convert.ToInt32(GetXmlAttr(node, "ActID")));
                    var status = Convert.ToInt32(GetXmlAttr(node, "Status"));
                    activity.Status = status;
                    activity.ActInstID = Convert.ToInt32(GetXmlAttr(node, "ActInstID"));

                    switch (status) {
                        case 1:
                            activity.m_Icon = new Icon(GetType(), "Icons.activity_waiting.ico");
                            continue;
                        case 2:
                            activity.m_Icon = new Icon(GetType(), "Icons.activity_active.ico");
                            continue;
                        case 3:
                            activity.m_Icon = new Icon(GetType(), "Icons.activity_expire.ico");
                            continue;
                        case 4:
                            activity.m_Icon = new Icon(GetType(), "Icons.activity_complete.ico");
                            continue;
                        default:
                            continue;
                    }
                }
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("Events")) {
                    int int32_1 = Convert.ToInt32(GetXmlAttr(node, "ID"));
                    int int32_2 = Convert.ToInt32(GetXmlAttr(node, "ActID"));
                    string xmlAttr = GetXmlAttr(node, "Name");
                    int int32_3 = Convert.ToInt32(GetXmlAttr(node, "Type"));
                    Activity activity = m_Acts.Find(int32_2);
                    Event @event = activity.m_Events.Add(int32_3, activity.m_Events.Count, xmlAttr);
                    @event.m_ID = int32_1;
                    @event.m_Type = int32_3;
                    @event.m_Icon = int32_3 != 1 ? new Icon(GetType(), "Icons.user_event.ico") : new Icon(GetType(), "Icons.server_event.ico");
                }
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("EventInsts")) {
                    int int32_1 = Convert.ToInt32(GetXmlAttr(node, "EventID"));
                    int int32_2 = Convert.ToInt32(GetXmlAttr(node, "Status"));
                    Event @event = FindEvent(int32_1);
                    @event.Status = int32_2;
                    if (int32_2 == 0) {
                        @event.m_Icon = @event.m_Type != 1 ? new Icon(GetType(), "Icons.user_event_active.ico") : new Icon(GetType(), "Icons.server_event_active.ico");
                    } else if (int32_2 == 3) {
                        @event.m_Icon = @event.m_Type != 1 ? new Icon(GetType(), "Icons.user_event_complete.ico") : new Icon(GetType(), "Icons.server_event_complete.ico");
                    }
                }
                foreach (Activity mAct in m_Acts) {
                    if (mAct.m_Events.Count == 0) {
                        mAct.m_Icon = new Icon(GetType(), "Icons.start.ico");
                        mAct.m_bStart = true;
                        mAct.m_bMaximized = false;
                    } else
                        mAct.AdjustEvents();
                }
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("Lines")) {
                    int int32_1 = Convert.ToInt32(GetXmlAttr(node, "ID"));
                    int int32_2 = Convert.ToInt32(GetXmlAttr(node, "StartID"));
                    int int32_3 = Convert.ToInt32(GetXmlAttr(node, "FinishID"));
                    string[] strArray1 = GetXmlAttr(node, "Coordinates").Split(',');
                    Point spoint = new Point(Convert.ToInt32(strArray1[0]), Convert.ToInt32(strArray1[1]));
                    Point mpoint = new Point(Convert.ToInt32(strArray1[2]), Convert.ToInt32(strArray1[3]));
                    Point fpoint = new Point(Convert.ToInt32(strArray1[4]), Convert.ToInt32(strArray1[5]));
                    LineType type = (LineType)Convert.ToInt32(GetXmlAttr(node, "Type"));
                    Line line = m_Lines.Add("", m_Acts.Find(int32_2), m_Acts.Find(int32_3), type, spoint, mpoint, fpoint);
                    line.LineType = type;
                    line.m_ID = int32_1;
                    line.m_Label.m_Name = GetXmlAttr(node, "Lbl");
                    string[] strArray2 = GetXmlAttr(node, "LblRect").Split(',');
                    line.m_Label.m_Rect = Rectangle.FromLTRB(Convert.ToInt32(strArray2[0]), Convert.ToInt32(strArray2[1]), Convert.ToInt32(strArray2[2]), Convert.ToInt32(strArray2[3]));
                    string[] strArray3 = GetXmlAttr(node, "LblOffset").Split(',');
                    line.m_Label.m_Offset = new Point(Convert.ToInt32(strArray3[0]), Convert.ToInt32(strArray3[1]));
                    line.m_Label.m_bStart = Convert.ToBoolean(GetXmlAttr(node, "LblStart"));
                    line.m_Label.m_bPos = Convert.ToBoolean(GetXmlAttr(node, "LblPos"));
                    line.m_Label.m_StartOr = (LabelHV)Convert.ToInt32(GetXmlAttr(node, "LblStartOr"));
                    line.m_Label.m_FinishOr = (LabelHV)Convert.ToInt32(GetXmlAttr(node, "LblFinishOr"));
                }
                foreach (XmlNode node in xmlDocument.DocumentElement.SelectSingleNode("LineInsts"))
                    m_Lines.Find(Convert.ToInt32(GetXmlAttr(node, "LineID"))).m_Status = Convert.ToInt32(GetXmlAttr(node, "Result"));

            }
        }

        public ViewControl() {
            m_Acts = new Activities();
            m_Lines = new Lines();
        }

        public byte[] GetImageStream() {
            var bitmap = new Bitmap(1500, 2000);
            Graphics graphics = Graphics.FromImage(bitmap);
            foreach (Line mLine in m_Lines)
                mLine.Draw(graphics);

            foreach (Activity mAct in m_Acts)
                mAct.Draw(graphics);

            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            return memoryStream.ToArray();
        }

        public object GetJson() {
            List<LineView> lines = new List<LineView>();
            List<ActivityView> activities = new List<ActivityView>();

            foreach (Line mLine in m_Lines) {
                lines.Add(LineView.From(mLine));
            }

            foreach (Activity act in m_Acts) {
                activities.Add(ActivityView.From(act));
            }

            return new { lines = lines, activities = activities };
        }

        private string GetXmlAttr(XmlNode node, string attr) {
            XmlNode namedItem = node.Attributes.GetNamedItem(attr);
            if (namedItem == null)
                return "";
            return namedItem.Value;
        }

        private Event FindEvent(int id) {
            foreach (Activity mAct in m_Acts) {
                foreach (Event mEvent in mAct.m_Events) {
                    if (mEvent.m_ID == id)
                        return mEvent;
                }
            }
            return (Event)null;
        }
    }
}
