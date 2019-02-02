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
        public async void WriteToStream(Stream gifStream, HttpContent content, TransportContext context)
        {
            var buffer = new byte[1];

            try
            {
                for (int i = 0; i < 256; i++)
                {
                    buffer[0] = (byte)i;
                    await gifStream.WriteAsync(buffer, 0, 1);
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                gifStream.Close();
            }
        }
    }
}