using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WeatherApi.Core.Constant;
using WeatherApi.Dto;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IApiManager apiManager;
        private readonly IConfiguration configuration;

        public WeatherService(IApiManager apiManager, IConfiguration configuration)
        {
            this.apiManager = apiManager;
            this.configuration = configuration;
        }

        public GenericResponse<WeatherDto> GetByCityName(string cityName)
        {
            var response = new GenericResponse<WeatherDto>();

            try
            {
                var openWeatherApiUrl = this.configuration.GetValue<string>(ConfigConstant.OpenWeatherApiUrl);
                var openWeatherApiKey = this.configuration.GetValue<string>(ConfigConstant.OpenWeatherApiKey);

                var url = string.Format(openWeatherApiUrl, cityName, openWeatherApiKey);
                var weatherApiResponse = Task.Run(() => this.apiManager.SendRequestAsync(url, "GET")).GetAwaiter().GetResult();

                if (weatherApiResponse.IsError())
                {
                    response.AddErrorMessage(weatherApiResponse.GetErrorMessage());
                    return response;
                }

                var result = JsonConvert.DeserializeObject<Dto.OpenWeatherMap.OpenWeatherMapDto>(weatherApiResponse.Data);
                response.Data = GetWeatherDataDto(result);
                return response;
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        private WeatherDto GetWeatherDataDto(Dto.OpenWeatherMap.OpenWeatherMapDto openWeatherMapDto)
        {
            var weatherData = new WeatherDto()
            {
                Country = openWeatherMapDto.Sys.Country,
                City = openWeatherMapDto.Name,
                Location = "(Longitude:" + openWeatherMapDto.Coord.Lon + ",Latitude:" + openWeatherMapDto.Coord.Lat + ")",
                Time = openWeatherMapDto.TimeZone,
                Wind = openWeatherMapDto.Wind.Speed,
                Visibility = openWeatherMapDto.Visibility,
                SkyCondition = openWeatherMapDto.Weather[0].Description,
                TemperatureCelcius = openWeatherMapDto.Main.Temp / 10,
                TemperatureFahrenheit = GetFahrenheit(openWeatherMapDto.Main.Temp / 10),
                TemperatureMinimum = openWeatherMapDto.Main.TempMin,
                TemperatureMaximum = openWeatherMapDto.Main.TempMax,
                RelativeHumidity = openWeatherMapDto.Main.Humidity,
                Pressure = openWeatherMapDto.Main.Pressure,
            };

            return weatherData;
        }

        private decimal GetFahrenheit(decimal celcius)
        {
            return (celcius * 9 / 5) + 32;
        }
    }
}
