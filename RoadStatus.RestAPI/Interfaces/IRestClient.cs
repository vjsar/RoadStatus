using System;
using System.Net;

namespace RoadStatus.RestAPI.Interfaces
{
    public interface IRestClient
    {
        HttpStatusCode StatusCode { get; set; }

        string GetRoadStatus(string roadName);
    }
}
