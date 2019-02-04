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
        public int GlobalColorTableSize { get; set; }
        public bool GlobalColorTableIndicator { get; set; }

        public GifEncoder(Stream inputStream, int width, int height, List<Color> globalColorTable)
        {
            if (globalColorTable.Count > 256)
            {
                globalColorTable = globalColorTable.Take(256).ToList();
            }
            gifStream = inputStream;

            //Header
            Task.Run(() => WriteString("GIF89a")).Wait();

            //Logical Screen Descriptor
            WriteShort(width); //Canvas Width
            WriteShort(height); //Canvas Height
            //Packed Field
            int packedFieldValue = 16;
            if (globalColorTable.Count > 0)
            {
                GlobalColorTableIndicator = true;
                packedFieldValue += 128;
            }
            GlobalColorTableSize = 0;
            for (int p = 1; p < 8; p++)
            {
                if (globalColorTable.Count > Math.Pow(2, p))
                {
                    GlobalColorTableSize++;
                }
            }
            packedFieldValue += GlobalColorTableSize;
            WriteByte(packedFieldValue);
            WriteByte(0); //Background Color Index
            WriteByte(0); //Pixel Aspect Ratio

            //Global Color Table
            for (int i = 0; i < globalColorTable.Count; i++)
            {
                WriteByte(globalColorTable[i].R);
                WriteByte(globalColorTable[i].G);
                WriteByte(globalColorTable[i].B);
            }
            int colorsRequired = (int)Math.Pow(2, (GlobalColorTableSize + 1));
            for (int i = globalColorTable.Count; i < colorsRequired; i++)
            {
                WriteByte(0);
                WriteByte(0);
                WriteByte(0);
            }
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