using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NowBoard.Data.NowBoard;
using NowBoard.Data.OjpModel;
using NowBoard.Data.Setup;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace NowBoard.Components.Pages
{
    public partial class Home
    {
        [Inject]
        public required OpenTransportDataSetup OtdSetup { get; set; }
        private List<Datalist> datalist = new();
        private Timer? refreshTimer;

        protected override async Task OnInitializedAsync()
        {
            await DisplayDepartures();

            // Timer aktualisiert Abfahrten jede Minute
            refreshTimer = new Timer(async _ =>
            {
                await InvokeAsync(async () =>
                {
                    await DisplayDepartures();
                    StateHasChanged();
                });
            }, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await JS.InvokeVoidAsync("drawClock");
        }

        public void Dispose() => refreshTimer?.Dispose();

        private async Task DisplayDepartures()
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.opentransportdata.swiss/ojp20")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", OtdSetup.AuthToken
            );

            var request = new StringContent(RequestCreate(), Encoding.UTF8, "application/xml");
            var response = await client.PostAsync("", request);
            var responseXML = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(Ojp));
            if (serializer.Deserialize(new StringReader(responseXML)) is not Ojp responseOjp)
                return;

            var serviceDelivery = responseOjp.OjpResponse?.ServiceDelivery;
            if (serviceDelivery == null)
                return;

            var allDepartures = new List<DepartureInfo>();

            foreach (var delivery in serviceDelivery.OjpStopEventDeliveryList)
            {
                var stopName = delivery.StopEventResponseContext?.Places?.PlaceList?[0]?.StopPlace?.StopPlaceName?.Text?.Value ?? "Unbekannt";

                if (delivery.StopEventResults == null)
                    continue;

                foreach (var stopEvent in delivery.StopEventResults)
                {
                    var serviceDeparture = stopEvent.StopEvent?.ThisCall?.CallAtStop?.ServiceDeparture;
                    if (serviceDeparture == null)
                        continue;

                    var estimated = serviceDeparture.EstimatedTime.AddHours(1);
                    var scheduled = serviceDeparture.TimetabledTime.AddHours(1);
                    var line = stopEvent.StopEvent?.Service?.PublishedServiceName?.Text?.Value ?? string.Empty;

                    allDepartures.Add(new DepartureInfo
                    {
                        TimetabledTime = scheduled,
                        EstimatedTime = estimated,
                        Line = line,
                        Hinweis = estimated - scheduled,
                        Station = stopName
                    });
                }
            }

            datalist = allDepartures
                .Where(d => d.Line is "5" or "S21" or "S22")
                .OrderBy(d => d.TimetabledTime)
                .Take(5)
                .Select(d => new Datalist
                {
                    haltestelle = d.Station,
                    timetabledTime = d.TimetabledTime.ToString("t"),
                    estimatedTime = d.EstimatedTime.ToString("t"),
                    estimatedTime2 = d.EstimatedTime.ToString("t"),
                    linie = d.Line,
                    hinweis = d.Hinweis.TotalMinutes < 0.9 ? string.Empty : $"{(int)Math.Round(d.Hinweis.TotalMinutes)}'"
                })
                .ToList();
        }

        private int GetLoslaufenCountdown(int index)
        {
            if (datalist.Count <= index) return 0;
            if (!DateTime.TryParse(datalist[index].estimatedTime2, out var abfahrt)) return 0;

            var loslaufen = abfahrt.AddMinutes(-6);
            var diff = (loslaufen - DateTime.Now).TotalMinutes;

            return Math.Max(0, (int)Math.Ceiling(diff));
        }

        private static string RequestCreate() => @"<?xml version=""1.0"" encoding=""UTF-8""?>
<OJP xmlns=""http://www.vdv.de/ojp"" xmlns:siri=""http://www.siri.org.uk/siri"" version=""2.0"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.vdv.de/ojp ../../../../OJP4/OJP.xsd"">
  <OJPRequest>
    <siri:ServiceRequest>
      <OJPStopEventRequest>
        <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
        <siri:MessageIdentifier>SER</siri:MessageIdentifier>
        <Location>
          <PlaceRef>
            <StopPlaceRef>8574258</StopPlaceRef>
            <Name><Text>St. Gallen, Riethuesli</Text></Name>
          </PlaceRef>
        </Location>
        <Params>
          <OperatorFilter><Exclude>false</Exclude><OperatorRef>11</OperatorRef></OperatorFilter>
          <NumberOfResults>5</NumberOfResults>
          <StopEventType>departure</StopEventType>
          <IncludePreviousCalls>false</IncludePreviousCalls>
          <IncludeOnwardCalls>false</IncludeOnwardCalls>
          <UseRealtimeData>full</UseRealtimeData>
        </Params>
      </OJPStopEventRequest>

      <OJPStopEventRequest>
        <siri:RequestTimestamp>2024-06-01T11:24:34.598Z</siri:RequestTimestamp>
        <siri:MessageIdentifier>SER</siri:MessageIdentifier>
        <Location>
          <PlaceRef>
            <StopPlaceRef>8506371</StopPlaceRef>
            <Name><Text>St. Gallen Riethuesli</Text></Name>
          </PlaceRef>
        </Location>
        <Params>
          <OperatorFilter><Exclude>false</Exclude><OperatorRef>11</OperatorRef></OperatorFilter>
          <NumberOfResults>5</NumberOfResults>
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