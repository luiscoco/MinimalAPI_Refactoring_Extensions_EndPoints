using MinimalAPISample2.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices();

var app = builder.Build();
app.ConfigureHttpRequestPipeline();
app.Run();
