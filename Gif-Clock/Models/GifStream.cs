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
                GifEncoder encoder = new GifEncoder(outputStream, 100, 100);

                while (true)
                {
                    //Generate clock Image frame
                    var white = Image.FromFile(@"C:\Users\Andrew\Documents\GitHub\Gif-Clock\white.png");
                    await encoder.AddFrame(white, 0, 0);
                    Thread.Sleep(1000);
                    var black = Image.FromFile(@"C:\Users\Andrew\Documents\GitHub\Gif-Clock\black.png");
                    await encoder.AddFrame(black, 0, 0);
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