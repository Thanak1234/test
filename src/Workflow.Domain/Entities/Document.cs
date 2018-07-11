using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Workflow.Domain.Entities
{
    public class DocumentModel {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public string Status { get; set; }
        public string ActivityCode { get; set; }
    }

    [Table("DOCUMENT", Schema = "BPMDATA")]
    public class Document
    {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public string Status { get; set; } // ATTACH_TO_PROCESS, ATTACH_TO_ACTIVITY
    }

    [Table("DOCUMENT_FILE", Schema = "BPMDATA")]
    public class DocumentFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public byte[] GetFileBinary()
        {
            var fileContent = this.Content.XmlDeserializeFromString<ContentFile>();
            this.Name = fileContent.FileName;
            return Convert.FromBase64String(fileContent.FileContent);
        }
    }

    [XmlType(TypeName = "file")]
    public class ContentFile
    {
        [XmlElement("name")]
        public string FileName { get; set; }

        [XmlElement("content")]
        public string FileContent { get; set; }

        public string ToXml()
        {
            var content = new ContentFile()
            {
                FileName = this.FileName,
                FileContent = this.FileContent
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
