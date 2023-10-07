using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeatherApi.Dto;
using WeatherApi.Model;
using WeatherApi.ServiceContract;

namespace WeatherApi.Controllers.V1
{
    public class CityController : BaseApiController
    {
        #region Fields

        private readonly ICityService cityService;
        private readonly ICountryService countryService;

        #endregion

        #region Constructor

        public CityController(ICityService cityService, ICountryService countryService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get city list
        /// </summary>
        /// <param name="countryId">the request must have base countryId</param>
        /// <returns>the status code and message</returns>
        /// <response code="200">The operation is success</response>
        /// <response code="400">One or more mandatory fields are missing or in incorrect format</response>
        /// <response code="401">The operation is not contains token authentication</response>
        /// <response code="404">The data is empty.</response>
        /// <response code="500">Exception occurred on the API side</response>
        [HttpGet]
        [Route("/v{version:apiversion}/city/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityListModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorModel))]
        public IActionResult List(string countryId)
        {
            if (string.IsNullOrEmpty(countryId))
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

            var cityResponse = this.cityService.GetAllByCountryId(countryId);
            if (cityResponse.IsError())
            {
                return this.BadRequest(
                    new ApiErrorModel()
                    {
                        ErrorMessages = cityResponse.GetMessageErrorTextArray()
                    });
            }

            if (!cityResponse.DtoCollection.Any())
            {
                return this.NotFound(
                    new ApiErrorModel()
                    {
                        ErrorMessages = new[] { Resource.General_NotFound }
                    });
            }

            var cityListModel = cityResponse.DtoCollection.Select(ct => GetCityModel(ct)).ToList();

            var response = new CityListModel()
            {
                Cities = cityListModel,
            };

            return this.Ok(response);

        }

        #endregion

        #region Private Methods

        private CityModel GetCityModel(CityDto dto)
        {
            return new CityModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                CountryId = dto.CountryId,
            };
        }

        #endregion
    }
}
