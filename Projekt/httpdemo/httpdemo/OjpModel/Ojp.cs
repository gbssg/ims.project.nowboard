using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace httpdemo.OjpModel
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
