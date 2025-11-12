using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class TextContainer
    {
        [XmlElement(ElementName = "Text")]
        public Text Text { get; set; }
    }

}
