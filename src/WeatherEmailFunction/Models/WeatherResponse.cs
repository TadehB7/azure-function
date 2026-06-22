namespace WeatherEmailFunction.Models;

public class WeatherResponse
{
    public CurrentWeather current_weather { get; set; }
}

public class CurrentWeather
{
    public double temperature { get; set; }
    public double windspeed { get; set; }
}
