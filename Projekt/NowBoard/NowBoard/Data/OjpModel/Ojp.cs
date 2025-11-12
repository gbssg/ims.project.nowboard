using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    [XmlRoot(ElementName = "OJP", Namespace = "http://www.vdv.de/ojp")]
    public class Ojp
    {
        [XmlElement(ElementName = "OJPResponse")]
        public OjpResponse OjpResponse { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns = new XmlSerializerNamespaces();
    }
}
