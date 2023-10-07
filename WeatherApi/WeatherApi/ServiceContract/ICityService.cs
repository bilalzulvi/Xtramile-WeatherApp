using WeatherApi.Dto;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.ServiceContract
{
    public interface ICityService
    {
        GenericResponse<CityDto> GetByCountryIdAndCityId(string countryId, string cityId);

        GenericCollectionResponse<CityDto> GetAllByCountryId(string countryId);
    }
}
