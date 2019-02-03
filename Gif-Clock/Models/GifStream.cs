using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Drawing;

namespace GifClock
{
    public class GifStream
    {
        public static async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                GifEncoder encoder = new GifEncoder(outputStream, 1, 1, new List<Color>());
                while (true)
                {
                    //Generate clock Image frame
                    //await encoder.AddFrame();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                //ShakeItOff(ex);
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}