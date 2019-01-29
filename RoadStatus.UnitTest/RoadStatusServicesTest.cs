using System;
using Xunit;
using Moq;
using RoadStatus.RestAPI;
using System.Net.Http;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.UnitTest
{
    public class RoadStatusServicesTest
    {
        private readonly Mock<IConfiguration> ConfigurationMock;
        private readonly IRestClient RestClientMock;
        private readonly RoadStatusServices Services;
        private string RoadName;

        public RoadStatusServicesTest()
        {
            ConfigurationMock = new Mock<IConfiguration>();
            RestClientMock = new RestClient(ConfigurationMock.Object, new HttpClient(new DelegatingHandlerStub()));
            Services = new RoadStatusServices(RestClientMock);
        }

        [Fact]
        public void When_Road_Is_Valid_And_Returns_Valid_Data()
        {
            RoadName = "A24";
            var expectedResult = 0;
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status200");
            var actualResult = GetRoadStatus(RoadName);
            Assert.Equal(expectedResult, actualResult); 
        }

        [Fact]
        public void When_Road_Is_InValid_And_Returns_Valid_Data()
        {
            RoadName = "A234";
            var expectedResult = 1;
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status404");
            var actualResult = GetRoadStatus(RoadName);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void When_Road_Is_Valid_And_Returns_Valid_Message()
        {
            RoadName = "A24";
            var message = "\t The status of the A24 is as follows\r\n\t Road Status is Good\r\n\t Road Status Description is No Exceptional Delays";
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status200");
            var actualResult = GetRoadStatus(RoadName);
            Assert.NotNull(Services.Message);
            Assert.Equal(message, Services.Message.ToString());

        }

        private int GetRoadStatus(string roadName)
        {
            return Services.GetRoadStatus(RoadName);
        }
    }
}
