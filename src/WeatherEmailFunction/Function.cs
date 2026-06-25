using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using WeatherEmailFunction.Services;
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WeatherEmailFunction;


public class Function
{
    private readonly WeatherService _weatherService;
    private readonly EmailService _emailService;
    private readonly ILogger<Function> _logger;

    public Function(
        WeatherService weatherService,
        EmailService emailService,
        ILogger<Function> logger)
    {
        _weatherService = weatherService;
        _emailService = emailService;
        _logger = logger;
    }

    [LambdaFunction]
    public async Task FunctionHandler(object input, ILambdaContext context)
    {
        _logger.LogInformation("Timer triggered");

        var weather = await _weatherService.GetWeatherAsync();

        if (weather == null)
            return;

        string body = $"""
            Current Weather

            Temperature: {weather.current_weather.temperature}°C
            Wind Speed: {weather.current_weather.windspeed} km/h
            Time: {DateTime.Now}
            """;

        await _emailService.SendAsync(body);

        _logger.LogInformation("Email sent");
    }
}