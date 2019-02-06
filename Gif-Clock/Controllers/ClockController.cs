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
        public HttpResponseMessage Get(string timeZone = "Central Standard Time")
        {
            var response = Request.CreateResponse();
            GifStream gifStream = new GifStream(new Clock(timeZone), 1000, false);
            Action<Stream, HttpContent, TransportContext> writeToStream = gifStream.WriteToStream;
            response.Content = new PushStreamContent(writeToStream, new MediaTypeHeaderValue("image/gif"));

            return response;
        }
    }
}
