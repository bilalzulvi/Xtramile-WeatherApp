using WeatherApi.Dto;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.ServiceContract
{
    public interface ICountryService
    {
        GenericResponse<CountryDto> GetById(string id);

        GenericCollectionResponse<CountryDto> GetAll();
    }
}
