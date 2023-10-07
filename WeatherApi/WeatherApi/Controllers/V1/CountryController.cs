using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeatherApi.Dto;
using WeatherApi.Model;
using WeatherApi.ServiceContract;

namespace WeatherApi.Controllers.V1
{
    public class CountryController : BaseApiController
    {
        #region Fields

        private readonly ICountryService countryService;

        #endregion

        #region Constructor

        public CountryController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get country list
        /// </summary>
        /// <returns>the status code and message</returns>
        /// <response code="200">The operation is success</response>
        /// <response code="400">One or more mandatory fields are missing or in incorrect format</response>
        /// <response code="401">The operation is not contains token authentication</response>
        /// <response code="404">The data is empty.</response>
        /// <response code="500">Exception occurred on the API side</response>
        [HttpGet]
        [Route("/v{version:apiversion}/country/list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryListModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorModel))]
        public IActionResult List()
        {
            var countryResponse = this.countryService.GetAll();
            if (!countryResponse.DtoCollection.Any())
            {
                return this.NotFound(
                    new ApiErrorModel()
                    {
                        ErrorMessages = new[] { Resource.General_NotFound }
                    });
            }

            var countryListModels = countryResponse.DtoCollection.Select(ct => GetCountryModel(ct)).ToList();

            var response = new CountryListModel()
            {
                Countries = countryListModels,
            };

            return this.Ok(response);
        }

        #endregion

        #region Private Methods

        private CountryModel GetCountryModel(CountryDto dto)
        {
            return new CountryModel()
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        #endregion
    }
}
