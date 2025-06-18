using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class StopPoint
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string StopPointRef { get; set; }

        public TextContainer StopPointName { get; set; }
        public PrivateCode PrivateCode { get; set; }
        public string ParentRef { get; set; }
        public string TopographicPlaceRef { get; set; }
    }

}
