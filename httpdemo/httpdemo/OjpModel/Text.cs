using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class Text
    {
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Language { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

}
