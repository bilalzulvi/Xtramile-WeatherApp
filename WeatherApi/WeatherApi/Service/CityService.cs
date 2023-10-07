using System;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public GenericResponse<CityDto> GetByCountryIdAndCityId(string countryId, string cityId)
        {
            var response = new GenericResponse<CityDto>();

            try
            {
                if (string.IsNullOrEmpty(countryId) || string.IsNullOrEmpty(cityId))
                {
                    response.AddErrorMessage(Resource.General_ParameterEmpty);
                    return response;
                }

                var city = this.cityRepository.GetByCountryIdAndCityId(countryId, cityId);
                if (city == null)
                {
                    response.AddErrorMessage(Resource.General_CityNotFound);
                    return response;
                }

                response.Data = city;
            }
            catch (Exception ex)
            {

                response.AddErrorMessage(ex.Message);
            }

            return response;
        }

        public GenericCollectionResponse<CityDto> GetAllByCountryId(string countryId)
        {
            var response = new GenericCollectionResponse<CityDto>();

            try
            {
                if (string.IsNullOrEmpty(countryId))
                {
                    response.AddErrorMessage(Resource.General_ParameterEmpty);
                    return response;
                }

                var cities = this.cityRepository.GetAllByCountryId(countryId);
                foreach (var city in cities)
                {
                    response.DtoCollection.Add(city);
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
