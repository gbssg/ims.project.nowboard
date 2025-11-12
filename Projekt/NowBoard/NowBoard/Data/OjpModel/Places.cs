using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class Places
    {
        [XmlElement("Place")]
        public List<Place> PlaceList { get; set; }
    }

}
