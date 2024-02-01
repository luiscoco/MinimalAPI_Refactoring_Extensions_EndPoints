# How to refactor a .NET8 MinimalAPI with Extensions and EndPoints folders

## 1. Create a .NET8 MinimalAPI 

We create a new .NET8 WebAPI with Visual Studio 2022 Community Edition and **unselect the controllers option**



This is the folders structure

![image](https://github.com/luiscoco/MinimalAPI_Refactoring_Extensions_EndPoints/assets/32194879/937fdda2-fe9d-4863-af06-7094bdc4053a)

This is the middleware (**program.cs**) provided by the Visual Studio template

**program.cs**

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


## 2. Create the Extensions 

### 2.1. ApplicationServices Extensions

### 2.2. HttpRequestPipeline Extensions

## 3. Create the EndPoints


## 4. 


