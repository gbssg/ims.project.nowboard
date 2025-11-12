using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class GeoPosition
    {
        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public double Longitude { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public double Latitude { get; set; }
    }

}
