using System.Collections.Generic;
using WeatherApi.Dto;

namespace WeatherApi.RepositoryContract
{
    public interface ICountryRepository
    {
        CountryDto GetById(string id);

        List<CountryDto> GetAll();
    }
}
