# NowBoard

NowBoard is a web application that displays live public transport departures for a specific stop.

## Features
- Live departure data via OJP 2.0 API  
- Automatic refresh  
- Delay information  
- Simple SBB-style display  
- Optional kiosk mode (Raspberry Pi)

## Tech Stack
- C# / .NET  
- Blazor Web Server  
- HTML, CSS, JavaScript  

## Setup
1. Clone the repository  
2. Add your API token to:
`NowBoard/NowBoard/appsettings.Development.secrets.json`
```
{
  "OpenTransportData": {
    "AuthToken": "YOUR_TOKEN"
  }
}
```
3. Run the project:
`dotnet run`

## Notes
Requires internet connection
API access from opentransportdata.swiss
