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
    public class CountryControllerUnitTest
    {
        #region Fields

        private Mock<ICountryService> countryServiceMock;
        private CountryController countryController;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.countryServiceMock = new Mock<ICountryService>();
            this.countryController = new CountryController(this.countryServiceMock.Object);
        }

        #endregion

        #region Test Methods

        #region List

        [TestMethod]
        public void List_DataNotFound_ReturnStatusCodeNotFound()
        {
            this.PrepareCountryServiceGetAllNoValue();
            var response = (ObjectResult)countryController.List();
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [TestMethod]
        public void List_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCountryServiceGetAll();
            var response = (ObjectResult)countryController.List();
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Prepare

        private void PrepareCountryServiceGetAllNoValue()
        {
            this.countryServiceMock.Setup(item => item.GetAll())
                .Returns(new GenericCollectionResponse<CountryDto>());
        }

        private void PrepareCountryServiceGetAll()
        {
            var data = new GenericCollectionResponse<CountryDto>();
            data.DtoCollection.Add(new CountryDto
            {
                Id = "IDN",
                Name = "Indonesia"
            });

            this.countryServiceMock.Setup(item => item.GetAll())
                .Returns(data);
        }

        #endregion

        #endregion
    }
}
