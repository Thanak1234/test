using SourceCode.Workflow.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.K2Service.Cores {

    public class XmlFieldCollection : System.Collections.ObjectModel.Collection<XmlField> {

        public static XmlFieldCollection FromApi(XmlFields xmlFields) {
            XmlFieldCollection xmlFieldCollection = new XmlFieldCollection();
            foreach (SourceCode.Workflow.Client.XmlField xmlField in xmlFields) {
                if (xmlField != null) {
                    XmlField item = new XmlField {
                        Name = xmlField.Name,
                        Value = xmlField.Value
                    };
                    xmlFieldCollection.Add(item);
                }
            }
            return xmlFieldCollection;
        }

        public void ToApi(XmlFields xmlFields) {

            Dictionary<string, XmlField> dictionary = new Dictionary<string, XmlField>(xmlFields.Count);
            foreach (XmlField xmlField in xmlFields) {
                if (xmlField != null) {
                    dictionary.Add(xmlField.Name, xmlField);
                }
            }
            foreach (XmlField current in this) {
                if (current != null) {
                    XmlField xmlField2;
                    if (!dictionary.TryGetValue(current.Name, out xmlField2)) {
                        throw new InvalidOperationException(string.Format("Field not Found", current.Name));
                    }
                    xmlField2.Value = current.Value;
                }
            }
        }
    }
}
