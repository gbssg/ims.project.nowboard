using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;
using httpdemo.OjpModel;

namespace httpdemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("NowBoard Demo\n");

            while (true)
            {
                Console.Clear();

                try
                {
                    await displaydepartures();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("\nRefreshing in 1 min... (Press Ctrl+C to exit)");
                await Task.Delay(60000);
            }
        }

        private static async Task displaydepartures()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.opentransportdata.swiss/ojp20");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJvcmciOiI2NDA2NTFhNTIyZmEwNTAwMDEyOWJiZTEiLCJpZCI6ImMwZDY2ZGI2NmQ3NDQ4ZjM4ODMxMDg5MTM1MWNmY2UwIiwiaCI6Im11cm11cjEyOCJ9");

            var requestData = RequestCreate();
            var request = new StringContent(requestData, Encoding.UTF8, "application/xml");

            var response = await client.PostAsync("", request);
            var responseXML = await response.Content.ReadAsStringAsync();

            var ojpReader = new XmlSerializer(typeof(Ojp));
            var responseOjp = (Ojp?)ojpReader.Deserialize(new StringReader(responseXML));

            var deliveries = responseOjp?.OjpResponse?.ServiceDelivery?.OjpStopEventDeliveryList;

            var allDepartures = new List<DepartureInfo>();

            foreach (var delivery in deliveries)
            {
                if (delivery.StopEventResponseContext?.Places?.PlaceList?.Any() != true || delivery.StopEventResults == null)
                    continue;

                var haltestelle = delivery.StopEventResponseContext.Places.PlaceList[0].StopPlace.StopPlaceName.Text.Value ?? "Unbekannt";

                foreach (var stopEvent in delivery.StopEventResults)
                {
                    var serviceDeparture = stopEvent.StopEvent.ThisCall.CallAtStop.ServiceDeparture;
                    var estimatedTime = serviceDeparture.EstimatedTime.AddHours(2);
                    var timetabledTime = serviceDeparture.TimetabledTime.AddHours(2);
                    var linie = stopEvent.StopEvent.Service.PublishedServiceName.Text.Value;
                    var hinweis = estimatedTime - timetabledTime;

                    allDepartures.Add(new DepartureInfo
                    {
                        TimetabledTime = timetabledTime,
                        EstimatedTime = estimatedTime,
                        Line = linie,
                        Notice = hinweis,
                        Station = haltestelle
                    });
                }
            }

            var sortedDepartures = allDepartures
                .OrderBy(d => d.TimetabledTime)
                .ToList(); // Abfahrten sortieren nach Zeit

            Console.WriteLine(" Haltestelle\t\t\tLinie\t\tAbfahrt\t\tHinweis");
            Console.WriteLine("-------------------------------------------------------------------------");

            foreach (var departure in sortedDepartures)
            {
                string anzeigeLinie = departure.Line;
                string prefix = "";
                string anzeigeHaltestelle = departure.Station;

                if (departure.Line == "180")
                { continue; } // Springt zur nächsten Abfahrt

                else if (departure.Line == "5")
                {
                    prefix = "B";
                }
                else if (departure.Line == "S21" || departure.Line == "S22")
                {
                    prefix = "";
                }

                Console.WriteLine($" {anzeigeHaltestelle}\t\t{prefix}{anzeigeLinie}\t\t{departure.TimetabledTime:HH:mm:ss}\t{departure.Notice}");
            }
        }

        public class DepartureInfo
        {
            public DateTime TimetabledTime { get; set; }
            public DateTime EstimatedTime { get; set; }
            public string Line { get; set; }
            public TimeSpan Notice { get; set; }
            public string Station { get; set; }
        }

        private static string RequestCreate()
        {
            return @"<?xml version=""1.0"" encoding=""UTF-8""?>
<OJP xmlns=""http://www.vdv.de/ojp"" xmlns:siri=""http://www.siri.org.uk/siri"" version=""2.0"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.vdv.de/ojp ../../../../OJP4/OJP.xsd"">
  <OJPRequest>
    <siri:ServiceRequest>
      <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
      <siri:RequestorRef>MENTZRegTest</siri:RequestorRef>
      <OJPStopEventRequest>
        <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
        <siri:MessageIdentifier>SER</siri:MessageIdentifier>
        <Location>
          <PlaceRef>
            <StopPlaceRef>8506371</StopPlaceRef>
            <Name>
              <Text>St. Gallen, Riethuesli</Text>
            </Name>
          </PlaceRef>
        </Location>
        <Params>
          <OperatorFilter>
            <Exclude>false</Exclude>
            <OperatorRef>11</OperatorRef>
          </OperatorFilter>
          <NumberOfResults>7</NumberOfResults>
          <StopEventType>departure</StopEventType>
          <IncludePreviousCalls>false</IncludePreviousCalls>
          <IncludeOnwardCalls>false</IncludeOnwardCalls>
          <UseRealtimeData>full</UseRealtimeData>
        </Params>
      </OJPStopEventRequest>

    <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
          <siri:RequestorRef>MENTZRegTest</siri:RequestorRef>
          <OJPStopEventRequest>
            <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
            <siri:MessageIdentifier>SER</siri:MessageIdentifier>
            <Location>
              <PlaceRef>
                <StopPlaceRef>8574258</StopPlaceRef>
                <Name>
                  <Text>St. Gallen Riethuesli</Text>
                </Name>
              </PlaceRef>
            </Location>
            <Params>
              <OperatorFilter>
                <Exclude>false</Exclude>
                <OperatorRef>11</OperatorRef>
              </OperatorFilter>
              <NumberOfResults>10</NumberOfResults>
              <StopEventType>departure</StopEventType>
              <IncludePreviousCalls>false</IncludePreviousCalls>
              <IncludeOnwardCalls>false</IncludeOnwardCalls>
              <UseRealtimeData>full</UseRealtimeData>
            </Params>
          </OJPStopEventRequest>
    </siri:ServiceRequest>
  </OJPRequest>
</OJP>";
        }
    }
}