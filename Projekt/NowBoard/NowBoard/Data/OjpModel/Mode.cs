using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class Mode
    {
        public string PtMode { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string RailSubmode { get; set; }

        public TextContainer Name { get; set; }
        public TextContainer ShortName { get; set; }
    }

}
