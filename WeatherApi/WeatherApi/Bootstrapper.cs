using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using WeatherApi.Repository;
using WeatherApi.RepositoryContract;
using WeatherApi.Service;
using WeatherApi.ServiceContract;

namespace WeatherApi
{
    [ExcludeFromCodeCoverage]
    public class Bootstrapper
    {
        public static void SetupRepository(IServiceCollection service)
        {
            service.AddTransient<ICityRepository, CityRepository>();
            service.AddTransient<ICountryRepository, CountryRepository>();
        }

        public static void SetupServices(IServiceCollection service)
        {
            service.AddTransient<IApiManager, ApiManager>();
            service.AddTransient<ICityService, CityService>();
            service.AddTransient<ICountryService, CountryService>();
            service.AddTransient<IWeatherService, WeatherService>();
        }
    }
}
