using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoadStatus.UnitTest
{
    public class DelegatingHandlerStub:DelegatingHandler
    {

        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;


        public DelegatingHandlerStub()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            _handlerFunc = (request, cancellationToken) =>
            {
                if (request.RequestUri.PathAndQuery.Contains("200"))
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a24\",\"displayName\":\"A24\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.23393,51.33958],[-0.10287,51.49159]]\",\"envelope\":\"[[-0.23393,51.33958],[-0.23393,51.49159],[-0.10287,51.49159],[-0.10287,51.33958],[-0.23393,51.33958]]\",\"url\":\"/Road/a24\"}]");
                }
                else if (request.RequestUri.PathAndQuery.Contains("404"))
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Content = new StringContent("{\"$type\":\"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\",\"timestampUtc\":\"2019-01-29T06:16:15.526704Z\",\"exceptionType\":\"EntityNotFoundException\",\"httpStatusCode\":404,\"httpStatus\":\"NotFound\",\"relativeUri\":\"/Road/A234\",\"message\":\"The following road id is not recognised: A234\"}");
                }

                else if (request.RequestUri.PathAndQuery.Contains("400"))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }

                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}
