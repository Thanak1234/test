using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Workflow.Core.Domain.Upload
{
    [XmlType(TypeName = "file")]
    public class FileContent
    {
        [XmlElement("name")]
        public string FileName { get; set; }

        [XmlElement("content")]
        public string Content { get; set; }

        public string ToXml()
        {
            var content = new FileContent() {
                FileName = this.FileName,
                Content = this.Content
            };

            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(content.GetType());
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, content, emptyNamepsaces);
                return stream.ToString();
            }

        }
    }
}
