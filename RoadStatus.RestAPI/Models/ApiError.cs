using System;
using System.Collections.Generic;
using System.Text;

namespace RoadStatus.RestAPI.Models
{
    public class ApiError
    {
        public string TimestampUtc { get; set; }
        public string ExceptionType { get; set; }
        public string HttpStatusCode { get; set; }
        public string HttpStatus { get; set; }
        public string RelativeUri { get; set; }
        public string Message { get; set; }
    }
}
