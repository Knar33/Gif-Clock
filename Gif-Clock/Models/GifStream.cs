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
                List<Color> globalColorTable = new List<Color>();
                globalColorTable.Add(Color.FromArgb(0, 0, 0));
                globalColorTable.Add(Color.FromArgb(255, 255, 255));

                GifEncoder encoder = new GifEncoder(outputStream, 1, 1, globalColorTable);
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