using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WeatherApi.Dto;
using WeatherApi.Model;
using WeatherApi.ServiceContract;

namespace WeatherApi.Controllers.V1
{
    public class WeatherController : BaseApiController
    {
        #region Fields

        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly IWeatherService weatherService;

        #endregion

        #region Constructor

        public WeatherController(ICountryService countryService, ICityService cityService, IWeatherService weatherService)
        {
            this.countryService = countryService;
            this.cityService = cityService;
            this.weatherService = weatherService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get weather detail
        /// </summary>
        /// <param name="countryId">the request must have base countryId</param>
        /// <param name="cityId">the request must have base cityId</param>
        /// <returns>the status code and message</returns>
        /// <response code="200">The operation is success</response>
        /// <response code="400">One or more mandatory fields are missing or in incorrect format</response>
        /// <response code="401">The operation is not contains token authentication</response>
        /// <response code="404">The data is empty.</response>
        /// <response code="500">Exception occurred on the API side</response>
        [HttpGet]
        [Route("/v{version:apiversion}/weather")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorModel))]
        public IActionResult GetByCountryIdAndCityId(string countryId, string cityId)
        {
            if (string.IsNullOrEmpty(countryId) || string.IsNullOrEmpty(cityId))
            {
                return this.BadRequest(
                    new ApiErrorModel()
                    {
                        ErrorMessages = new[] { Resource.General_BadRequest }
                    });
            }

            var countryResponse = this.countryService.GetById(countryId);
            if (countryResponse.IsError())
            {
                return this.BadRequest(
                    new ApiErrorModel()
                    {
                        ErrorMessages = countryResponse.GetMessageErrorTextArray()
                    });
            }

            var cityResponse = this.cityService.GetByCountryIdAndCityId(countryId, cityId);
            if (cityResponse.IsError())
            {
                return this.BadRequest(
                    new ApiErrorModel()
                    {
                        ErrorMessages = cityResponse.GetMessageErrorTextArray()
                    });
            }

            var weatherResponse = this.weatherService.GetByCityName(cityResponse.Data.Name);
            if (weatherResponse.IsError())
            {
                return this.BadRequest(
                    new ApiErrorModel()
                    {
                        ErrorMessages = weatherResponse.GetMessageErrorTextArray()
                    });
            }

            var response = GetWeatherModel(weatherResponse.Data);

            return this.Ok(response);
        }

        #endregion

        #region Private Methods

        private WeatherModel GetWeatherModel(WeatherDto weatherDto)
        {
            return new WeatherModel()
            {
                Country = weatherDto.Country,
                City = weatherDto.City,
                Location = weatherDto.Location,
                Time = weatherDto.Time,
                Wind = weatherDto.Wind,
                Visibility = weatherDto.Visibility,
                SkyCondition = weatherDto.SkyCondition,
                TemperatureCelcius = weatherDto.TemperatureCelcius.ToString(),
                TemperatureFahrenheit = weatherDto.TemperatureFahrenheit.ToString(),
                DewPoint = weatherDto.TemperatureMinimum + "-" + weatherDto.TemperatureMaximum,
                RelativeHumidity = weatherDto.RelativeHumidity,        
                Pressure = weatherDto.Pressure,     
            };
        }

        #endregion
    }
}
