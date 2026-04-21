using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class TextContainer
    {
        [XmlElement(ElementName = "Text")]
        public Text Text { get; set; }
    }

}
