using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherApi.Controllers.V1;
using WeatherApi.Dto;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.UnitTest.Controllers
{
    [TestClass]
    public class WeatherControllerUnitTest
    {
        #region Fields

        private Mock<ICityService> cityServiceMock;
        private Mock<ICountryService> countryServiceMock;
        private Mock<IWeatherService> weatherService;
        private WeatherController weatherController;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.cityServiceMock = new Mock<ICityService>();
            this.countryServiceMock = new Mock<ICountryService>();
            this.weatherService = new Mock<IWeatherService>();
            this.weatherController = new WeatherController(
                this.countryServiceMock.Object,
                this.cityServiceMock.Object,
                this.weatherService.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCountryIdAndCityId

        [TestMethod]
        public void GetByCountryIdAndCityId_DataNotFound_ReturnStatusCodeNotFound()
        {
            var response = (ObjectResult)weatherController.GetByCountryIdAndCityId(null, null);
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }


        [TestMethod]
        public void GetByCountryIdAndCityId_CountryNotValid_ReturnStatusCodeBadRequest()
        {
            this.PrepareCountryServiceGetByIdBadRequest();
            var response = (ObjectResult)this.weatherController.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetByCountryIdAndCityId_CityNotValid_ReturnStatusCodeBadRequest()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetByCountryIdAndCityIdBadRequest();
            var response = (ObjectResult)this.weatherController.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetByCountryIdAndCityId_WeatherNotValid_ReturnStatusCodeBadRequest()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetByCountryIdAndCityId();
            this.PrepareWeatherServiceGetByCityNameBadRequest();
            var response = (ObjectResult)this.weatherController.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void GetByCountryIdAndCityId_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetByCountryIdAndCityId();
            this.PrepareWeatherServiceGetByCityName();
            var response = (ObjectResult)weatherController.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Prepare

        private void PrepareCountryServiceGetById()
        {
            var data = new GenericResponse<CountryDto>()
            {
                Data = new CountryDto()
                {
                    Id = "IDN",
                    Name = "Indonesia"
                }
            };

            this.countryServiceMock.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCountryServiceGetByIdBadRequest()
        {
            var data = new GenericResponse<CountryDto>();
            data.AddErrorMessage(Resource.General_ParameterEmpty);

            this.countryServiceMock.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityServiceGetByCountryIdAndCityId()
        {
            var data = new GenericResponse<CityDto>()
            {
                Data = new CityDto()
                {
                    Id = "JKT",
                    Name = "Jakarta",
                    CountryId = "IDN"
                }
            };

            this.cityServiceMock.Setup(item => item.GetByCountryIdAndCityId(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityServiceGetByCountryIdAndCityIdBadRequest()
        {
            var data = new GenericResponse<CountryDto>();
            data.AddErrorMessage(Resource.General_ParameterEmpty);

            this.countryServiceMock.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareWeatherServiceGetByCityName()
        {
            var data = new GenericResponse<WeatherDto>()
            {
                Data = new WeatherDto()
            };

            this.weatherService.Setup(item => item.GetByCityName(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareWeatherServiceGetByCityNameBadRequest()
        {
            var data = new GenericResponse<WeatherDto>();
            data.AddErrorMessage(Resource.General_ParameterEmpty);

            this.weatherService.Setup(item => item.GetByCityName(It.IsAny<string>()))
                .Returns(data);
        }

        #endregion

        #endregion
    }
}
