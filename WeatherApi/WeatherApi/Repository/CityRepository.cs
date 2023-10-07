using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;

namespace WeatherApi.Repository
{
    [ExcludeFromCodeCoverage]
    public class CityRepository : ICityRepository
    {
        private readonly List<CityDto> cities = new List<CityDto>();

        public CityRepository()
        {
            this.cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = "JKT",
                    Name = "Jakarta",
                    CountryId = "IDN"
                },
                new CityDto()
                {
                    Id = "MTM",
                    Name = "Mataram",
                    CountryId = "IDN"
                },
                new CityDto()
                {
                    Id = "TKY",
                    Name = "Tokyo",
                    CountryId = "JPN"
                },
                new CityDto()
                {
                    Id = "OSK",
                    Name = "Osaka",
                    CountryId = "JPN"
                },
                new CityDto()
                {
                    Id = "SLM",
                    Name = "Salem",
                    CountryId = "IND"
                },
                new CityDto()
                {
                    Id = "HRD",
                    Name = "Harda",
                    CountryId = "IND"
                }
            };
        }

        public CityDto GetByCountryIdAndCityId(string countryId, string cityId)
        {
            return cities.FirstOrDefault(ct => ct.CountryId == countryId && ct.Id == cityId);
        }

        public List<CityDto> GetAllByCountryId(string countryId)
        {
            return cities.Where(ct => ct.CountryId == countryId).ToList();
        }     
    }
}
