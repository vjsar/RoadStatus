using Autofac;
using Microsoft.Extensions.Configuration;
using RoadStatus.RestAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus.IntegrationTests
{
    public class Container
    {
        public IContainer Init()
        {
            var builder = new ContainerBuilder();
            var config = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            builder
                .Register(x => new Configuration(config))
                .As<IConfiguration>();

            builder
                .Register(x => new RestClient(x.Resolve<IConfiguration>(), new HttpClient()))
                .As<IRestClient>();
            builder
                .Register(x => new RoadStatusServices(x.Resolve<IRestClient>()))
                .As<IRoadStatusServices>();
            return builder.Build();

        }
    }
}
