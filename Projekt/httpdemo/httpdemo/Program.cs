using System.Linq.Expressions;
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

            var haltestelle = responseOjp.OjpResponse.ServiceDelivery.OjpStopEventDelivery.StopEventResponseContext.Places.PlaceList[0].StopPlace.StopPlaceName.Text.Value ?? "Unbekannt";

            Console.WriteLine(" Haltestelle\t\t\tLinie\t\tAbfahrt\t\tHinweis");
            Console.WriteLine("-------------------------------------------------------------------------");

            foreach (var stopEvent in responseOjp.OjpResponse.ServiceDelivery.OjpStopEventDelivery.StopEventResults)
            {
                var estimatedTime = stopEvent.StopEvent.ThisCall.CallAtStop.ServiceDeparture.EstimatedTime.AddHours(2);
                var timetabledTime = stopEvent.StopEvent.ThisCall.CallAtStop.ServiceDeparture.TimetabledTime.AddHours(2);
                var linie = stopEvent.StopEvent.Service.PublishedServiceName.Text.Value;
                var hinweis = estimatedTime - timetabledTime;

                if (linie == "180")
                { }
                else
                {
                    Console.WriteLine($" {haltestelle}\t\tB {linie}\t\t{timetabledTime:HH:mm:ss}\t{hinweis}");
                }
            }
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
            <StopPlaceRef>8574258</StopPlaceRef>
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
    </siri:ServiceRequest>
  </OJPRequest>
</OJP>";
        }
    }
}