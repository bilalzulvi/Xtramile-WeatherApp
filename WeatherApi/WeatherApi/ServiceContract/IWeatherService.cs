using WeatherApi.Dto;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.ServiceContract
{
    public interface IWeatherService
    {
        GenericResponse<WeatherDto> GetByCityName(string cityName);
    }
}
