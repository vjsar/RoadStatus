using Microsoft.Extensions.Configuration;
using RoadStatus.RestAPI.Interfaces;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;

namespace RoadStatus.RestAPI
{
    public class Configuration : IConfiguration
    {

        public Configuration(IConfigurationRoot configurationRoot)
        {
            Url = configurationRoot.GetSection("Url").Value;
            AppId = configurationRoot.GetSection("AppId").Value;
            AppKey = configurationRoot.GetSection("AppKey").Value;
        }

        public string Url { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }

    }
}
