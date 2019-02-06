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
        public int DelayTime { get; set; }
        private bool UseLocalColorTable;

        public GifStream(IGenerator imageGenerator, int delayTime, bool useLocalColorTable = false)
        {
            ImageGenerator = imageGenerator;
            DelayTime = delayTime;
            UseLocalColorTable = useLocalColorTable;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                GifEncoder encoder = new GifEncoder(outputStream, UseLocalColorTable);

                while (true)
                {
                    //TODO: return x, y offset from ImageGenerator, instead of hardcoding to 0,0
                    await encoder.AddFrame(ImageGenerator.GenerateImage(), 0, 0);
                    Thread.Sleep(DelayTime);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}