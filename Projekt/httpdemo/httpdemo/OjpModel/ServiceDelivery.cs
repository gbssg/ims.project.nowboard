using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class ServiceDelivery
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public DateTime ResponseTimestamp { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string ProducerRef { get; set; }

        [XmlElement(ElementName = "OJPStopEventDelivery", Namespace = "http://www.vdv.de/ojp")]
        public List<OjpStopEventDelivery> OjpStopEventDeliveryList { get; set; }
    }

}
