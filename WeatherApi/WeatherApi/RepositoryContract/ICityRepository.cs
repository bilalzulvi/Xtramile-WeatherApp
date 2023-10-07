using System.Collections.Generic;
using WeatherApi.Dto;

namespace WeatherApi.RepositoryContract
{
    public interface ICityRepository
    {
        CityDto GetByCountryIdAndCityId(string countryId, string cityId);

        List<CityDto> GetAllByCountryId(string countryId);
    }
}
