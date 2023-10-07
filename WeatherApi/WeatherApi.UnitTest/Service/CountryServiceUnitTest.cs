using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WeatherApi.Dto;
using WeatherApi.RepositoryContract;
using WeatherApi.Service;

namespace WeatherApi.UnitTest.Service
{
    [TestClass]
    public class CountryServiceUnitTest
    {
        #region Fields

        private Mock<ICountryRepository> countryRepository;
        private CountryService countryService;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.countryRepository = new Mock<ICountryRepository>();
            this.countryService = new CountryService(this.countryRepository.Object);
        }

        #endregion

        #region Test Methods

        #region GetById

        [TestMethod]
        public void GetById_ParameterNotValid_ReturnErrorMessage()
        {
            var response = countryService.GetById(null);
            Assert.IsNull(response.Data);
            Assert.AreEqual(Resource.General_ParameterEmpty, response.GetErrorMessage());
        }

        [TestMethod]
        public void GetById_DataNotFound_ReturnErrorMessage()
        {
            this.PrepareCountryRepositoryGetByIdNoValue();
            var response = countryService.GetById("IDN");
            Assert.IsNull(response.Data);
            Assert.AreEqual(Resource.General_CountryNotFound, response.GetErrorMessage());
        }

        [TestMethod]
        public void GetById_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCountryRepositoryGetById();
            var response = countryService.GetById("IDN");
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Indonesia", response.Data.Name);
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAll_Valid_ReturnStatusCodeOk()
        {
            this.PrepareCountryRepositoryGetAll();
            var response = countryService.GetAll();
            Assert.IsNotNull(response.DtoCollection);
            Assert.IsTrue(response.DtoCollection.Count > 0);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Prepare

        private void PrepareCountryRepositoryGetById()
        {
            var data = new CountryDto()
            {
                Id = "IDN",
                Name = "Indonesia"
            };

            this.countryRepository.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCountryRepositoryGetByIdNoValue()
        {
            var data = (CountryDto)null;

            this.countryRepository.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(data);
        }

        private void PrepareCountryRepositoryGetAll()
        {
            var data = new List<CountryDto>();
            data.Add(new CountryDto
            {
                Id = "IDN",
                Name = "Indonesia"
            });

            this.countryRepository.Setup(item => item.GetAll())
                .Returns(data);
        }

        #endregion

        #endregion
    }
}
