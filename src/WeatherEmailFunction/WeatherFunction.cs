using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using WeatherEmailFunction.Services;

namespace WeatherEmailFunction;

public class WeatherFunction
{
    private readonly WeatherService _weatherService;
    private readonly EmailService _emailService;
    private readonly ILogger<WeatherFunction> _logger;

    public WeatherFunction(
        WeatherService weatherService,
        EmailService emailService,
        ILogger<WeatherFunction> logger)
    {
        _weatherService = weatherService;
        _emailService = emailService;
        _logger = logger;
    }

    [Function("WeatherFunction")]
    public async Task Run([TimerTrigger("%WeatherCronExpression%")] TimerInfo timer)
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