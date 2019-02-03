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
            //Header
            Task.Run(() => WriteString("GIF89a")).Wait();

            //Logican Screen Descriptor
            WriteShort(width); //Canvas Width
            WriteShort(height); //Canvas Height
            GeneratePackedField(globalColorTable.Count); //Packed Field
            WriteByte(0); //Background Color Index
            WriteByte(0); //Pixel Aspect Ratio

            //Global Color Table
        }

        public async Task AddFrame(Image frame)
        {
        }

        private void GeneratePackedField(int colorCount)
        {
            //The first bit is the Global Color Table Flag
            //The next 3 bits are the Color Resolution, which I am setting to 1
            //The next is the sort flag. It will always be zero for this
            //The last 3 bits are the Global Color Table Size. The real size of the table is 2^(n+1) where n is the Global Color Table Size.
            int packedFieldValue = 16;
            if (colorCount > 0)
            {
                packedFieldValue += 128;
            }

            int globalColorTableSize = 0;
            for (int p = 1; p < 8; p++)
            {
                if (colorCount > Math.Pow(2, p))
                {
                    globalColorTableSize++;
                }
            }
            packedFieldValue += globalColorTableSize;

            WriteByte(packedFieldValue);
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