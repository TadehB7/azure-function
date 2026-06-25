using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;
using WeatherEmailFunction.Services;

[assembly: LambdaGlobalProperties(GenerateMain = true)]

namespace WeatherEmailFunction;

[LambdaStartup]
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
        services.AddHttpClient<WeatherService>();
        services.AddSingleton<EmailService>();
    }
}