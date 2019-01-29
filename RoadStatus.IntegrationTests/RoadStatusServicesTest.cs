using System;
using Xunit;
using RoadStatus.RestAPI;
using System.Net.Http;
using Autofac;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.IntegrationTests
{
    public class RoadStatusServicesTest
    {
        private readonly IConfiguration Configuration;

        private readonly IRestClient RestClient;

        private string RoadName;

        private readonly IRoadStatusServices Services;


        public RoadStatusServicesTest()
        {
            var container = new Container().Init();
            Configuration = container.Resolve<IConfiguration>();
            RestClient = container.Resolve<IRestClient>();
            Services = container.Resolve<IRoadStatusServices>();
        }

        [Fact]
        public void When_Road_Is_Valid_And_Returns_In_Valid_Result()
        {
            RoadName = "M4";
            var expectedResult = 1;
            var actualResult = GetRoadStatus(RoadName);
            Assert.Equal(expectedResult,actualResult);
        }

        [Fact]
        public void When_Road_Is_In_Valid_And_Returns_Valid_Result()
        {
            RoadName = "A4";
            var expectedResult = 0;
            var actualResult = GetRoadStatus(RoadName);
            Assert.Equal(expectedResult, actualResult);
        }

        private int GetRoadStatus(string roadName)
        {
            return Services.GetRoadStatus(RoadName);
        }
    }
}
