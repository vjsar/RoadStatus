using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using RoadStatus.RestAPI.Models;
using RoadStatus.RestAPI.Interfaces;
namespace RoadStatus.RestAPI
{
    public class RoadStatusServices:IRoadStatusServices
    {
        private IRestClient _restClient;

        public RoadStatusServices(IRestClient restClient)
        {
            _restClient = restClient;
            Message = new StringBuilder();
        }

        public StringBuilder Message { get ; set ; }
        public RoadCorridor RoadCorridor { get ; set ; }
        public ApiError ApiError { get ; set ; }

        public int GetRoadStatus(string roadName)
        {
            try
            {
                var roadStatus = _restClient.GetRoadStatus(roadName);

                if (!string.IsNullOrEmpty(roadStatus))
                {
                    if (_restClient.StatusCode == HttpStatusCode.OK)
                    {
                        RoadCorridor = JsonConvert.DeserializeObject<RoadCorridor[]>(roadStatus)?.First();
                        SetDisplayStatusMessage();
                        return 0;
                    }

                    if (_restClient.StatusCode == HttpStatusCode.NotFound)
                    {
                        ApiError = JsonConvert.DeserializeObject<ApiError>(roadStatus);
                        SetDisplayErrorMessage(roadName);
                        return 1;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return -1;

        }

        private void SetDisplayStatusMessage()
        {
                Message.AppendLine($"\t The status of the {RoadCorridor.DisplayName} is as follows");
                Message.AppendLine($"\t Road Status is {RoadCorridor.StatusSeverity}");
                Message.Append($"\t Road Status Description is {RoadCorridor.StatusSeverityDescription}");
                Print();
        }

        private void SetDisplayErrorMessage(string roadName)
        {
            Message.AppendLine($"{roadName} is not a valid road");
            Print();
        }

        public void Print()
        {
            Console.WriteLine(Message.ToString());
        }

    }
}
