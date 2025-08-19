var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register HttpClient using factory pattern (recommended)
builder.Services.AddHttpClient();

var app = builder.Build();