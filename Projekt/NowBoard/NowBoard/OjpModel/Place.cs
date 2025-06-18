
namespace httpdemo.OjpModel
{
    public class Place
    {
        public StopPlace StopPlace { get; set; }
        public StopPoint StopPoint { get; set; }
        public TopographicPlace TopographicPlace { get; set; }
        public Name Name { get; set; }
        public GeoPosition GeoPosition { get; set; }

        internal static object ElementAt(int v)
        {
            throw new NotImplementedException();
        }
    }

}
