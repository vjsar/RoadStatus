using Autofac;
using Microsoft.Extensions.Configuration;
using RoadStatus.RestAPI;
using System;
using System.Net.Http;
using System.Linq;
using IConfiguration = RoadStatus.RestAPI.Interfaces.IConfiguration;
using RoadStatus.RestAPI.Interfaces;

namespace RoadStatus
{
    class Program
    {

        private static IConfigurationRoot _config;

        static int Main(string[] args)
        {

            var roadStatusServices = Configure().Resolve<IRoadStatusServices>();

            if (ValidateArgs(args) != null)
            {
                return roadStatusServices.GetRoadStatus(ValidateArgs(args));
            }

            return -1;
        }

        private static string ValidateArgs(string[] args)
        {
            if (args != null && args.Length ==1)
            {
                return args.First();
            }

            Console.WriteLine("Please enter:");
            Console.WriteLine("\tRoadStatus <RoadName>");
            return null;
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();


            _config = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            builder
                .Register(x => new Configuration(_config))
                .As<IConfiguration>();

            builder
                 .Register(x => new RestClient( x.Resolve<IConfiguration>(), new HttpClient()))
                .As<IRestClient>();
            builder
                .Register(x => new RoadStatusServices(x.Resolve<IRestClient>()))
                .As<IRoadStatusServices>();
                
            var container = builder.Build();
            return container;
        }
    }
    }

