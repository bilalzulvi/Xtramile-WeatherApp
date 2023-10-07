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
    public class CityControllerUnitTest
    {
        #region Fields

        private Mock<ICityService> cityServiceMock;
        private Mock<ICountryService> countryServiceMock;   
        private CityController cityController;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.cityServiceMock = new Mock<ICityService>();
            this.countryServiceMock = new Mock<ICountryService>();
            this.cityController = new CityController(this.cityServiceMock.Object, this.countryServiceMock.Object);
        }

        #endregion

        #region Test Methods

        #region List

        [TestMethod]
        public void List_RequestNull_ReturnStatusCodeBadRequest()
        {
            var response = (ObjectResult)this.cityController.List(null);
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void List_CountryNotValid_ReturnStatusCodeBadRequest()
        {
            this.PrepareCountryServiceGetByIdBadRequest();
            var response = (ObjectResult)this.cityController.List("IDN");
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void List_CityNotValid_ReturnStatusCodeBadRequest()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetAllByCountryIdBadRequest();
            var response = (ObjectResult)this.cityController.List("IDN");
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void List_DataNotFound_ReturnStatusCodeNotFound()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetAllByCountryIdNoValue();
            var response = (ObjectResult)this.cityController.List("IDN");
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void List_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCountryServiceGetById();
            this.PrepareCityServiceGetAllByCountryId();
            var response = (ObjectResult)this.cityController.List("IDN");
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

        private void PrepareCityServiceGetAllByCountryId()
        {
            var data = new GenericCollectionResponse<CityDto>();
            data.DtoCollection.Add(new CityDto
            {
                Id = "JKT",
                Name = "Jakarta",
                CountryId = "IDN"
            });

            this.cityServiceMock.Setup(item => item.GetAllByCountryId(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityServiceGetAllByCountryIdBadRequest()
        {
            var data = new GenericCollectionResponse<CityDto>();
            data.AddErrorMessage(Resource.General_ParameterEmpty);

            this.cityServiceMock.Setup(item => item.GetAllByCountryId(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityServiceGetAllByCountryIdNoValue()
        {
            var data = new GenericCollectionResponse<CityDto>();

            this.cityServiceMock.Setup(item => item.GetAllByCountryId(It.IsAny<string>()))
                .Returns(data);
        }

        #endregion

        #endregion
    }
}
