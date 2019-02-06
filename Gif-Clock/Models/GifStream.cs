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
        public IGenerator ImageGenerator { get; set; }

        public GifStream(IGenerator imageGenerator)
        {
            ImageGenerator = imageGenerator;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                GifEncoder encoder = new GifEncoder(outputStream, 62, 18);

                while (true)
                {
                    await encoder.AddFrame(ImageGenerator.GenerateImage(), 0, 0);
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