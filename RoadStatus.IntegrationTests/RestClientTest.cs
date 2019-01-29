using System;
using System.Linq;
using Xunit;
using RoadStatus.RestAPI;
using System.Net.Http;
using Newtonsoft.Json;
using RoadStatus.RestAPI.Models;
using Autofac;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.IntegrationTests
{
    public class RestClientTest
    {

        private readonly IConfiguration Configuration;

        private readonly IRestClient RestClient;

        private string RoadName;

        public RestClientTest()
        {
            var container = new Container().Init();
            Configuration = container.Resolve<IConfiguration>();
            RestClient = container.Resolve<IRestClient>(); 
            
        }

        [Fact]
        public void When_Call_API_With_Valid_Road()
        {
            RoadName = "A24";
            var statusSeverity = "Good";
            var statusSeverityDescription = "No Exceptional Delays";
            var actualResult = GetRoadStatus<RoadCorridor[]>(RoadName).First();
            Assert.NotNull(actualResult);
            Assert.Equal(RoadName, actualResult.DisplayName);
            Assert.Equal(statusSeverity, actualResult.StatusSeverity);
            Assert.Equal(statusSeverityDescription, actualResult.StatusSeverityDescription);
        }

        [Fact]
        public void When_Call_API_With_In_Valid_Road()
        {
            RoadName = "A234";
            var httpStatus = "NotFound";
            var httpStatusCode = "404";
            var message = $"The following road id is not recognised: {RoadName}";
            var actualResult = GetRoadStatus<ApiError>(RoadName);
            Assert.NotNull(actualResult);
            Assert.Equal(message, actualResult.Message);
            Assert.Equal(httpStatus, actualResult.HttpStatus);
            Assert.Equal(httpStatusCode, actualResult.HttpStatusCode);
        }

        private T GetRoadStatus<T>(string roadName)
        {
           var result = RestClient.GetRoadStatus(roadName);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
