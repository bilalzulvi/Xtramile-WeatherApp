using System;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public GenericResponse<CountryDto> GetById(string id)
        {
            var response = new GenericResponse<CountryDto>();

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    response.AddErrorMessage(Resource.General_ParameterEmpty);
                    return response;
                }

                var country = this.countryRepository.GetById(id);
                if (country == null)
                {
                    response.AddErrorMessage(Resource.General_CountryNotFound);
                    return response;
                }

                response.Data = country;
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public GenericCollectionResponse<CountryDto> GetAll()
        {
            var response = new GenericCollectionResponse<CountryDto>();

            try
            {
                var countries = this.countryRepository.GetAll();
                foreach (var country in countries)
                {
                    response.DtoCollection.Add(country);
                }
            }
            catch (Exception ex)
            {
                response.AddErrorMessage(ex.Message);
            }

            return response;
        }      
    }
}
