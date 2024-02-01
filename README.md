# How to refactor a .NET8 MinimalAPI for adding Extensions and EndPoints folders

## 1. Create a .NET8 MinimalAPI (See Sample1, Default Visual Studio template code)

We create a new .NET8 WebAPI with Visual Studio 2022 Community Edition and **unselect the controllers option**



This is the folders structure

![image](https://github.com/luiscoco/MinimalAPI_Refactoring_Extensions_EndPoints/assets/32194879/937fdda2-fe9d-4863-af06-7094bdc4053a)

This is the middleware (**program.cs**) provided by the Visual Studio template

**program.cs**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
```

## 2. Create the Extensions files (See Sample 2)

We create a new folder **Extensions**

![image](https://github.com/luiscoco/MinimalAPI_Refactoring_Extensions_EndPoints/assets/32194879/6d892790-39c1-4c5c-8f0e-d64d4df8d2eb)

And inside the **Extensions** folder we create two new files: **ApplicationServicesExtensions.cs** and **HttpRequestPipelineExtensions.cs**

### 2.1. ApplicationServicesExtensions.cs

We create a new file **ApplicationServicesExtensions.cs** for including the services adding to the application container

**ApplicationServicesExtensions.cs**

```csharp
using System.Text.Json;

namespace MinimalAPISample2.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddSwaggerGen();

        return services;
    }

}
```

We modify the middleware (**program.cs**) for invoking the **ConfigureApplicationServices** function

```csharp
using MinimalAPISample2.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices();

var app = builder.Build();
...
```

### 2.2. HttpRequestPipelineExtensions.cs 

We create a new file **HttpRequestPipelineExtensions.cs** for configuring the HTTP request pipeline

**HttpRequestPipelineExtensions.cs** 

```csharp
namespace MinimalAPISample2.Extensions;

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class HttpRequestPipelineExtensions
{

    public static WebApplication ConfigureHttpRequestPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        return app;
    }
}
```

We modify the middleware (**program.cs**) for invoking the **ConfigureApplicationServices** and **ConfigureHttpRequestPipeline** functions

```csharp
using MinimalAPISample2.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices();

var app = builder.Build();
app.ConfigureHttpRequestPipeline();
app.Run();
```

## 3. Create the EndPoints


## 4. 


