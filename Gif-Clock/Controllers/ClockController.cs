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
            Clock clock = new Clock();
            GifStream gifStream = new GifStream(clock);
            Action<Stream, HttpContent, TransportContext> writeToStream = gifStream.WriteToStream;
            response.Content = new PushStreamContent(writeToStream, new MediaTypeHeaderValue("image/gif"));

            return response;
        }

        public HttpResponseMessage Get(string timeZone)
        {
            var response = Request.CreateResponse();
            Clock clock = new Clock(timeZone);
            GifStream gifStream = new GifStream(clock);
            Action<Stream, HttpContent, TransportContext> writeToStream = gifStream.WriteToStream;
            response.Content = new PushStreamContent(writeToStream, new MediaTypeHeaderValue("image/gif"));

            return response;
        }
    }
}
