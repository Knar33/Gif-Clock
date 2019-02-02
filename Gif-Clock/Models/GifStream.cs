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
            var buffer = new byte[1000];

            try
            {
                //10 minutes of loop, 1kb per second
                for (int e = 0; e < 600; e++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            buffer[(j*100) + i] = (byte)(i + 50);
                        }
                    }
                    await gifStream.WriteAsync(buffer, 0, 1000);
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