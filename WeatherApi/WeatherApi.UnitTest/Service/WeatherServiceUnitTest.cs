using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherApi.Service;
using WeatherApi.ServiceContract;
using WeatherApi.ServiceContract.Response;

namespace WeatherApi.UnitTest.Service
{
    [TestClass]
    public class WeatherServiceUnitTest
    {
        #region Fields

        private Mock<IApiManager> apiManagerMock;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationMock;
        private Mock<Microsoft.Extensions.Configuration.IConfigurationSection> configurationSectionMock;
        private WeatherService weatherService;

        #endregion

        #region Test Initialize

        [TestInitialize]
        public void Initialize()
        {
            this.apiManagerMock = new Mock<IApiManager>();
            this.configurationMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            this.configurationSectionMock = new Mock<IConfigurationSection>();
            this.weatherService = new WeatherService(
                this.apiManagerMock.Object,
                (Microsoft.Extensions.Configuration.IConfiguration)this.configurationMock.Object);

            this.configurationSectionMock.Setup(x => x.Value).Returns("http://localhost:7777");
            this.configurationMock.Setup(x => x.GetSection(It.IsAny<string>())).Returns(configurationSectionMock.Object);
        }

        #endregion

        #region Test Methods

        #region GetByCityName

        [TestMethod]
        public void GetByCityName_Valid_ReturnStatusCodeOk()
        {
            this.PrepareApiManagerSendRequest();
            var response = weatherService.GetByCityName("Jakarta");
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Jakarta", response.Data.City);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Prepare

        private void PrepareApiManagerSendRequest()
        {
            var data = new GenericResponse<string>()
            {
                Data = "{\"coord\":{\"lon\":106.8451,\"lat\":-6.2146},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"brokenclouds\",\"icon\":\"04n\"}],\"base\":\"stations\",\"main\":{\"temp\":303.35,\"feels_like\":306.37,\"temp_min\":301.98,\"temp_max\":304.42,\"pressure\":1012,\"humidity\":60},\"visibility\":8000,\"wind\":{\"speed\":1.54,\"deg\":50},\"clouds\":{\"all\":63},\"dt\":1696689791,\"sys\":{\"type\":1,\"id\":9383,\"country\":\"ID\",\"sunrise\":1696631692,\"sunset\":1696675571},\"timezone\":25200,\"id\":1642911,\"name\":\"Jakarta\",\"cod\":200}"
            };

            this.apiManagerMock.Setup(item => item.SendRequestAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(data);
        }

        #endregion

        #endregion
    }
}
