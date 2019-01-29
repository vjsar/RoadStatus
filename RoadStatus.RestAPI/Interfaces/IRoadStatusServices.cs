using RoadStatus.RestAPI.Models;
using System.Text;

namespace RoadStatus.RestAPI.Interfaces
{
    public interface IRoadStatusServices
    {
        int GetRoadStatus(string roadName);
        StringBuilder Message { get; set; }
        RoadCorridor RoadCorridor { get; set; }
        ApiError ApiError { get; set; }
        void Print();
    }
}
