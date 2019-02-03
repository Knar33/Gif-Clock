using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GifClock
{
    public class GifEncoder
    {
        public Stream gifStream { get; set; }

        public GifEncoder(Stream inputStream, int width, int height, List<Color> globalColorTable)
        {
            gifStream = inputStream;
            //Encode header
            Task.Run(() => WriteString("GIF89a")).Wait();
            //Logican Screen Descriptor
            //Global Color Table
        }

        public async Task AddFrame(Image frame)
        {
        }

        private void WriteByte(int value)
        {
            gifStream.WriteByte(Convert.ToByte(value));
        }

        private void WriteShort(int value)
        {
            gifStream.WriteByte(Convert.ToByte(value & 0xff));
            gifStream.WriteByte(Convert.ToByte((value >> 8) & 0xff));
        }

        private async Task WriteString(string value)
        {
            await gifStream.WriteAsync(value.ToArray().Select(c => (byte)c).ToArray(), 0, value.Length);
        }
    }
}