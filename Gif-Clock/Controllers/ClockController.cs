using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace GifClock.Controllers
{
    public class ClockController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse();
            Action<Stream, HttpContent, TransportContext> writeToStream = GifStream.WriteToStream;
            response.Content = new PushStreamContent(writeToStream, new MediaTypeHeaderValue("text/plain"));

            return response;
        }
    }
}
