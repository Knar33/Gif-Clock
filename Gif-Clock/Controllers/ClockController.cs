﻿using System;
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
            var gifStream = new GifStream();

            var response = Request.CreateResponse();
            Action<Stream, HttpContent, TransportContext> writeToStream = gifStream.WriteToStream;
            response.Content = new PushStreamContent(writeToStream);

            return response;
        }
    }
}
