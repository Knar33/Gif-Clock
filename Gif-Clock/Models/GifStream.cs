using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading;

namespace GifClock
{
    public class GifStream
    {
        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            var buffer = new byte[10];

            while (true)
            {
                buffer[0] = 0;
                buffer[1] = 1;
                buffer[2] = 2;
                await outputStream.WriteAsync(buffer, 0, 10);
                Thread.Sleep(1000);
            } 
        }
    }
}