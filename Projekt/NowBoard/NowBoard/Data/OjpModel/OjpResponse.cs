using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class OjpResponse
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public ServiceDelivery ServiceDelivery { get; set; }
    }

}
