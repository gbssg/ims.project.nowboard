using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class CallAtStop
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string StopPointRef { get; set; }

        public TextContainer StopPointName { get; set; }
        public ServiceDeparture ServiceDeparture { get; set; }
        public int Order { get; set; }
        public bool RequestStop { get; set; }
    }

}
