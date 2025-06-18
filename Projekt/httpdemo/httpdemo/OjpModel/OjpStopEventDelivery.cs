using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class OjpStopEventDelivery
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public DateTime ResponseTimestamp { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string RequestMessageRef { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string DefaultLanguage { get; set; }

        public int CalcTime { get; set; }

        public StopEventResponseContext StopEventResponseContext { get; set; }

        [XmlElement("StopEventResult")]
        public List<StopEventResult> StopEventResults { get; set; }
    }

}
