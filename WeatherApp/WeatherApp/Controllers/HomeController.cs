using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Core.Constant;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;

        #endregion

        #region Constructor

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this.configuration = configuration;
        }

        #endregion

        #region Public Methods

        public IActionResult Index()
        {
            var model = new IndexVm
            {
                Country = "",
                CountryList = GenerateCountryList(),
                City = "",
                CityList = GenerateEmptyList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetCityByCountryId(string countryId)
        {
            return this.Json(
                    new
                    {
                        IsSuccess = true,
                        Value = GenerateCityList(countryId),
                    });
        }

        [HttpGet]
        public IActionResult GetDetailByCountryIdAndCityId(string countryId, string cityId)
        {
            var model = this.GetWeatherAsync(countryId, cityId);
            return this.PartialView("Detail", model.Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Private Methods

        private SelectList GenerateEmptyList()
        {
            var countryListItem = new List<SelectListItem>();

            return new SelectList(countryListItem, "Value", "Text");
        }

        private SelectList GenerateCountryList()
        {
            var countryListItem = new List<SelectListItem>();

            var countryListModel = GetCountriesAsync();

            foreach(CountryModel countryModel in countryListModel.Result.Countries)
            {
                countryListItem.Add(new SelectListItem
                {
                    Text = countryModel.Name,
                    Value = countryModel.Id
                });
            }

            return new SelectList(countryListItem, "Value", "Text");
        }

        private SelectList GenerateCityList(string countryId)
        {
            var cityListItem = new List<SelectListItem>();

            var cityListModel = GetCitiesAsync(countryId);

            foreach (CityModel cityModel in cityListModel.Result.Cities)
            {
                cityListItem.Add(new SelectListItem
                {
                    Text = cityModel.Name,
                    Value = cityModel.Id
                });
            }

            return new SelectList(cityListItem, "Value", "Text");
        }

        private async Task<CountryListModel> GetCountriesAsync()
        {
            CountryListModel countryListModel = null;
            var url = this.configuration.GetValue<string>(ConfigConstant.WeatherApiUrl) + "/v1/country/list";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                countryListModel = await response.Content.ReadAsAsync<CountryListModel>();
            }

            return countryListModel;
        }

        private async Task<CityListModel> GetCitiesAsync(string countryId)
        {
            CityListModel cityListModel = null;
            var url = this.configuration.GetValue<string>(ConfigConstant.WeatherApiUrl) + "/v1/city/list?countryId=" + countryId;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                cityListModel = await response.Content.ReadAsAsync<CityListModel>();
            }

            return cityListModel;
        }

        private async Task<WeatherModel> GetWeatherAsync(string countryId, string cityId)
        {
            WeatherModel weatherModel = null;
            var url = this.configuration.GetValue<string>(ConfigConstant.WeatherApiUrl) + "/v1/weather?countryId=" + countryId  + "&cityId=" + cityId;
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                weatherModel = await response.Content.ReadAsAsync<WeatherModel>();
            }

            return weatherModel;
        }

        #endregion
    }
}