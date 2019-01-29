using System;
using System.Linq;
using Xunit;
using Moq;
using RoadStatus.RestAPI;
using System.Net.Http;
using Newtonsoft.Json;
using RoadStatus.RestAPI.Models;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.UnitTest
{
    public class RestClientTest
    {

        private readonly Mock<IConfiguration> ConfigurationMock;
        private readonly IRestClient RestClientMock;
        private readonly RoadStatusServices Services;
        private string RoadName;

        public RestClientTest()
        {
            ConfigurationMock = new Mock<IConfiguration>();
            RestClientMock = new RestClient(ConfigurationMock.Object, new HttpClient(new DelegatingHandlerStub()));
            Services = new RoadStatusServices(RestClientMock);
        }

        [Fact]
        public void When_Call_API_With_Valid_Url()
        {
            RoadName = "A24";
            var statusSeverity = "Good";
            var statusSeverityDescription = "No Exceptional Delays";
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status200");
            var actualResult = GetRoadStatus<RoadCorridor[]>(RoadName).First();
            Assert.NotNull(actualResult);
            Assert.Equal(statusSeverity, actualResult.StatusSeverity);
            Assert.Equal(statusSeverityDescription, actualResult.StatusSeverityDescription);
        }

        [Fact]
        public void When_Call_API_With_In_Valid_Url()
        {
            RoadName = "A234";
            var httpStatus = "NotFound";
            var httpStatusCode = "404";
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status404");
            var actualResult = GetRoadStatus<ApiError>(RoadName);
            Assert.NotNull(actualResult);
            Assert.Equal(httpStatus, actualResult.HttpStatus);
            Assert.Equal(httpStatusCode, actualResult.HttpStatusCode);
        }

        [Fact]
        public void When_Call_API_With_In_Correct_Url()
        {
            RoadName = "A234";
            ConfigurationMock.Setup(x => x.Url).Returns(() => "http://api.unitest.com/Status400");
            Assert.Throws<HttpRequestException>(() => RestClientMock.GetRoadStatus(RoadName));
        }


        private T GetRoadStatus<T>(string roadName)
        {
           var result = RestClientMock.GetRoadStatus(roadName);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
