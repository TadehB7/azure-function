using System.Net.Http.Json;
using WeatherEmailFunction.Models;

namespace WeatherEmailFunction.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeatherAsync()
    {
        string url =
            "https://api.open-meteo.com/v1/forecast?latitude=34.05&longitude=-118.25&current_weather=true";

        return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
    }
}
