using RoadStatus.RestAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RoadStatus.RestAPI
{
    public class RestClient : IRestClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public RestClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;

        }
        public HttpStatusCode StatusCode { get; set; }

        public string GetRoadStatus(string roadName)
        {
            using (_httpClient)
            {

                var url = CreateUrl(roadName);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var responseMessage = _httpClient.SendAsync(requestMessage).Result;

                StatusCode = responseMessage.StatusCode;

                if (responseMessage.StatusCode != HttpStatusCode.OK && responseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException($"Http Response Error Status Code {responseMessage.StatusCode}");
                }

                var content = responseMessage.Content.ReadAsStringAsync().Result;

                return content;
            }

            
        }

        private string CreateUrl(string roadName)
        {
            return $"{_configuration.Url}{roadName}?app_id={_configuration.AppId}&app_key={_configuration.AppKey}";
        }

    }
}
