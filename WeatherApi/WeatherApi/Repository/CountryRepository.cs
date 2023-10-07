using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;

namespace WeatherApi.Repository
{
    [ExcludeFromCodeCoverage]
    public class CountryRepository : ICountryRepository
    {
        private readonly List<CountryDto> countries;

        public CountryRepository()
        {
            this.countries = new List<CountryDto>()
            {
                new CountryDto()
                {
                    Id = "IDN",
                    Name = "Indonesia"
                },
                new CountryDto()
                {
                    Id = "JPN",
                    Name = "Japan"
                },
                new CountryDto()
                {
                    Id = "IND",
                    Name = "India"
                }
            };
        }

        public CountryDto GetById(string id)
        {
            return countries.FirstOrDefault(ct => ct.Id == id);
        }

        public List<CountryDto> GetAll()
        {
            return this.countries;
        }
    }
}
