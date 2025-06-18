using System.Xml.Serialization;

namespace httpdemo.OjpModel
{
    public class Places
    {
        [XmlElement("Place")]
        public List<Place> PlaceList { get; set; }
    }

}
