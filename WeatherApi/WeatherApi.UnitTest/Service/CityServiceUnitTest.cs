using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;
using WeatherApi.Service;

namespace WeatherApi.UnitTest.Service
{
    [TestClass]
    public class CityServiceUnitTest
    {
        #region Fields

        private Mock<ICityRepository> cityRepository;
        private CityService cityService;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.cityRepository = new Mock<ICityRepository>();
            this.cityService = new CityService(this.cityRepository.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCountryIdAndCityId

        [TestMethod]
        public void GetByCountryIdAndCityId_ParameterNotValid_ReturnErrorMessage()
        {
            this.PrepareCityRepositoryGetByCountryIdAndCityIdNoValue();
            var response = cityService.GetByCountryIdAndCityId(null, null);
            Assert.IsNull(response.Data);
            Assert.AreEqual(Resource.General_ParameterEmpty, response.GetErrorMessage());
        }

        [TestMethod]
        public void GetByCountryIdAndCityId_DataNotFound_ReturnErrorMessage()
        {
            this.PrepareCityRepositoryGetByCountryIdAndCityIdNoValue();
            var response = cityService.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.IsNull(response.Data);
            Assert.AreEqual(Resource.General_CityNotFound, response.GetErrorMessage());
        }

        [TestMethod]
        public void GetByCountryIdAndCityId_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCityRepositoryGetByCountryIdAndCityId();
            var response = cityService.GetByCountryIdAndCityId("IDN", "JKT");
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Jakarta", response.Data.Name);
        }

        #endregion

        #region GetAllByCountryId

        [TestMethod]
        public void GetAllByCountryId_ParameterNotValid_ReturnErrorMessage()
        {
            var response = cityService.GetAllByCountryId(null);
            Assert.IsTrue(response.DtoCollection.Count == 0);
            Assert.AreEqual(Resource.General_ParameterEmpty, response.GetErrorMessage());
        }

        [TestMethod]
        public void GetAllByCountryId_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCityRepositoryGetGetAllByCountryId();
            var response = cityService.GetAllByCountryId("IDN");
            Assert.IsNotNull(response.DtoCollection);
            Assert.IsTrue(response.DtoCollection.Count > 0);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Prepare

        private void PrepareCityRepositoryGetByCountryIdAndCityId()
        {
            var data = new CityDto()
            {
                Id = "JKT",
                Name = "Jakarta",
                CountryId = "IDN"
            };

            this.cityRepository.Setup(item => item.GetByCountryIdAndCityId(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityRepositoryGetByCountryIdAndCityIdNoValue()
        {
            var data = (CityDto)null;

            this.cityRepository.Setup(item => item.GetByCountryIdAndCityId(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCityRepositoryGetGetAllByCountryId()
        {
            var data = new List<CityDto>();
            data.Add(new CityDto
            {
                Id = "JKT",
                Name = "Jakarta",
                CountryId = "IDN"
            });

            this.cityRepository.Setup(item => item.GetAllByCountryId(It.IsAny<string>()))
                .Returns(data);
        }

        #endregion

        #endregion
    }
}
