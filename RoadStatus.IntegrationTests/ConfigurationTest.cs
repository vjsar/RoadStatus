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
        [Fact (Skip = "AppId and AppKey should have a valid data in the appsettings.json and then update the test values.")]
        public void When_Configuration_Called_Get_Correct_Values()
        {
            var expectedConfigurationAppId = "";
            var expectedConfigurationAppKey ="";
            var expectedConfigurationUrl = "https://api.tfl.gov.uk/Road/";  

            Assert.NotNull(_configuration);
            Assert.Equal(expectedConfigurationAppId,_configuration.AppId);
            Assert.Equal(expectedConfigurationAppKey,_configuration.AppKey);
            Assert.Equal(expectedConfigurationUrl,_configuration.Url);
        }

        
    }
}
