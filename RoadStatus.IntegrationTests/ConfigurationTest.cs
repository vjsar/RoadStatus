using System;
using System.Net.Http;
using Autofac;
using Microsoft.Extensions.Configuration;
using RoadStatus.RestAPI;
using Xunit;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.IntegrationTests
{
    public class ConfigurationTest
    {
        private IConfiguration _configuration;

        public ConfigurationTest()
        {
            _configuration = new Container().Init().Resolve<IConfiguration>();

        }
        [Fact]
        public void When_Configuration_Called_Get_Correct_Values()
        {
            var expectedConfigurationAppId = "b87b9062";
            var expectedConfigurationAppKey ="66d8b4f6de3d049a5ed79f4e65333097";
            var expectedConfigurationUrl = "https://api.tfl.gov.uk/Road/";  

            Assert.NotNull(_configuration);
            Assert.Equal(expectedConfigurationAppId,_configuration.AppId);
            Assert.Equal(expectedConfigurationAppKey,_configuration.AppKey);
            Assert.Equal(expectedConfigurationUrl,_configuration.Url);
        }

        
    }
}
