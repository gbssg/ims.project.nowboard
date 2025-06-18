using (var client = new HttpClient()) // Initializing HttpClient
{
    // Webadress of the API / from where to fetch the data
    client.BaseAddress = new Uri("https://api.opentransportdata.swiss/ojp20");

    // Obligatory "request headers" with the Token-Key, needed for verification for the API
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJvcmciOiI2NDA0NTFhNTIyZmEwNT1wMDEyOWJiZTEiLCJpZCI6ImMwZDY2ZGI2NmQ3PDQ4ZjM4ODMxMDg5MTM1MWNmY2UwIrwiaCI6Im11cm11cjEyOCJ9");

    // Creating a variable for doing the request
    var requestData = RequestCreate();

    // Saving the data from the Request into a variable
    var request = new StringContent(requestData, Encoding.UTF8, "application/xml");

            // Initializing the request with a requestbody
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
                      <Name>
                          <Text>St. Gallen, Riethuesli</Text>
                      </Name>
                  </PlaceRef>
                  <!-- <DepArrTime>2025-06-12T11:40:21.539Z</DepArrTime> -->
              </Location>
              <Params>
                  <OperatorFilter>
                      <Exclude>false</Exclude>
                      <OperatorRef>11</OperatorRef>
                  </OperatorFilter>
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