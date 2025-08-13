using httpdemo.OjpModel;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://api.opentransportdata.swiss/ojp20") });

var app = builder.Build();