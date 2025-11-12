using System.Xml.Serialization;

namespace NowBoard.Data.OjpModel
{
    public class Service
    {
        public string OperatingDayRef { get; set; }
        public string JourneyRef { get; set; }
        public string PublicCode { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string LineRef { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string DirectionRef { get; set; }

        public Mode Mode { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public TextContainer PublishedServiceName { get; set; }
        public int TrainNumber { get; set; }
        public Attribute Attribute { get; set; }
        public string OriginStopPointRef { get; set; }
        public TextContainer OriginText { get; set; }

        [XmlElement(Namespace = "http://www.siri.org.uk/siri")]
        public string OperatorRef { get; set; }

        public string DestinationStopPointRef { get; set; }
        public TextContainer DestinationText { get; set; }
    }

}
