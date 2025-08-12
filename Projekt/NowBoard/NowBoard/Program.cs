var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register HttpClient using factory pattern (recommended)
builder.Services.AddHttpClient();

// Named HttpClient for specific APIs
builder.Services.AddHttpClient("ojpAPI", client =>
{
    client.BaseAddress = new Uri("https://api.opentransportdata.swiss/ojp20");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();